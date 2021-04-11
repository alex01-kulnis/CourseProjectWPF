using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectWPF.Models
{
    public class MedCard
    {
        [ForeignKey("User")]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BDay { get; set; }
        public string Gender { get; set; } //0-male, 1 - female

        public virtual User User { get; set; }
    }
}
