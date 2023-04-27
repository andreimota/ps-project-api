using ps_project_api.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_project_api.Business.Interfaces
{
    public interface IAppointmentService
    {
        IEnumerable<Appointment> GetDonorAppointments(Guid donorId);
        IEnumerable<Appointment> GetDoctorAppointments(Guid doctorId);
        Appointment BookAppointment(Appointment appointment);
        Appointment ChangeAppointmentStatus(Guid appointmentId, int status);
        IEnumerable<Appointment> GetPaginatedAppointments(int pageNumber, int pageSize, Guid doctorId);
        long GetAppointmentsCount(Guid doctorId);
    }
}
