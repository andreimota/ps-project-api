using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ps_project_api.Business.Interfaces;
using ps_project_api.Common.Implementations;
using ps_project_api.Common.Interfaces;
using ps_project_api.DAL;
using ps_project_api.DAL.Entities;
using ps_project_api.DAL.Enums;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_project_api.Business.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly TransfusionCenterContext _dbContext;
        private readonly IEmailSender _emailSender;

        public AppointmentService(TransfusionCenterContext dbContext, IEmailSender emailSender)
        {
            _dbContext = dbContext;
            _emailSender = emailSender;
        }

        public async Task<Appointment> BookAppointment(Appointment appointment)
        {
            var donor = _dbContext.Set<Donor>()
                .Include(u => u.User)
                .Where(d => d.Id == appointment.DonorId)
                .FirstOrDefault();

            var doctor = _dbContext.Set<Doctor>()
                .Include(d => d.Appointments)
                .Where(d => d.TransfusionCenterId == appointment.TransfusionCenterId)
                .OrderBy(d => d.Appointments
                    .Where(a => a.Status != (int)AppointmentStatus.Cancelled && a.Date.Date == appointment.Date.Date)
                    .Count())
                .FirstOrDefault();

            var currentAppointmentCount = _dbContext.Set<Appointment>()
                .Where(a => a.TransfusionCenterId == appointment.TransfusionCenterId 
                        && a.Date.Date == appointment.Date.Date
                        && a.Status != (int)AppointmentStatus.Cancelled)
                .Count();

            var donationsLimit = _dbContext.Set<TransfusionCenter>().Where(t => t.Id == appointment.TransfusionCenterId).Select(t => t.DonationsLimit).FirstOrDefault();

            if(currentAppointmentCount >= donationsLimit)
            {
                throw new ArgumentException("This transfusion center is fully booked for the day.");
            }

            appointment.DoctorId = doctor.Id;
            appointment.Status = (int)AppointmentStatus.Pending;

            _dbContext.Add(appointment);

            _dbContext.SaveChanges();

            _emailSender.SendAppointmentConfirmationEmail(donor.User.Email, donor.FirstName + " " + donor.LastName, appointment.Date.ToShortDateString());

            return appointment;
        }

        public Appointment ChangeAppointmentStatus(Guid appointmentId, int status)
        {
            var appointment = _dbContext.Set<Appointment>()
                .Where(d => d.Id == appointmentId)
                .FirstOrDefault();

            appointment.Status = status;

            _dbContext.SaveChanges();

            return appointment;
        }

        public IEnumerable<Appointment> GetDoctorAppointments(Guid doctorId)
        {
            return _dbContext.Set<Appointment>()
                .Include(d => d.Doctor)
                .Include(d => d.Donor)
                .Include(d => d.TransfusionCenter)
                .Where(a => a.DoctorId == doctorId 
                        && a.Status != (int)AppointmentStatus.Cancelled
                        && a.Date.Date == DateTime.Today)
                .ToList();
        }

        public IEnumerable<Appointment> GetPaginatedAppointments(int pageNumber, int pageSize, Guid doctorId)
        {
            return _dbContext.Set<Appointment>()
                .Include(d => d.Doctor)
                .Include(d => d.Donor)
                .Include(d => d.TransfusionCenter)
                .Where(d => d.DoctorId == doctorId)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IEnumerable<Appointment> GetDonorAppointments(Guid donorId)
        {
            return _dbContext.Set<Appointment>()
                .Include(d => d.Doctor)
                .Include(d => d.Donor)
                .Include(d => d.TransfusionCenter)
                .Where(a => a.DonorId == donorId)
                .ToList();
        }

        public long GetAppointmentsCount(Guid doctorId)
        {
            return _dbContext.Set<Appointment>()
                .Where(d => d.DoctorId == doctorId)
                .LongCount();
        }
    }
}
