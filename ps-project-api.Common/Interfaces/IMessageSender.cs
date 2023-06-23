using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_project_api.Common.Interfaces
{
    public interface IEmailSender
    {
        Task SendMessage(SendGridMessage message);
        Task SendAppointmentConfirmationEmail(string to, string recipientName, string appointmentDate);
        Task SendAppointmentReminderEmail(string to, string recipientName);
    }
}
