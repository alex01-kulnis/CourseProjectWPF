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
                datagridPatiens.ItemsSource = db.HistotyVisitings.Where(p => p.UserId == thisUser.Id).ToList();
            }           
        }
    }
}
