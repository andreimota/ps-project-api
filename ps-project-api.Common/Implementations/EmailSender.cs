using Microsoft.Extensions.Configuration;
using ps_project_api.Common.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_project_api.Common.Implementations
{
    public class EmailSender : IEmailSender
    {
        private IConfiguration _config;

        private IMessageBuilder<SendGridMessage> _emailBuilder = new EmailBuilder();

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendMessage(SendGridMessage message)
        {
            var apiKey = _config.GetSection("ApiKeys")["SendGrid"];

            var client = new SendGridClient(apiKey);

            var response = await client.SendEmailAsync(message);
        }

        public async Task SendAppointmentConfirmationEmail(string to, string recipientName, string appointmentDate)
        {
            var message = _emailBuilder
                .To(to, recipientName)
                .From(_config.GetSection("Sender")["SendGrid"], "")
                .Subject("Your appointment has beed approved")
                .Body(
                    @$"<h3>Hello, {recipientName}</h3>
                       <p>Your appointment for the date {appointmentDate} has been registered successfully.</p>"
                )
                .GetEmail();

            await SendMessage(message);
        }

        public async Task SendAppointmentReminderEmail(string to, string recipientName)
        {
            var message =  _emailBuilder
                .To(to, recipientName)
                .From(_config.GetSection("Sender")["SendGrid"], "")
                .Subject("You have an upcoming appointment")
                .Body(
                    $@"<h3>Hello, {recipientName}</h3>
                       <p>Don't forget your appointment for tomorrow</p>"
                )
                .GetEmail();

            await SendMessage(message);
        }
    }
}
