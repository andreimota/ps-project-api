using ps_project_api.Business.Interfaces;
using ps_project_api.Common.Interfaces;
using ps_project_api.DAL;
using ps_project_api.DAL.Entities;
using ps_project_api.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_project_api.Business.Implementations
{
    public class UserService : IUserService
    {
        private readonly TransfusionCenterContext _dbContext;
        private readonly IAuthService _authService;

        public UserService(TransfusionCenterContext dbContext, IAuthService authService) 
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public string Authenticate(User user)
        {
            var existingUser = _dbContext.Set<User>().Where(u => u.Email == user.Email).FirstOrDefault();
            var encryptedPassword = _authService.Encrypt(user.Password + existingUser?.Salt);

            if (existingUser == null || encryptedPassword != existingUser?.Password) 
                throw new NullReferenceException("Invalid username or password");

            switch(existingUser.AccountTypeId)
            {
                case (int)AccountType.DONOR:
                    var donor = _dbContext.Set<Donor>().Where(u => u.UserId == existingUser.Id).FirstOrDefault();
                    return _authService.GenerateJwtToken(donor.Id.ToString(), existingUser.AccountTypeId);

                case (int)AccountType.DOCTOR:
                    var doctor = _dbContext.Set<Doctor>().Where(u => u.UserId == existingUser.Id).FirstOrDefault();
                    return _authService.GenerateJwtToken(doctor.Id.ToString(), existingUser.AccountTypeId);

                case (int)AccountType.ADMIN:
                    var admin = _dbContext.Set<Admin>().Where(u => u.UserId == existingUser.Id).FirstOrDefault();
                    return _authService.GenerateJwtToken(admin.Id.ToString(), existingUser.AccountTypeId);

                default:
                    throw new ArgumentException("Invalid account type.");
            }
        }
    }
}
