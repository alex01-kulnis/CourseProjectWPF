using CourseProjectWPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectWPF.Models
{
    public class User
    {
        public User()
        {
            Doctors = new List<Doctor>();
        }

        public int Id { get; set; }
        public int? RecordingId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTime BDay { get; set; }
        public string Gender { get; set; }
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
                                            
        public virtual MedCard MedCard { get; set; }
        public virtual List<Recording> Recordings { get; set; }
        public virtual List<HistoryVisiting> HistoryVisitings { get; set; }
        public virtual List<Doctor> Doctors { get; set; }
    }
}