using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectWPF.Models
{
    public class Recording
    {
        public string Id { get; set; }
        public string FIO { get; set; }
        public string Doctor { get; set; }
        public DateTime VisitDay { get; set; }
        public DateTime VisitTime { get; set; }


        public virtual User User { get; set; }
    }
}
