using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ps_project_api.Business.Interfaces;
using ps_project_api.Models.TransfusionCenterDTOs;


namespace ps_project_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransfusionCenterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITransfusionCenterService _transfusionCenterService;

        public TransfusionCenterController(IMapper mapper, ITransfusionCenterService transfusionCenterService)
        {
            _mapper = mapper;
            _transfusionCenterService = transfusionCenterService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var transfusionCenters = _transfusionCenterService.GetAll();

            return Ok(
                _mapper.Map<IEnumerable<TransfusionCenterViewDTO>>(transfusionCenters)
            );
        }
    }
}
