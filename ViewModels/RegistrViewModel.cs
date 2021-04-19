using CourseProjectWPF.DB;
using CourseProjectWPF.Models;
using CourseProjectWPF.Views;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseProjectWPF.ViewModels
{
    class RegistrViewModel : ViewModelBase
    {
        


        
        User user;
        public RegistrViewModel()
        {

        }       
        
        private string _Gender;
        public string Gender
        {
            get { return _Gender; }
            set
            {
                if (value.Equals("Мужчина"))
                    this._Gender = "Мужчина";
                else if (value.Equals("Женщина"))
                    this._Gender = "Женщина";
                RaisePropertiesChanged(nameof(Gender));

            }
        }

        private string _Login;
        public string Login
        {
            get { return _Login; }
            set
            {
                this._Login = value;
                RaisePropertiesChanged(nameof(Login));
            }
        }

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set
            {
                this._Password = value;
                RaisePropertiesChanged(nameof(Password));
            }
        }

        private string _Firstname;
        public string FirstName
        {
            get { return _Firstname; }
            set
            {
                this._Firstname = value;
                RaisePropertiesChanged(nameof(FirstName));
            }
        }

        private string _Surname;
        public string Surname
        {
            get { return _Surname; }
            set
            {
                this._Surname = value;
                RaisePropertiesChanged(nameof(Surname));
            }
        }

        private DateTime _BDay;
        public DateTime BDay
        {
            get { return  _BDay; }
            set
            {
                _BDay = value; OnPropertyChanged("BDay");
            }
        }

        private DateTime _startDate = DateTime.Now;
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged("StartDate"); }
        }

        public new event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }

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

        public ICommand register => new DelegateCommand(RegisterCommand);

        public void RegisterCommand()
        {
            using (MyDbContext db = new MyDbContext())
            {                
                bool Registration = true;
                ErrorMes = "";
                try
                {
                    SqlParameter param = new SqlParameter("@Login", Login);
                    var users = db.Database.SqlQuery<User>("SELECT * FROM Users WHERE Login LIKE @Login", param);
                    foreach (var user in users)
                    {
                        if (user.Login == Login)
                        {
                            Registration = false;
                        }
                    }
                    if (Registration)
                    {
                        user = new User();
                        user.FirstName = FirstName;
                        user.Surname = Surname;
                        
                        user.BDay = BDay;
                        //user.BDay = DateTime.ParseExact(str, "dd.MM.yyyy",
                        //    System.Globalization.CultureInfo.InvariantCulture);
                        user.Gender = Gender;
                        user.Login = Login;
                        user.Password = Password;

                      
                        db.Users.Add(user);
                        db.SaveChanges();

                        AuthView t = new AuthView();
                        t.Show();
                        Close();
                    }
                    else
                    {
                        ErrorMes = "Такой логин уже существует";
                        Login = "";
                    }
                }
                catch (Exception ex) 
                { 
                }
            }
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
        public ICommand back => new DelegateCommand(Back);

        public void Back()
        {
            AuthView t = new AuthView();
            t.Show();
            Close();
        }   
    }
}