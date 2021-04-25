using BespokeFusion;
using CourseProjectWPF.DB;
using CourseProjectWPF.Models;
using CourseProjectWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для AdminChangeClient.xaml
    /// </summary>
    public partial class AdminChangeClient : Window
    {
        bool PasswrodChange = false;
        int i = 0;
        AdminChangeClientModel a = new AdminChangeClientModel();
        User userr;

        public AdminChangeClient()
        {
            InitializeComponent();
            DataContext = a;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;            
        }

        //display information about selected user
        public AdminChangeClient(User user) : this()
        {
            userr = user;
            Name_textbox.Text = user.FirstName;
            Surname_textbox.Text = user.Surname;
            Bday_textbox.Text = user.BDay.ToShortDateString();
            Gender.Text = userr.Gender;
            login_textbox.Text = user.Login;
            password_box.Password = user.Password;
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            using (MyDbContext db = new MyDbContext())
            {
                User IsUser = null;
                //User IsAdmin = null;
                ErrorMessage.Text = "";
                bool Change = true;

                try
                {
                    string str = Bday_textbox.ToString();
                    int a = Convert.ToInt32(str = str.Substring(6, 4));
                    if (a < 2006 && a >= 1900)
                    {
                        IsUser = db.Users.Where(b => b.Login == login_textbox.Text && b.IsAdmin == false).FirstOrDefault();
                        //IsAdmin = db.Users.Where(b => b.Login == login_textbox.Text && b.IsAdmin == true ).FirstOrDefault();
                        if (IsUser != null /*|| IsAdmin == null*/)
                        {
                            db.Users.Find(userr.Id).FirstName = Name_textbox.Text.Trim();
                            db.Users.Find(userr.Id).Surname = Surname_textbox.Text.Trim();
                            db.Users.Find(userr.Id).BDay = DateTime.ParseExact(Bday_textbox.Text, "dd.MM.yyyy",
                                System.Globalization.CultureInfo.InvariantCulture);
                            db.Users.Find(userr.Id).Gender = Gender.Text.Trim();
                            db.Users.Find(userr.Id).Login = login_textbox.Text.Trim();
                            if (PasswrodChange)
                            {
                                string Pass = DB.DB.Hash(password_box.Password);
                                db.Users.Find(userr.Id).Password = Pass;
                            }
                            else
                            {
                                db.Users.Find(userr.Id).Password = password_box.Password;
                            }
                            db.SaveChanges();

                            MaterialMessageBox.Show("Данные изменены", "Уведомление");
                            DialogResult = true;
                            this.Close();                            
                        }
                        else
                        {
                            SqlParameter param = new SqlParameter("@Login", login_textbox.Text);
                            var users = db.Database.SqlQuery<User>("SELECT * FROM Users WHERE Login LIKE @Login", param);
                            foreach (var user in users)
                            {
                                if (user.Login == login_textbox.Text)
                                {
                                    Change = false;
                                }
                            }
                            if (Change)
                            {
                                db.Users.Find(userr.Id).FirstName = Name_textbox.Text.Trim();
                                db.Users.Find(userr.Id).Surname = Surname_textbox.Text.Trim();
                                db.Users.Find(userr.Id).BDay = DateTime.ParseExact(Bday_textbox.Text, "dd.MM.yyyy",
                                    System.Globalization.CultureInfo.InvariantCulture);
                                db.Users.Find(userr.Id).Gender = Gender.Text.Trim();
                                db.Users.Find(userr.Id).Login = login_textbox.Text.Trim();
                                if (PasswrodChange)
                                {
                                    string Pass = DB.DB.Hash(password_box.Password);
                                    db.Users.Find(userr.Id).Password = Pass;
                                }
                                else
                                {
                                    db.Users.Find(userr.Id).Password = password_box.Password;
                                }
                                db.SaveChanges();                                
                                MaterialMessageBox.Show("Данные изменены", "Уведомление");
                                DialogResult = true;
                                this.Close();
                            }
                            else
                            {
                                ErrorMessage.Text = "Такой логин уже существует";
                            }
                        }                        
                    }
                    else
                    {
                        ErrorMessage.Text = "Некорректная дата";
                    }
                }                
                catch  
                {
                    ErrorMessage.Text = "Такой логин уже существует";
                }
            }
        }

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
                edit.IsEnabled = false;
            else if (!String.IsNullOrEmpty(Name_textbox.Text) &&
                !String.IsNullOrEmpty(Surname_textbox.Text) &&
                !String.IsNullOrEmpty(Bday_textbox.Text) &&
                !String.IsNullOrEmpty(login_textbox.Text) &&
                password_box.Password.Length >= 8 &&
                !String.IsNullOrEmpty(Gender.Text))
                edit.IsEnabled = true;
        }

        private void Surname_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(Surname_textbox.Text))
                edit.IsEnabled = false;
            else if (!String.IsNullOrEmpty(Name_textbox.Text) &&
                !String.IsNullOrEmpty(Surname_textbox.Text) &&
                !String.IsNullOrEmpty(Bday_textbox.Text) &&
                !String.IsNullOrEmpty(login_textbox.Text) &&
                password_box.Password.Length >= 8 &&
                !String.IsNullOrEmpty(Gender.Text))
                edit.IsEnabled = true;
        }

        private void Bday_textbox_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {            
            if (String.IsNullOrEmpty(Bday_textbox.Text))
                edit.IsEnabled = false;            
            else if (!String.IsNullOrEmpty(Name_textbox.Text) &&
                !String.IsNullOrEmpty(Surname_textbox.Text) &&
                !String.IsNullOrEmpty(Bday_textbox.Text) &&
                !String.IsNullOrEmpty(login_textbox.Text) &&
                password_box.Password.Length >= 8 &&
                !String.IsNullOrEmpty(Gender.Text))
                edit.IsEnabled = true;
        }

        private void login_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(login_textbox.Text))
                edit.IsEnabled = false;
            else if (!String.IsNullOrEmpty(Name_textbox.Text) &&
                !String.IsNullOrEmpty(Surname_textbox.Text) &&
                !String.IsNullOrEmpty(Bday_textbox.Text) &&
                !String.IsNullOrEmpty(login_textbox.Text) &&
                password_box.Password.Length >= 8 &&
                !String.IsNullOrEmpty(Gender.Text))
                edit.IsEnabled = true;
        }

        private void password_box_PasswordChanged(object sender, RoutedEventArgs e)
        {
            
            if(i >= 1)
            {
                PasswrodChange = true;
            }
                
            if (password_box.Password.Length < 8)
                edit.IsEnabled = false;
            else if (!String.IsNullOrEmpty(Name_textbox.Text) &&
                !String.IsNullOrEmpty(Surname_textbox.Text) &&
                !String.IsNullOrEmpty(Bday_textbox.Text) &&
                !String.IsNullOrEmpty(login_textbox.Text) &&
                password_box.Password.Length >= 8 &&
                !String.IsNullOrEmpty(Gender.Text))
            {
                edit.IsEnabled = true;
                i++;
            }                
        }        
    }
}
