using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_project_api.DAL.Entities
{
    public class Donor
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int BloodTypeId { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

    }
}
