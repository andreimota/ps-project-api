using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_project_api.DAL.Entities
{
    public class TransfusionCenter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string WorkingHours { get; set; }
        public int DonationsLimit { get; set; }

        [InverseProperty("TransfusionCenter")]
        public ICollection<Doctor> Doctors { get; set; }
    }
}
