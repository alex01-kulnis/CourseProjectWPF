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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseProjectWPF.Views
{
    /// <summary>
    /// Логика взаимодействия для HistotyVisitingWindow.xaml
    /// </summary>
    public partial class HistotyVisitingWindow : UserControl
    {
        HistoryVisiting a = new HistoryVisiting();
        public HistotyVisitingWindow(MainClientWindow mainClientWindow)
        {
            InitializeComponent();
            ShowHistory();
        }

        public void ShowHistory()
        {
            using (MyDbContext db = new MyDbContext())
            {
                User thisUser = db.Users.Find(App.CurrentUser.Id);
                //select only patient                
                datagridPatiens.ItemsSource = db.HistoryVisitings.Where(p => p.UserId == thisUser.Id).ToList();

                a = db.HistoryVisitings.Where(p => p.UserId == thisUser.Id).FirstOrDefault();
            }           
        }

        private void datagridPatiens_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // check patient was chosen in list
            if (datagridPatiens.SelectedItems.Count <= 0)
                return;

            // open add patient window with filled fields
            var recording = datagridPatiens.SelectedItem as HistoryVisiting;
            InfoForCard.Text = recording.Info;
        }
    }
}