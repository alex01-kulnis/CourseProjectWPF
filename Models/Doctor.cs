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
        [Key]
        public int Id { get; set; }       
        public string FIO { get; set; }       
        public string Post { get; set; }
        public string Cabinet { get; set; }

        public virtual Schedule Schedule { get; set; }
        //public virtual MedCard MedCard { get; set; }
        //public virtual List<Recording> Recordings { get; set; }
    }
}
