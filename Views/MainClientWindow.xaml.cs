using BespokeFusion;
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
    /// Логика взаимодействия для MainClientWindow.xaml
    /// </summary>
    public partial class MainClientWindow : Window
    {
        MainClientWindowModel a = new MainClientWindowModel();
        public MainClientWindow()
        {
            InitializeComponent();
            DataContext = a;
                        
        }

        #region Menu Buttons       
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void CardButton(object sender, RoutedEventArgs e)
        {
            if (ButtonCloseMenu.Visibility == Visibility.Visible)
            {
                ButtonOpenMenu.Visibility = Visibility.Visible;
                ButtonCloseMenu.Visibility = Visibility.Collapsed;

                //GridMain.Children.Clear();
                GridMain.Children.Add(new CardWindow(this));
            }
            else
            {
                //GridMain.Children.Clear();
                GridMain.Children.Add(new CardWindow(this));
            }            
        }

        private void RecordingButton(object sender, RoutedEventArgs e)
        {
            using (MyDbContext db = new MyDbContext())                
            {
                User thisUser = db.Users.Find(App.CurrentUser.Id);
                MedCard a = new MedCard();
                a = db.MedCards.Where(b => b.BDay != null && b.ID == thisUser.Id).FirstOrDefault();
                if (a != null)
                {
                    if (ButtonCloseMenu.Visibility == Visibility.Visible)
                    {
                        ButtonOpenMenu.Visibility = Visibility.Visible;
                        ButtonCloseMenu.Visibility = Visibility.Collapsed;

                        //GridMain.Children.Clear();
                        GridMain.Children.Add(new RecordingWindow(this));
                    }
                    else
                    {
                        //GridMain.Children.Clear();
                        GridMain.Children.Add(new RecordingWindow(this));
                    }
                }
                else
                    MaterialMessageBox.Show("Заполните личную карточку, чтобы записаться к врачу", "Уведомление");
            }                                                  
        }

        private void HistoryButton(object sender, RoutedEventArgs e)
        {
            using (MyDbContext db = new MyDbContext())
            {
                User thisUser = db.Users.Find(App.CurrentUser.Id);
                MedCard a = new MedCard();
                a = db.MedCards.Where(b => b.BDay != null && b.ID == thisUser.Id).FirstOrDefault();
                if (a != null)
                {
                    if (ButtonCloseMenu.Visibility == Visibility.Visible)
                    {
                        ButtonOpenMenu.Visibility = Visibility.Visible;
                        ButtonCloseMenu.Visibility = Visibility.Collapsed;

                        //GridMain.Children.Clear();
                        GridMain.Children.Add(new HistotyVisitingWindow(this));
                    }
                    else
                    {
                        //GridMain.Children.Clear();
                        GridMain.Children.Add(new HistotyVisitingWindow(this));
                    }
                }
                else
                    MaterialMessageBox.Show("У вас не может быть истории посещений, т.к. вы не заполнили личную карточку ", "Уведомление");
            }
        }

        #endregion        
    }
}