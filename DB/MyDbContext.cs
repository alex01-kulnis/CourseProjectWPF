using CourseProjectWPF.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectWPF.DB
{
    public class MyDbContext: DbContext
    {
        public MyDbContext(): base("KP_DB")
        {

        }

        //public DbSet<Group> Groups { get; set; }
        //public DbSet<Song> Songs { get; set; }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MedCard> MedCards { get; set; }
        public DbSet<Recording> Recordings { get; set; }
        public DbSet<HistotyVisiting> HistotyVisitings { get; set; }
        //public DbSet<PacientIdAndDoctorId> PacientIdAndDoctorIds { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {       
        }
    }
}