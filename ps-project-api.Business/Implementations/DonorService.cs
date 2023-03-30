using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ps_project_api.Business.Interfaces;
using ps_project_api.Common.Implementations;
using ps_project_api.Common.Interfaces;
using ps_project_api.DAL;
using ps_project_api.DAL.Entities;
using ps_project_api.Models.DonorDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_project_api.Business.Implementations
{
    public class DonorService : IDonorService
    {
        private readonly TransfusionCenterContext _dbContext;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public DonorService(TransfusionCenterContext dbContext, IAuthService authService, IMapper mapper) 
        {
            _dbContext = dbContext;
            _authService = authService;
            _mapper = mapper;
        }

        public string Register(Donor donor)
        {
            var existingDonor = _dbContext.Set<Donor>()
                .Include(u => u.User)
                .Where(d => d.User.Email == donor.User.Email)
                .FirstOrDefault();

            if (existingDonor == null)
            {
                donor.User.Salt = _authService.GetSalt(16);
                donor.User.Password = _authService.Encrypt(donor.User.Password + donor.User.Salt);
                donor.BloodTypeId = 1;

                _dbContext.Add(donor);

                _dbContext.SaveChanges();

                return _authService.GenerateJwtToken(donor.Id.ToString(), donor.User.AccountTypeId);
            }
            else throw new ArgumentException("E-mail is already used.");
        }

        public Donor Update(DonorUpdateDTO donor)
        {
            var existingDonor = _dbContext.Set<Donor>()
                .Include(e => e.User)
                .Where(a => a.Id == donor.Id)
                .FirstOrDefault();

            _mapper.Map(donor, existingDonor);

            if (donor.Password != "")
            {
                existingDonor.User.Password = _authService.Encrypt(existingDonor?.User.Salt + donor.Password);
            }

            _dbContext.SaveChanges();

            return existingDonor;
        }

        public Donor GetDonorById(Guid donorId)
        {
            return _dbContext.Set<Donor>()
                .Include(e => e.User)
                .Where(d => d.Id == donorId)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public void Remove(Guid donorId)
        {
            var existingDonor = _dbContext.Set<Donor>()
                .Where(d => d.Id == donorId)
                .FirstOrDefault();

            _dbContext.Remove(existingDonor);

            _dbContext.SaveChanges(true);
        }
    }
}
