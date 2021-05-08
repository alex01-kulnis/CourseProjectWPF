﻿using System;
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
        //public Recording(string FIO, string Doctor, string Cabinet)
        ////{
        ////    this.FIO = FIO;
        ////    this.Doctor = Doctor;
        ////    this.Cabinet = Cabinet;
        ////}
        public int Id { get; set; }
        public string FIO { get; set; }
        public string Doctor { get; set; }
        public string Cabinet { get; set; }
        public DateTime VisitDay { get; set; }
        public string VisitTime { get; set; }
        public int UserId { get; set; }
        public bool IsVisited { get; set; }


        public virtual User User { get; set; }
    }
}
