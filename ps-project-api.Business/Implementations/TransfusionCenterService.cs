using Microsoft.Identity.Client;
using ps_project_api.Business.Interfaces;
using ps_project_api.DAL;
using ps_project_api.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_project_api.Business.Implementations
{
    public class TransfusionCenterService : ITransfusionCenterService
    {
        private readonly TransfusionCenterContext _dbContext;

        public TransfusionCenterService(TransfusionCenterContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<TransfusionCenter> GetAll()
        {
            return _dbContext.Set<TransfusionCenter>().ToList();
        }
    }
}
