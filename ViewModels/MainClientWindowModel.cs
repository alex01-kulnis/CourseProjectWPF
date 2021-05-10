using CourseProjectWPF.Views;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CourseProjectWPF.ViewModels
{
    class MainClientWindowModel: ViewModelBase
    {        
        public MainClientWindowModel()
        {
            
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

        public ICommand ChangeAcc => new DelegateCommand(Changeacc);

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
    }
}