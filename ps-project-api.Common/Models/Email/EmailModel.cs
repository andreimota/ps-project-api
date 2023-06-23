using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_project_api.Common.Models.Email
{
    public class EmailModel
    {
        public EmailAddress From { get; set; }
        public string Subject { get; set; }
        public EmailAddress To { get; set; }
        public string Body { get; set; }
    }
}
