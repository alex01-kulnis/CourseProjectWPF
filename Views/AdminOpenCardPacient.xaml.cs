using BespokeFusion;
using CourseProjectWPF.DB;
using CourseProjectWPF.Models;
using Microsoft.Win32;
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
    /// Логика взаимодействия для AdminOpenCardPacient.xaml
    /// </summary>
    public partial class AdminOpenCardPacient : Window
    {
        User userr;        
        MedCard card = new MedCard();
        string PathImage = "";
        public AdminOpenCardPacient()
        {
            InitializeComponent();
            SaveButton.IsEnabled = false;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public AdminOpenCardPacient(User user) : this()
        {
            userr = user;
            using (MyDbContext db = new MyDbContext())
            {
                card = db.MedCards.Where(b => b.ID == userr.Id).FirstOrDefault();
            }
            ShowInfo();
        }

        //вывод информации
        void ShowInfo()
        {
            try
            {
                using (MyDbContext db = new MyDbContext())
                {                                       
                    card = db.MedCards.Where(b => b.ID == userr.Id).FirstOrDefault();
                    if (card != null)
                    {
                        Name_textbox.Text = db.MedCards.Find(userr.Id).Name.Trim();
                        Surname_textbox.Text = db.MedCards.Find(userr.Id).Surname.Trim();
                        Patronymic_textbox.Text = db.MedCards.Find(userr.Id).Patronymic.Trim();
                        Gender.Text = db.MedCards.Find(userr.Id).Gender.Trim();
                        Bday_textbox.Text = db.MedCards.Find(userr.Id).BDay.ToShortDateString();
                        City_textbox.Text = db.MedCards.Find(userr.Id).City.Trim();
                        Street_textbox.Text = db.MedCards.Find(userr.Id).Street.Trim();
                        House_textbox.Text = Convert.ToString(db.MedCards.Find(userr.Id).House);
                        Housing.Text = db.MedCards.Find(userr.Id).Housing.Trim();
                        Flat_textbox.Text = db.MedCards.Find(userr.Id).Flat.Trim();
                        PathImage = db.MedCards.Find(userr.Id).Image;
                        AdMainImage.Source = new BitmapImage(new Uri(PathImage, UriKind.Absolute));
                        Border.BorderThickness = new Thickness(0);
                    }

                    //var users = (from user in db.Users
                    //             select user).ToList();
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

        private void Housing_TextChanged(object sender, TextChangedEventArgs e)
        {
            string str = Housing.Text;
            Regex regex = new Regex("^[0-9]{0,3}[а-яА-Я]?$");
            if (!regex.IsMatch(str))
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
                    AdMainImage.Source != null)
                SaveButton.IsEnabled = true;
        }
        #endregion
        #endregion

        #region Buttons        
        //Сохранение/Изменение
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MyDbContext db = new MyDbContext())
                {                    
                    MedCard test = db.MedCards.FirstOrDefault(p => p.ID == userr.Id);

                    if (Bday_textbox.SelectedDate < DateTime.Now.Date && Bday_textbox.SelectedDate > DateTime.Now.AddYears(-150).Date)
                    {
                        if (test == null)
                        {
                            MedCard card = new MedCard();
                            card.ID = userr.Id;
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
                            card.Flat = Flat_textbox.Text;
                            card.Image = PathImage;
                            db.MedCards.AddRange(new List<MedCard> { card });
                            db.SaveChanges();
                            MaterialMessageBox.Show("Сохранение/Изменение прошло успешна", "Уведомление");
                        }
                        else
                        {
                            db.MedCards.Find(userr.Id).Name = Name_textbox.Text.Trim();
                            db.MedCards.Find(userr.Id).Surname = Surname_textbox.Text.Trim();
                            db.MedCards.Find(userr.Id).Patronymic = Patronymic_textbox.Text.Trim();
                            db.MedCards.Find(userr.Id).Gender = Gender.Text.Trim();
                            db.MedCards.Find(userr.Id).BDay = DateTime.ParseExact(Bday_textbox.Text, "dd.MM.yyyy",
                                    System.Globalization.CultureInfo.InvariantCulture);
                            db.MedCards.Find(userr.Id).City = City_textbox.Text.Trim();
                            db.MedCards.Find(userr.Id).Street = Street_textbox.Text.Trim();
                            db.MedCards.Find(userr.Id).House = Convert.ToInt32(House_textbox.Text);
                            db.MedCards.Find(userr.Id).Housing = Housing.Text.Trim();
                            db.MedCards.Find(userr.Id).Flat = Flat_textbox.Text.Trim();
                            db.MedCards.Find(userr.Id).Image = PathImage;
                            AdMainImage.Source = new BitmapImage(new Uri(PathImage, UriKind.Absolute));
                            db.SaveChanges();
                            MaterialMessageBox.Show("Сохранение/Изменение прошло успешна", "Уведомление");
                        }
                    }
                    else
                    {
                        MaterialMessageBox.Show("Некорретная дата, попробуйте ещё раз", "Уведомление");
                        Bday_textbox.Text = "";
                    }
                }
            }
            catch (Exception ex) { MaterialMessageBox.Show("Упс.. Что-то пошло не так", "Уведомление"); }
        }

        //закрыть окно
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
            catch (Exception ex) { MaterialMessageBox.Show("Упс.. Что-то пошло не так", "Уведомление"); }
        }
        #endregion        
    }
}