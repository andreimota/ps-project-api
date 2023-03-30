using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ps_project_api.DAL.Entities
{
    public class Doctor
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("TransfusionCenter")]
        public Guid TransfusionCenterId { get; set; }
        public TransfusionCenter TransfusionCenter { get; set; }

        [InverseProperty("Doctor")]
        public ICollection<Appointment> Appointments { get; set; }
    }
}
