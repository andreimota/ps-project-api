namespace ps_project_api.Api.Models.DoctorDTOs
{
    public class DoctorViewDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string SSN { get; set; }
        public Guid TransfusionCenterId { get; set; }
    }
}
