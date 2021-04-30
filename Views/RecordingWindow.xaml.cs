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
    /// Логика взаимодействия для RecordingWindow.xaml
    /// </summary>
    public partial class RecordingWindow : UserControl
    {
        public RecordingWindow(MainClientWindow mainClientWindow)
        {
            InitializeComponent();
        }

        private void FindTime(object sender, RoutedEventArgs e)
        {
            if (DataVisiting.Text != "")
            {

                Error_Message.Text = "";                    
            }
            else
            {
                Error_Message.Text = "Выберите дату";
            }
            
        }
    }
}
