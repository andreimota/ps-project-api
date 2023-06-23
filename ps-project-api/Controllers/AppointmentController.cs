using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ps_project_api.Business.Interfaces;
using ps_project_api.DAL.Entities;
using ps_project_api.Models.AppointmentDTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ps_project_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IMapper _mapper;

        public AppointmentController(IAppointmentService appointmentService, IMapper mapper)
        {
            _appointmentService = appointmentService;
            _mapper = mapper;
        }

        [HttpGet("doctor/{id}")]
        public IActionResult GetDoctorAppointments(Guid id)
        {
            var appointments = _appointmentService.GetDoctorAppointments(id);

            return Ok(_mapper.Map<IEnumerable<AppointmentViewDTO>>(appointments));
        }

        [HttpGet("donor/{id}")]
        public IActionResult GetDonorAppointments(Guid id)
        {
            var appointments = _appointmentService.GetDonorAppointments(id);

            return Ok(_mapper.Map<IEnumerable<AppointmentViewDTO>>(appointments));
        }

        [HttpGet("doctor/{page}/{pageSize}/{id}")]
        public IActionResult GetPaginatedDoctorAppointments(int page, int pageSize, Guid id)
        {
            var appointments = _appointmentService.GetPaginatedAppointments(page, pageSize, id);

            return Ok(_mapper.Map<IEnumerable<AppointmentViewDTO>>(appointments));
        }

        [HttpGet("doctor/count/{id}")]
        public IActionResult GetAppointmentsCount(Guid id)
        {
            return Ok(_appointmentService.GetAppointmentsCount(id));
        }

        [HttpPost]
        public async Task<IActionResult> BookAppointment(AppointmentCreateDTO model)
        {
            var appointment = _mapper.Map<Appointment>(model);

            var response = await _appointmentService.BookAppointment(appointment);

            return Ok(response);
        }

        [HttpPut("{id}/{status}")]
        public IActionResult ChangeAppointmentStatus(Guid id, int status)
        {
            return Ok(_appointmentService.ChangeAppointmentStatus(id, status));
        }
    }
}
