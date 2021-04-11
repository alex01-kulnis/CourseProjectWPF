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

        public DbSet<User> Users { get; set; }
        public DbSet<MedCard> MedCards { get; set; }
        public DbSet<Recording> Recordings { get; set; }
    }
}
