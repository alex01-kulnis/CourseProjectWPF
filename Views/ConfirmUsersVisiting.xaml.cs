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
        HistoryVisiting a = new HistoryVisiting();
        MedCard card = new MedCard();
        public ConfirmUsersVisiting()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            // disable edit and remove buttons
            buttonsEditRemoveStateChange();
            buttonsEditRemoveStateChange2();
        }

        //display information about selected user
        public ConfirmUsersVisiting(User user) : this()
        {
            userr = user;
            using (MyDbContext db = new MyDbContext())
            {
                
                card = db.MedCards.Where(b => b.ID == userr.Id).FirstOrDefault();
            }
            FIO.Text ="Пациент :" + " " + card.Surname + " " + card.Name + " " + card.Patronymic;
            showInfo();
            ShowHistory();
        }

        public void ShowHistory()
        {
            using (MyDbContext db = new MyDbContext())
            {
                //select only patient                
                datagridHistory.ItemsSource = db.HistoryVisitings.Where(p => p.UserId == userr.Id).ToList();

                a = db.HistoryVisitings.Where(p => p.UserId == userr.Id).FirstOrDefault();
            }
        }

        void showInfo()
        {
            using (MyDbContext db = new MyDbContext())
            {
                datagridPatiens.ItemsSource = db.Recordings.Where(p => p.UserId == userr.Id).ToList();
            }

        }

        private void datagridHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonsEditRemoveStateChange2();
        }

        // data grid selection changed event
        private void datagridPatiens_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonsEditRemoveStateChange();
        }

        void buttonsEditRemoveStateChange2()
        {
            // to make buttons enabled only when patient was chosen
            buttonСhangeHistory.IsEnabled = (datagridHistory.SelectedItems.Count > 0);
        }

        void buttonsEditRemoveStateChange()
        {
            // to make buttons enabled only when patient was chosen
            buttonConfirmVisiting.IsEnabled = (datagridPatiens.SelectedItems.Count > 0);
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

                        HistoryVisiting histoty = new HistoryVisiting();
                        histoty.Id = recording.Id;
                        histoty.UserId = recording.UserId;
                        histoty.FIO = recording.FIO;
                        histoty.Doctor = recording.Doctor;
                        histoty.Cabinet = recording.Cabinet;
                        histoty.VisitDay = recording.VisitDay;
                        histoty.VisitTime = recording.VisitTime;
                        histoty.Info = InfoForCardConfirm.Text;

                        db.HistoryVisitings.Add(histoty);
                        db.Recordings.Remove(recording);
                        // save
                        db.SaveChanges();
                        
                        MaterialMessageBox.Show("Талон подтверждён", "Уведомление");
                        InfoForCardConfirm.Text = "";
                        showInfo();
                        ShowHistory();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void СhangeHistory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // check patient was chosen in list
                if (datagridHistory.SelectedItems.Count <= 0)
                    return;
                // get selected patient
                HistoryVisiting historychange = datagridHistory.SelectedItem as HistoryVisiting;

                // show confirmation message box
                if (MessageBox.Show($"Вы уверенны, что хотите изменить ?",
                    "Подтвердите", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    using (MyDbContext db = new MyDbContext())
                    {
                        // save position to restore
                        int selectedIndex = datagridHistory.SelectedIndex;
                        historychange = db.HistoryVisitings.FirstOrDefault(p => p.Id == historychange.Id);                        

                        db.HistoryVisitings.Find(historychange.Id).Info = InfoForCheck.Text;
                        // save
                        db.SaveChanges();

                        MaterialMessageBox.Show("Данные изменены", "Уведомление");
                        InfoForCheck.Text = "";                        
                        ShowHistory();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void DelRecording_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MyDbContext db = new MyDbContext())
                {
                    DateTime a = DateTime.Now.Date;
                    var all = db.Recordings.FirstOrDefault(p => p.VisitDay < a);

                    if (all != null)
                    {
                        db.Recordings.Remove(all);
                        // save
                        db.SaveChanges();
                        MaterialMessageBox.Show("Всё очищено", "Уведомление");
                    }
                    else                    
                        MaterialMessageBox.Show("Такие даты отсуствуют", "Уведомление");                    
                }
            }
            catch (Exception)
            {
            }
        }

        private void datagridHistory_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // open add patient window with filled fields
            var recording = datagridHistory.SelectedItem as HistoryVisiting;
            InfoForCheck.Text = recording.Info;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        
    }
}