namespace ps_project_api.Models.DonorDTOs
{
    public class DonorViewDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int BloodTypeId { get; set; }
    }
}
