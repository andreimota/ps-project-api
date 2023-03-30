using Microsoft.EntityFrameworkCore;
using ps_project_api.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_project_api.DAL
{
    public class TransfusionCenterContext : DbContext
    {
        public TransfusionCenterContext(DbContextOptions<TransfusionCenterContext> options) : base(options)
        {

        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<TransfusionCenter> TransfusionCenters { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().ToTable("Doctor");
            modelBuilder.Entity<Donor>().ToTable("Donor");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Appointment>().ToTable("Appointment");
            modelBuilder.Entity<TransfusionCenter>().ToTable("TransfusionCenter");
            modelBuilder.Entity<Admin>().ToTable("Admin");
        }
    }
}
