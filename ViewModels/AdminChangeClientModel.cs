using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseProjectWPF.ViewModels
{
    class AdminChangeClientModel
    {
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

        public void Back()
        {
            Close();
        }

        public ICommand back => new DelegateCommand(Back);
    }
}