using BespokeFusion;
using CourseProjectWPF.DB;
using CourseProjectWPF.Models;
using CourseProjectWPF.ViewModels;
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
                buttonsEditRemoveStateChange();
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

        // data grid selection changed event
        private void datagridPatiens_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonsEditRemoveStateChange();
        }

        void buttonsEditRemoveStateChange()
        {
            // to make buttons enabled only when patient was chosen
            OpenVisitUser.IsEnabled = buttonEdit.IsEnabled = buttonRemove.IsEnabled =  (datagridPatiens.SelectedItems.Count > 0);
        }
                       

        #region Validation
        private void onlyLetters(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[a-zA-Zа-яА-Я]{1,}$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void onlyDig(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsStringNumeric(e.Text);
        }

        // check string has numbers
        bool IsStringNumeric(string str)
        {
            return Regex.IsMatch(str, "[^0-9]+");
        }

        private void textbox_restrictSpace(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }
        #endregion

        #region All buttons
        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception){}            
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

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // open add patient window
                AdminAddClient addEditWindow = new AdminAddClient();
                addEditWindow.Title = "Add patient";

                // update data grid if patient was added 
                if (addEditWindow.ShowDialog() == true)
                {
                    fillDataFromDBtoDatagrid();
                    // focus on the added patient 
                    datagridPatiens.SelectedIndex = datagridPatiens.Items.Count - 1;
                    // scroll patient list to the added patient
                    //datagridPatiens.ScrollIntoView(datagridPatiens.SelectedItem);
                    // popup notification
                    //showNotification("The patient was added");
                }
            }
            catch (Exception)
            {
            }                    
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // check patient was chosen in list
                if (datagridPatiens.SelectedItems.Count <= 0)
                    return;

                // get selected patient
                User pacient = datagridPatiens.SelectedItem as User;
                MedCard card = new MedCard();
                // show confirmation message box

                if (MessageBox.Show($"Вы уверенны, что хотите удалить {pacient.FirstName + " " + pacient.Surname} ?",
                    "Подтвердите", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    using (MyDbContext db = new MyDbContext())
                    {
                        // save position to restore
                        int selectedIndex = datagridPatiens.SelectedIndex;
                        pacient = db.Users.FirstOrDefault(p => p.Id == pacient.Id);
                        card = db.MedCards.FirstOrDefault(p => p.ID == pacient.Id);
                        // find patient in db
                        if (card == null)
                        {
                            db.Users.Remove(pacient);
                        }
                        else
                        {
                            db.Users.Remove(pacient);
                            db.MedCards.Remove(card);
                        }                        
                        
                        // save
                        db.SaveChanges();
                        // update data grid list
                        fillDataFromDBtoDatagrid();
                        // scroll patient list to the previous position
                        //datagridPatiens.ScrollIntoView(datagridPatiens.Items[Math.Min(selectedIndex, datagridPatiens.Items.Count - 1)]);
                        // popup notification
                        MaterialMessageBox.Show("Пациент удалён");
                    }
                }
            }
            catch (Exception ex){}
            
        }

        // edit patient button click
        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // check patient was chosen in list
                if (datagridPatiens.SelectedItems.Count <= 0)
                    return;

                // open add patient window with filled fields
                AdminChangeClient addEditWindow = new AdminChangeClient(datagridPatiens.SelectedItem as User);
                addEditWindow.Title = "Edit patient";

                // update data grid if patient was changed 
                if (addEditWindow.ShowDialog() == true)
                {
                    // save position to restore
                    int selectedIndex = datagridPatiens.SelectedIndex;

                    fillDataFromDBtoDatagrid();
                    // focus on the changed patient from saved position
                    datagridPatiens.SelectedIndex = selectedIndex;
                    // scroll patient list to the changed patient
                    datagridPatiens.ScrollIntoView(datagridPatiens.SelectedItem);
                    // popup notification                
                }
            }
            catch (Exception)
            {
            }          
        }

        private void ConfirmVisited_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User pacient = datagridPatiens.SelectedItem as User;
                using (MyDbContext db = new MyDbContext())
                {
                    // check patient was chosen in list
                    if (datagridPatiens.SelectedItems.Count <= 0)
                        return;

                    MedCard card = new MedCard();
                    card = db.MedCards.Where(b => b.ID == pacient.Id).FirstOrDefault();
                    if (card != null)
                    {
                        // open add patient window with filled fields
                        ConfirmUsersVisiting addEditWindow = new ConfirmUsersVisiting(datagridPatiens.SelectedItem as User);
                        addEditWindow.Title = "Graphics";

                        // update data grid if patient was changed 
                        if (addEditWindow.ShowDialog() == true)
                        {
                            // save position to restore
                            int selectedIndex = datagridPatiens.SelectedIndex;

                            fillDataFromDBtoDatagrid();
                            // focus on the changed patient from saved position
                            datagridPatiens.SelectedIndex = selectedIndex;
                            // scroll patient list to the changed patient
                            datagridPatiens.ScrollIntoView(datagridPatiens.SelectedItem);
                            // popup notification                
                        }
                    }
                    else
                    {
                        MaterialMessageBox.Show("У этого пользователя нету личной карточки", "Уведомление");
                    }
                    
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion        
    }
}