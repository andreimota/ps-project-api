using ps_project_api.Common.Interfaces;
using ps_project_api.Common.Models.Email;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_project_api.Common.Implementations
{
    public class EmailBuilder : IMessageBuilder<SendGridMessage>
    {
        private EmailModel _email;

        public EmailBuilder()
        {
            _email = new EmailModel();
        }

        public IMessageBuilder<SendGridMessage> Body(string body)
        {
            _email.Body = body;

            return this;
        }

        public IMessageBuilder<SendGridMessage> From(string email, string name)
        {
            _email.From = new EmailAddress(email, name);

            return this;
        }

        public IMessageBuilder<SendGridMessage> Subject(string subject)
        {
            _email.Subject = subject;

            return this;
        }

        public IMessageBuilder<SendGridMessage> To(string email, string name)
        {
            _email.To = new EmailAddress(email, name);

            return this;
        }

        public SendGridMessage GetEmail()
        {
            var message =  MailHelper.CreateSingleEmail(
                _email.From,
                _email.To,
                _email.Subject,
                _email.Body,
                _email.Body
            );

            this.reset();

            return message;
        }

        public void reset()
        {
            this._email = new EmailModel();
        }
    }
}
