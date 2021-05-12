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
        public string Patronymic { get; set; }
        public string Gender { get; set; } 
        public DateTime BDay { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int House { get; set; }
        public string Housing { get; set; }
        public string Flat { get; set; }
        public string Image { get; set; }
 
        public virtual User User { get; set; }
    }
}