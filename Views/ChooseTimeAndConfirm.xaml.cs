using BespokeFusion;
using CourseProjectWPF.DB;
using CourseProjectWPF.Models;
using CourseProjectWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для ChooseTimeAndConfirm.xaml
    /// </summary>
    public partial class ChooseTimeAndConfirm : Window
    {
        private DateTime? selectedDate;
        private string DocFio;
        ChooseTimeAndConfirmModel a = new ChooseTimeAndConfirmModel();       
        public DateTime Time1;
        public DateTime Time2;       
        
        public ChooseTimeAndConfirm()
        {            
            InitializeComponent();
            DataContext = a;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;          
        }
               
        public ChooseTimeAndConfirm(DateTime? selectedDate, string text):this()
        {
            try
            {
                Visiting_textbox.SelectedDate = this.selectedDate = selectedDate;
                FIO_textbox.Text = this.DocFio = text;
                ShowInfo();
            }
            catch (Exception){}           
        }  
        
       //show info
       public void ShowInfo()
       {
            try
            {
                Doctor doctor;
                using (MyDbContext db = new MyDbContext())
                {
                    User thisUser = db.Users.Find(App.CurrentUser.Id);
                    doctor = db.Doctors.Where(b => b.FIO == FIO_textbox.Text).FirstOrDefault();
                    Post_textbox.Text = doctor.Post;
                    Cabinet_textbox.Text = doctor.Cabinet;

                    DateTime WorkTime = DateTime.ParseExact(Visiting_textbox.Text, "dd.MM.yyyy",
                                    System.Globalization.CultureInfo.InvariantCulture);
                    if (WorkTime.Day % 2 == 0)
                    {
                        TimeWork_textbox.Text = doctor.Evenday_start.Hour.ToString() + "." + doctor.Evenday_start.Minute.ToString() + doctor.Evenday_start.Second.ToString()
                            + "-" + doctor.Evenday_end.Hour.ToString() + "." + doctor.Evenday_end.Minute.ToString() + doctor.Evenday_end.Second.ToString();
                        Time1 = doctor.Evenday_start;
                        Time2 = doctor.Evenday_end;
                    }
                    else
                    {
                        TimeWork_textbox.Text = doctor.Oddday_start.Hour.ToString() + "." + doctor.Oddday_start.Minute.ToString() + doctor.Oddday_start.Second.ToString()
                            + "-" + doctor.Oddday_end.Hour.ToString() + "." + doctor.Oddday_end.Minute.ToString() + doctor.Oddday_start.Second.ToString();
                        Time1 = doctor.Oddday_start;
                        Time2 = doctor.Oddday_end;
                    }
                   
                    var d = db.Recordings.Where(b => b.FIO == FIO_textbox.Text && b.VisitDay == Visiting_textbox.SelectedDate).FirstOrDefault();                                      

                    if (d != null)
                    {
                        var Times1 = Time1.TimeOfDay.ToString();
                        var Times2 = Time1.AddMinutes(12).TimeOfDay.ToString();
                        var Times3 = Time1.AddMinutes(24).TimeOfDay.ToString();
                        var Times4 = Time1.AddMinutes(36).TimeOfDay.ToString();
                        var Times5 = Time1.AddMinutes(48).TimeOfDay.ToString();
                        var Times6 = Time1.AddMinutes(60).TimeOfDay.ToString();
                        var Times7 = Time1.AddMinutes(72).TimeOfDay.ToString();
                        var Times8 = Time1.AddMinutes(84).TimeOfDay.ToString();
                        var Times9 = Time1.AddMinutes(96).TimeOfDay.ToString();
                        var Times10 = Time1.AddMinutes(108).TimeOfDay.ToString();

                        List<string> str2 = new List<string>();

                        var tt1 = db.Recordings.Where(b => b.FIO == FIO_textbox.Text && b.VisitDay == Visiting_textbox.SelectedDate && b.VisitTime.ToString() == Times1.ToString()).FirstOrDefault();
                        if (tt1 == null)                        
                            str2.Add(Times1.ToString());
                                                
                        var tt2 = db.Recordings.Where(b => b.FIO == FIO_textbox.Text && b.VisitDay == Visiting_textbox.SelectedDate && b.VisitTime.ToString() == Times2.ToString()).FirstOrDefault();
                        if (tt2 == null)
                            str2.Add(Times2.ToString());

                        var tt3 = db.Recordings.Where(b => b.FIO == FIO_textbox.Text && b.VisitDay == Visiting_textbox.SelectedDate && b.VisitTime.ToString() == Times3.ToString()).FirstOrDefault();
                        if (tt3 == null)
                            str2.Add(Times3.ToString());

                        var tt4 = db.Recordings.Where(b => b.FIO == FIO_textbox.Text && b.VisitDay == Visiting_textbox.SelectedDate && b.VisitTime.ToString() == Times4.ToString()).FirstOrDefault();
                        if (tt4 == null)
                            str2.Add(Times4.ToString());

                        var tt5 = db.Recordings.Where(b => b.FIO == FIO_textbox.Text && b.VisitDay == Visiting_textbox.SelectedDate && b.VisitTime.ToString() == Times5.ToString()).FirstOrDefault();
                        if (tt5 == null)
                            str2.Add(Times5.ToString());

                        var tt6 = db.Recordings.Where(b => b.FIO == FIO_textbox.Text && b.VisitDay == Visiting_textbox.SelectedDate && b.VisitTime.ToString() == Times6.ToString()).FirstOrDefault();
                        if (tt6 == null)
                            str2.Add(Times6.ToString());

                        var tt7 = db.Recordings.Where(b => b.FIO == FIO_textbox.Text && b.VisitDay == Visiting_textbox.SelectedDate && b.VisitTime.ToString() == Times7.ToString()).FirstOrDefault();
                        if (tt7 == null)
                            str2.Add(Times7.ToString());

                        var tt8 = db.Recordings.Where(b => b.FIO == FIO_textbox.Text && b.VisitDay == Visiting_textbox.SelectedDate && b.VisitTime.ToString() == Times8.ToString()).FirstOrDefault();
                        if (tt8 == null)
                            str2.Add(Times8.ToString());

                        var tt9 = db.Recordings.Where(b => b.FIO == FIO_textbox.Text && b.VisitDay == Visiting_textbox.SelectedDate && b.VisitTime.ToString() == Times9.ToString()).FirstOrDefault();
                        if (tt9 == null)
                            str2.Add(Times9.ToString());

                        var tt10 = db.Recordings.Where(b => b.FIO == FIO_textbox.Text && b.VisitDay == Visiting_textbox.SelectedDate && b.VisitTime.ToString() == Times10.ToString()).FirstOrDefault();
                        if (tt10 == null)
                            str2.Add(Times10.ToString());

                        CountFreeTickets_textbox.Text = str2.Count.ToString();
                        TimeVisiting.ItemsSource = str2;
                        TimeVisiting.SelectedIndex = 0;                        
                    }
                    else
                    {
                        string t1, t2, t3, t4, t5, t6, t7, t8, t9, t10;
                        t1 = Time1.TimeOfDay.ToString();
                        t2 = Time1.AddMinutes(12).TimeOfDay.ToString();
                        t3 = Time1.AddMinutes(24).TimeOfDay.ToString();
                        t4 = Time1.AddMinutes(36).TimeOfDay.ToString();
                        t5 = Time1.AddMinutes(48).TimeOfDay.ToString();
                        t6 = Time1.AddMinutes(60).TimeOfDay.ToString();
                        t7 = Time1.AddMinutes(72).TimeOfDay.ToString();
                        t8 = Time1.AddMinutes(84).TimeOfDay.ToString();
                        t9 = Time1.AddMinutes(96).TimeOfDay.ToString();
                        t10 = Time1.AddMinutes(108).TimeOfDay.ToString();
                                                              
                        List<string> str = new List<string>() { t1, t2, t3, t4, t5, t6, t7, t8, t9, t10 };
                        CountFreeTickets_textbox.Text = str.Count.ToString();
                        TimeVisiting.ItemsSource = str;
                        TimeVisiting.SelectedIndex = 0;
                    }                                     
                }
            }
            catch (Exception)
            {
                MaterialMessageBox.Show("Ошибка, что-то пошло не так..", "Уведомление");
            }
       }

        private void Recording_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MyDbContext db = new MyDbContext())
                {
                    if (TimeVisiting.Text == "")
                    {
                        ErrorMessage.Text = "Свободных талонов нету";
                    }
                    else
                    {
                        User thisUser = db.Users.Find(App.CurrentUser.Id);

                        Recording recording = new Recording();
                        recording.UserId = thisUser.Id;
                        recording.FIO = FIO_textbox.Text;
                        recording.Doctor = Post_textbox.Text;
                        recording.Cabinet = Cabinet_textbox.Text;
                        recording.VisitDay = DateTime.ParseExact(Visiting_textbox.Text, "dd.MM.yyyy",
                                        System.Globalization.CultureInfo.InvariantCulture);
                        recording.VisitTime = TimeVisiting.Text.Trim();                  
                        db.Recordings.Add(recording);
                        db.SaveChanges();
                        MaterialMessageBox.Show("Запись прошло успешна", "Уведомление");                        
                        this.Close();
                    }                    
                }
            }
            catch (Exception)
            {               
            }                           
        }
    }
}