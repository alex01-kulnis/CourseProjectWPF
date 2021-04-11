using CourseProjectWPF.DB;
using CourseProjectWPF.Models;
using CourseProjectWPF.ViewModels;
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
    /// Логика взаимодействия для MainAdminWindow.xaml
    /// </summary>
    public partial class MainAdminWindow : Window
    {
        MainAdminWindowModel a = new MainAdminWindowModel();
        
        
        public MainAdminWindow()
        {
            InitializeComponent();

			try
			{
                DataContext = a;
                //fill data grid
                fillDataFromDBtoDatagrid();
                // window center to screen 
                WindowStartupLocation = WindowStartupLocation.CenterScreen;
                // disable edit and remove buttons
                //buttonsEditRemoveStateChange();
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

        void fillDataFromDBtoDatagrid()
        {
            MyDbContext db = null;
            try
            {
                db = new MyDbContext();
                //select only patient
                datagridPatiens.ItemsSource = db.Users.Where(p => p.IsAdmin == false).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void buttonsEditRemoveStateChange()
        {
            // to make buttons enabled only when patient was chosen
            buttonEdit.IsEnabled = buttonRemove.IsEnabled = buttonOpen.IsEnabled = (datagridPatiens.SelectedItems.Count > 0);
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            using (MyDbContext db = new MyDbContext())
            {
                // first select all not archived patiens
                IQueryable<User> queryable = db.Users.Where(p => p.IsAdmin == false);
                // if number card field not empty
                if (!string.IsNullOrWhiteSpace(textboxNumberCard.Text))
                {
                    // selection of patients with the entered card number
                    long cardNum = Convert.ToInt64(textboxNumberCard.Text);
                    queryable = queryable.Where(p => p.Id == cardNum);
                }
                // if last name field not empty
                if (!string.IsNullOrWhiteSpace(textboxLastName.Text))
                {
                    // selection of patients whose names contain the entered text 
                    queryable = queryable.Where(p => p.Surname.Contains(textboxLastName.Text.Trim()));
                }
                // if year of birth field not empty
                if (!string.IsNullOrWhiteSpace(textboxDateOfBirth.Text))
                {
                    // selection of patients with the entered year of birth
                    int year = Convert.ToInt32(textboxDateOfBirth.Text);
                    queryable = queryable.Where(p => p.BDay.Year == year);
                }
                
                // put query result to data grid table
                datagridPatiens.ItemsSource = queryable.ToList();
            }
        }

        private void buttonEraser_Click(object sender, RoutedEventArgs e)
        {
            textboxNumberCard.Text = textboxLastName.Text = textboxDateOfBirth.Text = "";
            MyDbContext db = null;
            try
            {
                db = new MyDbContext();
                //select only patient
                datagridPatiens.ItemsSource = db.Users.Where(p => p.IsAdmin == false).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
