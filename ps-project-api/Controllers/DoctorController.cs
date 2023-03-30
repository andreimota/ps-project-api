using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ps_project_api.Api.Models.DoctorDTOs;
using ps_project_api.Business.Interfaces;
using ps_project_api.Business.UpdateModels;
using ps_project_api.DAL.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProiectPS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;

        public DoctorController(IDoctorService doctorService, IMapper mapper)
        {
            _doctorService = doctorService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var doctors = _doctorService.GetAll();

            return Ok(_mapper.Map<IEnumerable<DoctorViewDTO>>(doctors));
        }

        [HttpPost]
        public IActionResult Register(DoctorCreateDTO newDoctor)
        {
            var mappedDoctor = _mapper.Map<Doctor>(newDoctor);

            var response = _doctorService.Register(mappedDoctor);

            return Ok(_mapper.Map<DoctorViewDTO>(response));
        }

        [HttpPut]
        public IActionResult Update(DoctorUpdateDTO doctor)
        {
            var response = _doctorService.Update(doctor);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _doctorService.Remove(id);

            return Ok();
        }
    }
}
