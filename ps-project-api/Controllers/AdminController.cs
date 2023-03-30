using Microsoft.AspNetCore.Mvc;
using ps_project_api.Common.Interfaces;
using ps_project_api.DAL;
using ps_project_api.DAL.Entities;
using ps_project_api.Models.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ps_project_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly TransfusionCenterContext _dbContext;

        public AdminController(IAuthService authService, TransfusionCenterContext dbContext)
        {
            _authService = authService;
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult CreateAdmin(AdminRegisterDTO model)
        {
            var admin = new Admin();
            admin.User = new User();
            admin.User.Email = model.Email;
            admin.User.Salt = _authService.GetSalt(16);
            admin.User.Password = _authService.Encrypt(model.Password + admin.User.Salt);
            admin.User.AccountTypeId = 1;

            _dbContext.Add(admin);

            _dbContext.SaveChanges();

            return Ok("Successfully created an administrator.");
        }
    }
}
