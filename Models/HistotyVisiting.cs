using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectWPF.Models
{
    public class HistotyVisiting
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
