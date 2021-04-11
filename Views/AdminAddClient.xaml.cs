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
    /// Логика взаимодействия для AdminAddClient.xaml
    /// </summary>
    public partial class AdminAddClient : Window
    {
        AdminAddClientModel reg = new AdminAddClientModel();
        User user;
        public AdminAddClient()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            DataContext = reg;
            Reg.IsEnabled = false;

        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            using (MyDbContext db = new MyDbContext())
            {
                ErrorMessage.Text = "";
                bool Registration = true;
                bool Data = true;
                try
                {
                    string str = Bday_textbox.ToString();
                    int a = Convert.ToInt32(str = str.Substring(6, 4));
                    if (a >= 2005)
                    {
                        Data = false;
                        ErrorMessage.Text = "Некорректная дата";
                    }
                    else
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
                            user.BDay = DateTime.ParseExact(Bday_textbox.Text, "dd.MM.yyyy",
                                System.Globalization.CultureInfo.InvariantCulture);
                            user.Gender = Gender.Text.Trim();
                            user.Login = login_textbox.Text.Trim();
                            user.Password = password_box.Password;

                            db.Users.Add(user);
                            db.SaveChanges();

                            MainAdminWindow t = new MainAdminWindow();
                            t.Show();
                            Close();
                            
                        }
                        else
                        {
                            ErrorMessage.Text = "Такой логин уже существует";
                        }
                    }
                }
                catch (Exception ex) { }
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
                Reg.IsEnabled = false;
            else if (!String.IsNullOrEmpty(Name_textbox.Text) &&
                !String.IsNullOrEmpty(Surname_textbox.Text) &&
                !String.IsNullOrEmpty(Bday_textbox.Text) &&
                !String.IsNullOrEmpty(login_textbox.Text) &&
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
                !String.IsNullOrEmpty(login_textbox.Text) &&
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
    }
}
