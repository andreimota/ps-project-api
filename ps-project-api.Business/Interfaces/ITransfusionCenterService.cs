using ps_project_api.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_project_api.Business.Interfaces
{
    public interface ITransfusionCenterService
    {
        IEnumerable<TransfusionCenter> GetAll();
    }
}
