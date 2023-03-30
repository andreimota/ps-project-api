using ps_project_api.Business.UpdateModels;
using ps_project_api.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_project_api.Business.Interfaces
{
    public interface IDoctorService
    {
        IEnumerable<Doctor> GetAll();
        Doctor Register(Doctor doctor);
        Doctor Update(DoctorUpdateDTO doctor);
        Doctor GetDoctorById(Guid doctorId);
        void Remove(Guid doctorId);
    }
}
