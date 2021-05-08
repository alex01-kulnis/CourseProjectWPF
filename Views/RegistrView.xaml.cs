using CourseProjectWPF.DB;
using CourseProjectWPF.Models;
using CourseProjectWPF.ViewModels;
using System;
using BespokeFusion;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace CourseProjectWPF.Views
{
    /// <summary>
    /// Логика взаимодействия для RegistrView.xaml
    /// </summary>
    public partial class RegistrView : Window
    {
        RegistrViewModel reg = new RegistrViewModel();
        User user;
        public RegistrView()
        {
            InitializeComponent();
            DataContext = reg;
            Reg.IsEnabled = false;                        
        }

        #region Button reg 
        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            using (MyDbContext db = new MyDbContext())
            {               
                ErrorMessage.Text = "";
                bool Registration = true;
                
                try
                {
                    string str = Bday_textbox.ToString();
                    int a = Convert.ToInt32(str = str.Substring(6, 4));
                    if (a < DateTime.Now.Year-13  && a >= 1900)
                    {
                        SqlParameter param = new SqlParameter("@Login", login_textbox.Text);
                        var users = db.Database.SqlQuery<User>("SELECT * FROM Users WHERE Login LIKE @Login", param);
                        foreach (var user in users)
                        {
                            if (user.Login == login_textbox.Text)
                            {
                                Registration = false;
                            }
                        }
                        if (Registration)
                        {
                            user = new User();
                            user.FirstName = Name_textbox.Text.Trim();
                            user.Surname = Surname_textbox.Text.Trim();
                            //user.BDay = Bday_textbox.Text.Trim();
                            user.BDay = DateTime.ParseExact(Bday_textbox.Text, "dd.MM.yyyy",
                                System.Globalization.CultureInfo.InvariantCulture);
                            user.Gender = Gender.Text.Trim();
                            user.Login = login_textbox.Text.Trim();
                            string Pass = DB.DB.Hash(password_box.Password);                            
                            user.Password = Pass;
                            db.Users.Add(user);
                            db.SaveChanges();                            
                            MaterialMessageBox.Show("Регистрация прошла успешна","Уведомление");       
                            
                            AuthView t = new AuthView();
                            Close();
                            Thread myThread = new Thread(new ThreadStart(DB.DB.ShowLoader));
                            myThread.SetApartmentState(ApartmentState.STA);
                            myThread.Start();
                            Thread.Sleep(1000);
                            myThread.Abort();
                            t.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            t.Show();
                        }
                        else
                        {
                            ErrorMessage.Text = "Такой логин уже существует";
                        }
                    }
                    else
                    {                        
                        ErrorMessage.Text = "Некорректная дата";                        
                    }                        
                }
                catch (Exception ex) { MaterialMessageBox.Show("Ошибка при подключении к базе данных", "Уведомление"); }
            }
        }
        #endregion

        #region Validation    
        private void onlyLetters(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[a-zA-Zа-яА-Я]{1,}$");
            e.Handled = !regex.IsMatch(e.Text);

            //char symbol = e.Text.FirstOrDefault();
            //e.Handled = !Char.IsLetter(symbol);
        }

        private void Name_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(Name_textbox.Text))
                Reg.IsEnabled = false;
            else if (!String.IsNullOrEmpty(Name_textbox.Text) &&
                !String.IsNullOrEmpty(Surname_textbox.Text) &&
                !String.IsNullOrEmpty(Bday_textbox.Text) &&
                !String.IsNullOrEmpty(login_textbox.Text)  &&
                password_box.Password.Length >= 8 &&
                !String.IsNullOrEmpty(Gender.Text))
                Reg.IsEnabled = true;
        }

        private void Surname_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(Surname_textbox.Text))
                Reg.IsEnabled = false;
            else if (!String.IsNullOrEmpty(Name_textbox.Text) &&
                !String.IsNullOrEmpty(Surname_textbox.Text) &&
                !String.IsNullOrEmpty(Bday_textbox.Text) &&
                !String.IsNullOrEmpty(login_textbox.Text)  &&
                password_box.Password.Length >= 8 &&
                !String.IsNullOrEmpty(Gender.Text))
                Reg.IsEnabled = true;
        }

        private void Bday_textbox_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(Bday_textbox.Text))
                Reg.IsEnabled = false;
            else if (!String.IsNullOrEmpty(Name_textbox.Text) &&
                !String.IsNullOrEmpty(Surname_textbox.Text) &&
                !String.IsNullOrEmpty(Bday_textbox.Text) &&
                !String.IsNullOrEmpty(login_textbox.Text) &&
                password_box.Password.Length >= 8 &&
                !String.IsNullOrEmpty(Gender.Text))
                Reg.IsEnabled = true;
        }

        private void login_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(login_textbox.Text))
                Reg.IsEnabled = false;
            else if (!String.IsNullOrEmpty(Name_textbox.Text) &&
                !String.IsNullOrEmpty(Surname_textbox.Text) &&
                !String.IsNullOrEmpty(Bday_textbox.Text) &&
                !String.IsNullOrEmpty(login_textbox.Text) &&
                password_box.Password.Length >= 8 &&
                !String.IsNullOrEmpty(Gender.Text))
                Reg.IsEnabled = true;
        }

        private void password_box_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (password_box.Password.Length < 8)
                Reg.IsEnabled = false;
            else if (!String.IsNullOrEmpty(Name_textbox.Text) &&
                !String.IsNullOrEmpty(Surname_textbox.Text) &&
                !String.IsNullOrEmpty(Bday_textbox.Text) &&
                !String.IsNullOrEmpty(login_textbox.Text) &&
                password_box.Password.Length >= 8 &&
                !String.IsNullOrEmpty(Gender.Text))
                Reg.IsEnabled = true;
        }
        #endregion
    }
}