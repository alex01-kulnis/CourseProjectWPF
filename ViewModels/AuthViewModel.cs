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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CourseProjectWPF.ViewModels
{
    class AuthViewModel : ViewModelBase
    {
        public string str { get; set; }
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
            Close();
            Thread myThread = new Thread(new ThreadStart(DB.DB.ShowLoader));
            myThread.SetApartmentState(ApartmentState.STA);
            myThread.Start();
            Thread.Sleep(1000);
            myThread.Abort();
            r.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            r.Show();            
        }

        public ICommand auth => new DelegateCommand(Auth);

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
                    try
                    {
                        
                        string Pass = DB.DB.Hash(Password);
                        IsUser = db.Users.Where(b => b.Login == Login && b.Password == Pass).FirstOrDefault();
                        IsAdmin = db.Users.Where(b => b.Login == Login && b.Password == Pass && b.IsAdmin == true).FirstOrDefault();
                        if (IsUser != null || IsAdmin != null)
                        {
                            App.CurrentUser = IsUser;
                            if (IsAdmin != null)
                            {
                                MainAdminWindow sp = new MainAdminWindow();
                                Close();
                                Thread myThread = new Thread(new ThreadStart(DB.DB.ShowLoader));
                                myThread.SetApartmentState(ApartmentState.STA);
                                myThread.Start();
                                Thread.Sleep(1000);
                                myThread.Abort();
                                sp.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                                sp.Show();
                            }
                            else
                            {                                
                                MainClientWindow sp = new MainClientWindow();
                                Close();
                                Thread myThread = new Thread(new ThreadStart(DB.DB.ShowLoader));
                                myThread.SetApartmentState(ApartmentState.STA);
                                myThread.Start();
                                Thread.Sleep(1000);
                                myThread.Abort();
                                sp.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                                sp.Show();
                            }
                        }
                        else
                        {
                            ErrorMes = "Некорректные данные";
                        }
                    }
                    catch (Exception ex) { }                                                                 
                }
            }
        }
    }
}