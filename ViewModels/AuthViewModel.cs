using CourseProjectWPF.DB;
using CourseProjectWPF.Models;
using CourseProjectWPF.Views;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CourseProjectWPF.ViewModels
{
    class AuthViewModel : ViewModelBase
    {
        public string Login { get; set; }
        public string Password { get; set; }

        private string errorMes;
        public string ErrorMes
        {
            get { return errorMes; }
            set
            {
                this.errorMes = value;
                RaisePropertiesChanged(nameof(ErrorMes));
            }
        }

        public string Error => throw new NotImplementedException();

        public AuthViewModel()
        {

        }

        public void Close()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }

        public ICommand close => new DelegateCommand(Close);
        public ICommand registration => new DelegateCommand(Reg);

        public void Reg()
        {
            RegistrView r = new RegistrView();
            r.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            r.Show();
            Close();
        }

        public ICommand auth => new DelegateCommand(Auth);

        public string Hash(string input)
        {
            byte[] hash = Encoding.ASCII.GetBytes(input);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashenc = md5.ComputeHash(hash);
            string output = "";
            foreach (var b in hashenc)
            {
                output += b.ToString("x2");
            }
            return output;
        }

        public void Auth()
        {
            User IsAdmin = null;
            ErrorMes = "";
            User IsUser = null;

            if (Login == null || Password == null || Login == String.Empty || Password == String.Empty)
            {
                ErrorMes = "Заполните поля";
            }
            else
            {
                using (MyDbContext db = new MyDbContext())
                {                                      
                    string Pass = DB.DB.Hash(Password);
                    IsUser = db.Users.Where(b => b.Login == Login && b.Password == Pass).FirstOrDefault();
                    IsAdmin = db.Users.Where(b => b.Login == Login && b.Password == Pass && b.IsAdmin == true).FirstOrDefault();
                    if (IsUser != null || IsAdmin != null) 
                    {                       
                        if (IsAdmin != null)
                        {
                            MainAdminWindow sp = new MainAdminWindow();
                            sp.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            sp.Show();
                            Close();
                        }
                        else
                        {
                            MainClientWindow sp = new MainClientWindow();
                            sp.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            sp.Show();
                            Close();
                        }
                    }
                    else
                    {
                        ErrorMes = "Некорректные данные";
                    }                                                                    
                }
            }
        }
    }
}