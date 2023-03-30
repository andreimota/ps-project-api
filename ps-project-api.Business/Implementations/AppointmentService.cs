using Microsoft.EntityFrameworkCore;
using ps_project_api.Business.Interfaces;
using ps_project_api.DAL;
using ps_project_api.DAL.Entities;
using ps_project_api.DAL.Enums;
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

        public AppointmentService(TransfusionCenterContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Appointment BookAppointment(Appointment appointment)
        {
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

        public IEnumerable<Appointment> GetDonorAppointments(Guid donorId)
        {
            return _dbContext.Set<Appointment>()
                .Include(d => d.Doctor)
                .Include(d => d.Donor)
                .Include(d => d.TransfusionCenter)
                .Where(a => a.DonorId == donorId)
                .ToList();
        }
    }
}
