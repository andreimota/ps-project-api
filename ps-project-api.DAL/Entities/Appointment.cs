namespace ps_project_api.DAL.Entities
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid DonorId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid TransfusionCenterId { get; set; }
        public Doctor Doctor { get; set; }
        public Donor Donor { get; set; }
        public TransfusionCenter TransfusionCenter { get; set; }
        public int Status { get; set; }
    }
}
