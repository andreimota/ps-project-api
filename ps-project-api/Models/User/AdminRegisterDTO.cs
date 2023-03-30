using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ps_project_api.Models.User
{
    public class AdminRegisterDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
