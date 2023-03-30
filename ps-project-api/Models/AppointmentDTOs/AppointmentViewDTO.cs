using ps_project_api.DAL.Entities;

namespace ps_project_api.Models.AppointmentDTOs
{
    public class AppointmentViewDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Doctor { get; set; }
        public string Donor { get; set; }
        public string TransfusionCenter { get; set; }
        public int Status { get; set; }
    }
}
