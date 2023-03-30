namespace ps_project_api.Models.AppointmentDTOs
{
    public class AppointmentCreateDTO
    {
        public DateTime Date { get; set; }
        public Guid DonorId { get; set; }
        public Guid TransfusionCenterId { get; set; }
    }
}
