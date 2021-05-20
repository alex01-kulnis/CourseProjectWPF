using CourseProjectWPF.DB;
using CourseProjectWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectWPF.Repositories
{
    class EFUserRepository : IUserRepository
    {
        private MyDbContext context;

        public EFUserRepository()
        {
            context = new MyDbContext();
        }

        public User getByLogin(string login)
        {
            return context.Users.FirstOrDefault(x => x.Login == login);
        }

        public User getByPs(string ps)
        {
            return context.Users.FirstOrDefault(x => x.Password == ps);
        }

        public User getUser(string login,string ps)
        {
            return context.Users.Where(b => b.Login == login && b.Password == ps).FirstOrDefault();
        }

        public User getAdmin(string login, string ps)
        {
            return context.Users.Where(b => b.Login == login && b.Password == ps && b.IsAdmin == true).FirstOrDefault();
        }
    }
}
