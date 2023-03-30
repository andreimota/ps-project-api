namespace ps_project_api.Api.Models.DoctorDTOs
{
    public class DoctorCreateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string SSN { get; set; }
        public string Password { get; set; }
        public Guid TransfusionCenterId { get; set; }
    }
}
