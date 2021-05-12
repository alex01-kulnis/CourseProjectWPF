using CourseProjectWPF.DB;
using CourseProjectWPF.Models;
using System;
using System.Collections.Generic;
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

        //FindBttn
        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MyDbContext db = new MyDbContext())
                {
                    User thisUser = db.Users.Find(App.CurrentUser.Id);
                    // first select all not archived patiens
                    IQueryable<HistoryVisiting> queryable = db.HistoryVisitings.Where(p => p.UserId == thisUser.Id);                    
                    // if year of birth field not empty
                    if (!string.IsNullOrWhiteSpace(NumberOfMonth.Text))
                    {
                        // selection of patients with the entered year of birth
                        int month = Convert.ToInt32(NumberOfMonth.Text);
                        queryable = queryable.Where(p => p.VisitDay.Month == month);
                    }

                    // put query result to data grid table
                    datagridPatiens.ItemsSource = queryable.ToList();
                }
            }
            catch (Exception) { }
        }
        
        private void buttonEraser_Click(object sender, RoutedEventArgs e)
        {
            ShowHistory();
        }

        //private void onlyDig(object sender, TextCompositionEventArgs e)
        //{
        //    e.Handled = IsStringNumeric(e.Text);
        //}

        //// check string has numbers
        //bool IsStringNumeric(string str)
        //{
        //    return Regex.IsMatch(str, "[^0-9]+");
        //}

        //private void textbox_restrictSpace(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Space)
        //        e.Handled = true;
        //}
    }
}