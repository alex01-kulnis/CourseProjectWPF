using CourseProjectWPF.DB;
using CourseProjectWPF.Models;
using CourseProjectWPF.Views;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CourseProjectWPF.ViewModels
{
    public class MainAdminWindowModel : ViewModelBase
    {
        public MainAdminWindowModel()
        {

        }

  //      private string _CardID;
  //      public string CardID
  //      {
  //          get { return _CardID; }
  //          set
  //          {
  //              this._CardID = value;
  //              RaisePropertiesChanged(nameof(CardID));
  //          }
  //      }

  //      private string _Surname;
  //      public string Surname
  //      {
  //          get { return _Surname; }
  //          set
  //          {
  //              this._Surname = value;
  //              RaisePropertiesChanged(nameof(Surname));
  //          }
  //      }        

  //      private string _YearOfBithday;
  //      public string YearOfBithday
  //      {
  //          get { return _YearOfBithday; }
  //          set
  //          {
  //              this._YearOfBithday = value;
  //              RaisePropertiesChanged(nameof(YearOfBithday));
  //          }
  //      }

  //      private string _Gender;
  //      public string Gender
  //      {
  //          get { return _Gender; }
  //          set
  //          {
  //              if (value.Equals("Мужчина"))
  //                  this._Gender = "М";
  //              else if (value.Equals("Женщина"))
  //                  this._Gender = "Ж";
  //              RaisePropertiesChanged(nameof(Gender));

  //          }
  //      }

  //      private ObservableCollection<User> _info = new ObservableCollection<User>();
  //      public ObservableCollection<User> info
  //      {
  //          get { return _info; }
  //          set { _info = value; }
  //      }

  //      public ICommand find => new DelegateCommand(Find);

  //      public void Find()
  //      {
		//	using (MyDbContext db = new MyDbContext())
		//	{
		//		// first select all not archived patiens
		//		IQueryable<User> queryable = db.Users.Where(p => p.IsAdmin == false);
		//		// if number card field not empty
		//		if (!string.IsNullOrWhiteSpace(CardID))
		//		{
		//			// selection of patients with the entered card number
		//			long cardNum = Convert.ToInt64(CardID);
		//			queryable = queryable.Where(p => p.Id == cardNum);
		//		}
		//		// if last name field not empty
		//		if (!string.IsNullOrWhiteSpace(Surname))
		//		{
		//			// selection of patients whose names contain the entered text 
		//			queryable = queryable.Where(p => p.Surname.Contains(Surname));
		//		}
		//		// if year of birth field not empty
		//		if (!string.IsNullOrWhiteSpace(YearOfBithday))
		//		{
		//			// selection of patients with the entered year of birth
		//			int year = Convert.ToInt32(YearOfBithday);
		//			queryable = queryable.Where(p => p.BDay.Year == year);
		//		}
		//		// if address field not empty
		//		if (!string.IsNullOrWhiteSpace(Gender))
		//		{
		//			// selection of patients whose address contain the entered text 
		//			queryable = queryable.Where(p => p.Gender.Contains(Gender));
		//		}
  //              // put query result to data grid table
  //              info.Add(queryable.FirstOrDefault());
                
  //              //datagridPatiens.ItemsSource = queryable.ToList();
  //          }
		//}

        public ICommand addclient => new DelegateCommand(Addclient);

        public void Addclient()
        {
            AdminAddClient t = new AdminAddClient();
            t.Show();
            Close();
        }
        
        public ICommand changeacc => new DelegateCommand(Changeacc);

        public void Changeacc()
        {
            if (MessageBox.Show($"Вы уверенны, что вы хотите выйти?",
                "Подтвердите", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                AuthView t = new AuthView();
                Close();
                Thread myThread = new Thread(new ThreadStart(DB.DB.ShowLoader));
                myThread.SetApartmentState(ApartmentState.STA);
                myThread.Start();
                Thread.Sleep(1000);
                myThread.Abort();
                t.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                t.Show();
            }    
                
        }

        public void Close()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }
    }
}