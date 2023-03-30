using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ps_project_api.Business.Interfaces;
using ps_project_api.Business.UpdateModels;
using ps_project_api.Common.Interfaces;
using ps_project_api.DAL;
using ps_project_api.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_project_api.Business.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly TransfusionCenterContext _dbContext;

        public DoctorService(TransfusionCenterContext dbContext, IAuthService authService, IMapper mapper)
        {
            _mapper = mapper;
            _authService = authService;
            _dbContext = dbContext;
        }

        public Doctor Register(Doctor doctor)
        {
            var existingDoctor = _dbContext.Set<Doctor>()
                .Include(u => u.User)
                .Where(d => d.User.Email == doctor.User.Email)
                .FirstOrDefault();

            if (existingDoctor == null)
            {
                doctor.User.Salt = _authService.GetSalt(16);
                doctor.User.Password = _authService.Encrypt(doctor.User.Password + doctor.User.Salt);

                _dbContext.Add(doctor);
                _dbContext.SaveChanges();
                return doctor;
            }
            else throw new ArgumentException("E-mail is already taken.");
        }

        public Doctor Update(DoctorUpdateDTO doctor)
        {
            var existingDoctor = _dbContext.Set<Doctor>()
                .Include(e => e.User)
                .Where(e => e.Id == doctor.Id)
                .FirstOrDefault();

            _mapper.Map(doctor, existingDoctor);

            if (doctor.Password != "")
            {
                existingDoctor.User.Password = _authService.Encrypt(existingDoctor?.User.Salt + doctor.Password);
            }

            _dbContext.SaveChanges();

            return existingDoctor;
        }

        public Doctor GetDoctorById(Guid doctorId)
        {
            return _dbContext.Set<Doctor>()
                .Include(e => e.User)
                .Where(d => d.Id == doctorId)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _dbContext.Set<Doctor>()
                .Include(entity => entity.User)
                .ToList();
        }

        public void Remove(Guid doctorId)
        {
            var existingDoctor = _dbContext.Set<Doctor>()
                .Where(d => d.Id == doctorId)
                .FirstOrDefault();

            _dbContext.Remove(existingDoctor);

            _dbContext.SaveChanges(true);
        }
    }
}
