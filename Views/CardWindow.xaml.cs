using BespokeFusion;
using CourseProjectWPF.DB;
using CourseProjectWPF.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
    /// Логика взаимодействия для CardWindow.xaml
    /// </summary>
    public partial class CardWindow : UserControl
    {
        string PathImage = "";
        public CardWindow(MainClientWindow mainClientWindow)
        {
            InitializeComponent();
            SaveButton.IsEnabled = false;
            ShowInfo();                                         
        }

        //вывод информации
        void ShowInfo()
        {
            try
            {
                using (MyDbContext db = new MyDbContext())
                {
                    User thisUser = db.Users.Find(App.CurrentUser.Id);
                    MedCard a = new MedCard();
                    a = db.MedCards.Where(b => b.ID == thisUser.Id).FirstOrDefault();
                    if (a != null)
                    {
                        Name_textbox.Text = db.MedCards.Find(thisUser.Id).Name.Trim();
                        Surname_textbox.Text = db.MedCards.Find(thisUser.Id).Surname.Trim();
                        Patronymic_textbox.Text = db.MedCards.Find(thisUser.Id).Patronymic.Trim();
                        Gender.Text = db.MedCards.Find(thisUser.Id).Gender.Trim();
                        Bday_textbox.Text = db.MedCards.Find(thisUser.Id).BDay.ToShortDateString();
                        City_textbox.Text = db.MedCards.Find(thisUser.Id).City.Trim();
                        Street_textbox.Text = db.MedCards.Find(thisUser.Id).Street.Trim();
                        House_textbox.Text = Convert.ToString(db.MedCards.Find(thisUser.Id).House);
                        Housing.Text = db.MedCards.Find(thisUser.Id).Housing.Trim();
                        Flat_textbox.Text = Convert.ToString(db.MedCards.Find(thisUser.Id).Flat);
                        PathImage = db.MedCards.Find(thisUser.Id).Image;
                        AdMainImage.Source = new BitmapImage(new Uri(PathImage, UriKind.Absolute));
                        Border.BorderThickness = new Thickness(0);
                    }
                    else
                        MaterialMessageBox.Show("Заполните личную карточку", "Уведомление");


                    var users = (from user in db.Users                                 
                                 select user).ToList();
                }
            }
            catch (Exception) { }
        }
        

        #region Validation
        //spaces
        private void textbox_restrictSpace(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

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

        private void login_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string str = Housing.Text;
            Regex regex = new Regex("^[0-9]{0,3}[а-яА-Я]?$");
            if(!regex.IsMatch(str))            
                Housing.Text = "";                                                                             
        }

        #region На пустые поля
        private void Name_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(Name_textbox.Text))
                SaveButton.IsEnabled = false;
            else if (!String.IsNullOrEmpty(Name_textbox.Text) &&
                !String.IsNullOrEmpty(Surname_textbox.Text) &&
                !String.IsNullOrEmpty(Patronymic_textbox.Text) &&
                !String.IsNullOrEmpty(Gender.Text) &&
                !String.IsNullOrEmpty(Bday_textbox.Text) &&
                !String.IsNullOrEmpty(City_textbox.Text) &&
                !String.IsNullOrEmpty(Street_textbox.Text) &&
                !String.IsNullOrEmpty(House_textbox.Text) &&
                !String.IsNullOrEmpty(Flat_textbox.Text) &&
                    AdMainImage.Source != null)
                SaveButton.IsEnabled = true;
        }

        private void Surname_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(Surname_textbox.Text))
                SaveButton.IsEnabled = false;
            else if (!String.IsNullOrEmpty(Name_textbox.Text) &&
                !String.IsNullOrEmpty(Surname_textbox.Text) &&
                !String.IsNullOrEmpty(Patronymic_textbox.Text) &&
                !String.IsNullOrEmpty(Gender.Text) &&
                !String.IsNullOrEmpty(Bday_textbox.Text) &&
                !String.IsNullOrEmpty(City_textbox.Text) &&
                !String.IsNullOrEmpty(Street_textbox.Text) &&
                !String.IsNullOrEmpty(House_textbox.Text) &&
                !String.IsNullOrEmpty(Flat_textbox.Text) &&
                    AdMainImage.Source != null)
                SaveButton.IsEnabled = true;
        }

        private void Patronymic_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(Patronymic_textbox.Text))
                SaveButton.IsEnabled = false;
            else if (!String.IsNullOrEmpty(Name_textbox.Text) &&
                !String.IsNullOrEmpty(Surname_textbox.Text) &&
                !String.IsNullOrEmpty(Patronymic_textbox.Text) &&
                !String.IsNullOrEmpty(Gender.Text) &&
                !String.IsNullOrEmpty(Bday_textbox.Text) &&
                !String.IsNullOrEmpty(City_textbox.Text) &&
                !String.IsNullOrEmpty(Street_textbox.Text) &&
                !String.IsNullOrEmpty(House_textbox.Text) &&
                !String.IsNullOrEmpty(Flat_textbox.Text) &&
                    AdMainImage.Source != null)
                SaveButton.IsEnabled = true;
        }

        private void Bday_textbox_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(Bday_textbox.Text))
                SaveButton.IsEnabled = false;
            else if (!String.IsNullOrEmpty(Name_textbox.Text) &&
                !String.IsNullOrEmpty(Surname_textbox.Text) &&
                !String.IsNullOrEmpty(Patronymic_textbox.Text) &&
                !String.IsNullOrEmpty(Gender.Text) &&
                !String.IsNullOrEmpty(Bday_textbox.Text) &&
                !String.IsNullOrEmpty(City_textbox.Text) &&
                !String.IsNullOrEmpty(Street_textbox.Text) &&
                !String.IsNullOrEmpty(House_textbox.Text) &&
                !String.IsNullOrEmpty(Flat_textbox.Text) &&
                    AdMainImage.Source != null)
                SaveButton.IsEnabled = true;
        }

        private void City_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(City_textbox.Text))
                SaveButton.IsEnabled = false;
            else if (!String.IsNullOrEmpty(Name_textbox.Text) &&
                !String.IsNullOrEmpty(Surname_textbox.Text) &&
                !String.IsNullOrEmpty(Patronymic_textbox.Text) &&
                !String.IsNullOrEmpty(Gender.Text) &&
                !String.IsNullOrEmpty(Bday_textbox.Text) &&
                !String.IsNullOrEmpty(City_textbox.Text) &&
                !String.IsNullOrEmpty(Street_textbox.Text) &&
                !String.IsNullOrEmpty(House_textbox.Text) &&
                !String.IsNullOrEmpty(Flat_textbox.Text) &&
                    AdMainImage.Source != null)
                SaveButton.IsEnabled = true;
        }

        private void Street_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(Street_textbox.Text))
                SaveButton.IsEnabled = false;
            else if (!String.IsNullOrEmpty(Name_textbox.Text) &&
                !String.IsNullOrEmpty(Surname_textbox.Text) &&
                !String.IsNullOrEmpty(Patronymic_textbox.Text) &&
                !String.IsNullOrEmpty(Gender.Text) &&
                !String.IsNullOrEmpty(Bday_textbox.Text) &&
                !String.IsNullOrEmpty(City_textbox.Text) &&
                !String.IsNullOrEmpty(Street_textbox.Text) &&
                !String.IsNullOrEmpty(House_textbox.Text) &&
                !String.IsNullOrEmpty(Flat_textbox.Text) &&
                    AdMainImage.Source != null)
                SaveButton.IsEnabled = true;
        }

        private void House_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(House_textbox.Text))
                SaveButton.IsEnabled = false;
            else if (!String.IsNullOrEmpty(Name_textbox.Text) &&
                !String.IsNullOrEmpty(Surname_textbox.Text) &&
                !String.IsNullOrEmpty(Patronymic_textbox.Text) &&
                !String.IsNullOrEmpty(Gender.Text) &&
                !String.IsNullOrEmpty(Bday_textbox.Text) &&
                !String.IsNullOrEmpty(City_textbox.Text) &&
                !String.IsNullOrEmpty(Street_textbox.Text) &&
                !String.IsNullOrEmpty(House_textbox.Text) &&
                !String.IsNullOrEmpty(Flat_textbox.Text) &&
                    AdMainImage.Source != null)
                SaveButton.IsEnabled = true;
        }

        private void Flat_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(Flat_textbox.Text))
                SaveButton.IsEnabled = false;
            else if (!String.IsNullOrEmpty(Name_textbox.Text) &&
                !String.IsNullOrEmpty(Surname_textbox.Text) &&
                !String.IsNullOrEmpty(Patronymic_textbox.Text) &&
                !String.IsNullOrEmpty(Gender.Text) &&
                !String.IsNullOrEmpty(Bday_textbox.Text) &&
                !String.IsNullOrEmpty(City_textbox.Text) &&
                !String.IsNullOrEmpty(Street_textbox.Text) &&
                !String.IsNullOrEmpty(House_textbox.Text) &&
                !String.IsNullOrEmpty(Flat_textbox.Text) &&
                    AdMainImage.Source != null)
                SaveButton.IsEnabled = true;
        }

        private void AdMainImage_Changed(object sender, SizeChangedEventArgs e)
        {
            if (AdMainImage.Source == null)
                SaveButton.IsEnabled = false;
            else if (!String.IsNullOrEmpty(Name_textbox.Text) &&
                !String.IsNullOrEmpty(Surname_textbox.Text) &&
                !String.IsNullOrEmpty(Patronymic_textbox.Text) &&
                !String.IsNullOrEmpty(Gender.Text) &&
                !String.IsNullOrEmpty(Bday_textbox.Text) &&
                !String.IsNullOrEmpty(City_textbox.Text) &&
                !String.IsNullOrEmpty(Street_textbox.Text) &&
                !String.IsNullOrEmpty(House_textbox.Text) &&
                !String.IsNullOrEmpty(Flat_textbox.Text) &&
                    AdMainImage.Source != null)
                SaveButton.IsEnabled = true;
        }
        #endregion
        #endregion

        #region Buttons        
        //Сохранение
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MyDbContext db = new MyDbContext())
                {
                    User thisUser = db.Users.Find(App.CurrentUser.Id);
                    MedCard test = db.MedCards.FirstOrDefault(p => p.ID == thisUser.Id);
                                                           
                    if (Bday_textbox.SelectedDate < DateTime.Now)
                    {
                        if (test == null)
                        {
                            MedCard card = new MedCard();
                            card.ID = thisUser.Id;
                            card.Name = Name_textbox.Text;
                            card.Surname = Surname_textbox.Text;
                            card.Patronymic = Patronymic_textbox.Text;
                            card.Gender = Gender.Text;
                            card.BDay = DateTime.ParseExact(Bday_textbox.Text, "dd.MM.yyyy",
                                        System.Globalization.CultureInfo.InvariantCulture);
                            card.City = City_textbox.Text;
                            card.Street = Street_textbox.Text;
                            card.House = Convert.ToInt32(House_textbox.Text);
                            card.Housing = Housing.Text;
                            card.Flat = Convert.ToInt32(Flat_textbox.Text);
                            card.Image = PathImage;
                            db.MedCards.AddRange(new List<MedCard> { card });
                            db.SaveChanges();
                            MaterialMessageBox.Show("Сохранение/Изменение прошло успешна", "Уведомление");
                        }
                        else
                        {
                            db.MedCards.Find(thisUser.Id).Name = Name_textbox.Text.Trim();
                            db.MedCards.Find(thisUser.Id).Surname = Surname_textbox.Text.Trim();
                            db.MedCards.Find(thisUser.Id).Patronymic = Patronymic_textbox.Text.Trim();
                            db.MedCards.Find(thisUser.Id).Gender = Gender.Text.Trim();
                            db.MedCards.Find(thisUser.Id).BDay = DateTime.ParseExact(Bday_textbox.Text, "dd.MM.yyyy",
                                    System.Globalization.CultureInfo.InvariantCulture);
                            db.MedCards.Find(thisUser.Id).City = City_textbox.Text.Trim();
                            db.MedCards.Find(thisUser.Id).Street = Street_textbox.Text.Trim();
                            db.MedCards.Find(thisUser.Id).House = Convert.ToInt32(House_textbox.Text);
                            db.MedCards.Find(thisUser.Id).Housing = Housing.Text.Trim();
                            db.MedCards.Find(thisUser.Id).Flat = Convert.ToInt32(Flat_textbox.Text);
                            db.MedCards.Find(thisUser.Id).Image = PathImage;
                            AdMainImage.Source = new BitmapImage(new Uri(PathImage, UriKind.Absolute));
                            db.SaveChanges();
                            MaterialMessageBox.Show("Сохранение/Изменение прошло успешна", "Уведомление");
                        }
                    }
                    else
                    {
                        Bday_textbox.Text = "";
                    }
                    
                }
            }
            catch (Exception ex) {}           
        }

        //Добавить фото
        private void AddPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Фотографии|*.jpg;*.png;*.jpeg;";
                if (openFileDialog.ShowDialog() == true)
                {
                    AdMainImage.Source = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute)); //отображение
                    PathImage = openFileDialog.FileName;
                    Border.BorderThickness = new Thickness(0);
                }
            }
            catch (Exception ex) { }
        }
        #endregion
    }
}