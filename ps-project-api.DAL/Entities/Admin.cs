using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_project_api.DAL.Entities
{
    public class Admin
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
