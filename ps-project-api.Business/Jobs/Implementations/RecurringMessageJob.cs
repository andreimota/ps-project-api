using Microsoft.EntityFrameworkCore;
using ps_project_api.Business.Jobs.Interfaces;
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

namespace ps_project_api.Business.Jobs.Implementations
{
    public class RecurringMessageJob : IRecurringMessageJob
    {
        private readonly TransfusionCenterContext _dbContext;
        private readonly IEmailSender _emailSender;

        public RecurringMessageJob(TransfusionCenterContext dbContext, IEmailSender emailSender) 
        {
            _dbContext = dbContext;
            _emailSender = emailSender;
        }

        public void SendAppointmentReminders()
        {
            var nextDayAppointments = _dbContext.Set<Appointment>()
                .Where(a => a.Status == (int)AppointmentStatus.Pending && a.Date.Date == DateTime.Now.Date.AddDays(1))
                .Include(d => d.Donor)
                    .ThenInclude(u => u.User)
                .ToList();

            foreach (var appointment in nextDayAppointments)
            {
                _emailSender.SendAppointmentReminderEmail(appointment.Donor.User.Email, appointment.Donor.FirstName + " " + appointment.Donor.LastName);
            }
        }
    }
}
