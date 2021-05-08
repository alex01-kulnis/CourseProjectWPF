using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectWPF.Models
{
    public class Doctor
    {
        public Doctor()
        {
            Users = new List<User>();
        }
        
        public int Id { get; set; }       
        public string FIO { get; set; }       
        public string Post { get; set; }
        public string Cabinet { get; set; }
        public DateTime Evenday_start  { get; set; }
        public DateTime Evenday_end { get; set; }
        public DateTime Oddday_start { get; set; }
        public DateTime Oddday_end { get; set; }

        //public virtual Schedule Schedule { get; set; }

        //public virtual MedCard MedCard { get; set; }
        //public virtual List<Recording> Recordings { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
