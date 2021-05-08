using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectWPF.Models
{
    public class Schedule
    {
    //    [ForeignKey("Doctor")]
        public int Id { get; set; }
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }


        //public virtual Doctor Doctor { get; set; }
        //public virtual MedCard MedCard { get; set; }
        //public virtual List<Recording> Recordings { get; set; }
    }
}
