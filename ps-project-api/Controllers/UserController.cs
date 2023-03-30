using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ps_project_api.Business.Interfaces;
using ps_project_api.DAL.Entities;
using ps_project_api.Models.User;

namespace ps_project_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateModel authModel)
        {
            try
            {
                var user = _mapper.Map<User>(authModel);

                var response = _userService.Authenticate(user);

                return Ok(response);
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
