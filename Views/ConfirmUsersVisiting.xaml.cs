using BespokeFusion;
using CourseProjectWPF.DB;
using CourseProjectWPF.Models;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для ConfirmUsersVisiting.xaml
    /// </summary>
    public partial class ConfirmUsersVisiting : Window
    {
        User userr;

        public ConfirmUsersVisiting()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            // disable edit and remove buttons
            buttonsEditRemoveStateChange();
        }

        //display information about selected user
        public ConfirmUsersVisiting(User user) : this()
        {
            userr = user;
            showInfo();
        }

        void showInfo()
        {
            using (MyDbContext db = new MyDbContext())
            {
                datagridPatiens.ItemsSource = db.Recordings.Where(p => p.UserId == userr.Id).ToList();
            }

        }
        // data grid selection changed event
        private void datagridPatiens_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonsEditRemoveStateChange();
        }

        void buttonsEditRemoveStateChange()
        {
            // to make buttons enabled only when patient was chosen
            buttonConfirmVisiting.IsEnabled = buttonDelRecording.IsEnabled = (datagridPatiens.SelectedItems.Count > 0);
        }

        #region All buttons
        private void ConfirmVisiting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // check patient was chosen in list
                if (datagridPatiens.SelectedItems.Count <= 0)
                    return;
                // get selected patient
                Recording recording = datagridPatiens.SelectedItem as Recording;

                // show confirmation message box
                if (MessageBox.Show($"Вы уверенны, что хотите сделать это?",
                    "Подтвердите", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    using (MyDbContext db = new MyDbContext())
                    {
                        // save position to restore
                        int selectedIndex = datagridPatiens.SelectedIndex;
                        recording = db.Recordings.FirstOrDefault(p => p.Id == recording.Id);

                        HistotyVisiting histoty = new HistotyVisiting();
                        histoty.Id = recording.Id;
                        histoty.UserId = userr.Id;
                        histoty.FIO = recording.FIO;
                        histoty.Doctor = recording.Doctor;
                        histoty.Cabinet = recording.FIO;
                        histoty.VisitDay = recording.VisitDay;
                        histoty.VisitTime = recording.VisitTime;

                        db.HistotyVisitings.Add(histoty);
                        db.Recordings.Remove(recording);
                        // save
                        db.SaveChanges();
                        
                        MaterialMessageBox.Show("Талон подтверждён", "Уведомление");
                        showInfo();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void DelRecording_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion        
    }
}