namespace ps_project_api.Api.Models.DonorDTOs
{
    public class DonorRegisterDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int BloodTypeId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
