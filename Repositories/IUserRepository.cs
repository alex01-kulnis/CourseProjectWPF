using CourseProjectWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectWPF.Repositories
{
    public interface IUserRepository
    { 
        User getByLogin(string name);
        User getByPs(string name);
        User getUser(string login,string ps);
        User getAdmin(string login, string ps);
    }
}