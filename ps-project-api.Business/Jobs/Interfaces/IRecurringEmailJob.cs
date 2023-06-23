using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_project_api.Business.Jobs.Interfaces
{
    public interface IRecurringMessageJob
    {
        void SendAppointmentReminders();
    }
}
