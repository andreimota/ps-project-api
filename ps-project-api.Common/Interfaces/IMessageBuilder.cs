using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_project_api.Common.Interfaces
{
    public interface IMessageBuilder<T> where T : class
    {
        IMessageBuilder<T> From(string email, string name);
        IMessageBuilder<T> Subject(string subject);
        IMessageBuilder<T> To(string email, string name);
        IMessageBuilder<T> Body(string body);
        T GetEmail();

        void reset();
    }
}
