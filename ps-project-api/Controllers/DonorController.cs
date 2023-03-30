using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ps_project_api.Api.Models.DonorDTOs;
using ps_project_api.Business.Implementations;
using ps_project_api.Business.Interfaces;
using ps_project_api.DAL.Entities;
using ps_project_api.Models.DonorDTOs;

namespace ps_project_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorController : ControllerBase
    {
        private readonly IDonorService _donorService;
        private readonly IMapper _mapper;

        public DonorController(IDonorService donorService, IMapper mapper)
        {
            _donorService = donorService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public IActionResult Register(DonorRegisterDTO donor)
        {
            var mappedDonor = _mapper.Map<Donor>(donor);

            var response = _donorService.Register(mappedDonor);

            return Ok(response);
        }

        [HttpPut]
        public IActionResult Update(DonorUpdateDTO donor)
        {
            var response = _donorService.Update(donor);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var donor = _donorService.GetDonorById(id);

            return Ok(_mapper.Map<DonorViewDTO>(donor));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _donorService.Remove(id);

            return Ok();
        }
    }
}
