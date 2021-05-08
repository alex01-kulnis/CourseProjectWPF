using BespokeFusion;
using CourseProjectWPF.DB;
using CourseProjectWPF.Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseProjectWPF.Views
{
    /// <summary>
    /// Логика взаимодействия для RecordingWindow.xaml
    /// </summary>
    public partial class RecordingWindow : UserControl
    {
        MainClientWindow ClientWindow = new MainClientWindow();
        public RecordingWindow(MainClientWindow mainClientWindow)
        {            
            InitializeComponent();
            ShowDoctorsAndActiveTickets();
            buttonsEditRemoveStateChange();
        }

        public RecordingWindow()
        {
        }

         // data grid selection changed event
        private void datagridPatiens_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonsEditRemoveStateChange();
        }

        void buttonsEditRemoveStateChange()
        {
            // to make buttons enabled only when patient was chosen
            buttonEdit.IsEnabled = (datagridPatiens.SelectedItems.Count > 0);
        }

        void ShowDoctorsAndActiveTickets()
        {
            try
            {
                using (MyDbContext db = new MyDbContext())
                {
                    var doctors = (from doctor in db.Doctors
                                   select doctor.FIO).ToList();
                    DoctorFIO.ItemsSource = doctors;
                    DoctorFIO.SelectedIndex = 1;

                    User thisUser = db.Users.Find(App.CurrentUser.Id);
                    //select only patient
                    datagridPatiens.ItemsSource = db.Recordings.Where(p => p.UserId == thisUser.Id).ToList();
                }
            }
            catch (Exception) { }
        }
      
        private void FindTime(object sender, RoutedEventArgs e)
        {
            try
            {                
                if (DataVisiting.Text != "")
                {
                    DateTime a = DateTime.ParseExact(DataVisiting.Text, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);                    
                    if (a.Month <= DateTime.Now.AddMonths(2).Month && a.Date > DateTime.Now.Date && a.Year <= DateTime.Now.Year)
                    {
                        if (a.DayOfWeek.ToString() == "Saturday" || a.DayOfWeek.ToString() == "Sunday")
                            Error_Message.Text = "Врач не работает в этот день";
                        else
                        {
                            Error_Message.Text = "";

                            this.Opacity = 0.5;
                            this.Effect = new BlurEffect();
                            ChooseTimeAndConfirm Window = new ChooseTimeAndConfirm(DataVisiting.SelectedDate, DoctorFIO.Text);
                            Window.ShowDialog();
                           
                            ShowDoctorsAndActiveTickets();
                            this.Opacity = 1;
                            this.Effect = null;
                        }
                    }
                    else
                    {
                        if (a.Date <= DateTime.Now.Date)
                        {
                            Error_Message.Text = "Некорректное дата";
                        }                            
                        else
                        {
                            Error_Message.Text = "Бронирование талонов разрешено за 2 месяца";
                        }
                            
                    }                                         
                }
                else                
                    Error_Message.Text = "Выберите дату";                
            }
            catch (Exception) { }                        
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // check patient was chosen in list
                if (datagridPatiens.SelectedItems.Count <= 0)
                    return;
                // get selected patient
                Recording recording = datagridPatiens.SelectedItem as Recording;
                
                // show confirmation message box
                if (MessageBox.Show($"Вы уверенны, что хотите отменить талон на {recording.VisitDay + " " + recording.VisitTime} ?",
                    "Подтвердите", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    using (MyDbContext db = new MyDbContext())
                    {
                        // save position to restore
                        int selectedIndex = datagridPatiens.SelectedIndex;
                        recording = db.Recordings.FirstOrDefault(p => p.Id == recording.Id);
                                                                      
                        db.Recordings.Remove(recording);                        
                        // save
                        db.SaveChanges();
                        // update data grid list
                        ShowDoctorsAndActiveTickets();
                        // scroll patient list to the previous position
                        //datagridPatiens.ScrollIntoView(datagridPatiens.Items[Math.Min(selectedIndex, datagridPatiens.Items.Count - 1)]);
                        // popup notification
                        MaterialMessageBox.Show("Талон отменен","Уведомление");
                    }
                }
            }
            catch (Exception)
            {
            }           
        }
    }
}