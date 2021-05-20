using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectWPF.Models
{
    public class Recording
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string Doctor { get; set; }
        public string Cabinet { get; set; }
        public DateTime VisitDay { get; set; }
        public string VisitTime { get; set; }        
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}