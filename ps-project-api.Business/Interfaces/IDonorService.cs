using ps_project_api.DAL.Entities;
using ps_project_api.Models.DonorDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_project_api.Business.Interfaces
{
    public interface IDonorService
    {
        string Register(Donor donor);
        Donor GetDonorById(Guid donorId);
        Donor Update(DonorUpdateDTO donor);
        void Remove(Guid donorId);
    }
}
