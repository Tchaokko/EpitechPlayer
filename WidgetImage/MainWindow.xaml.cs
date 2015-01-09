using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
//using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using System.Windows.Shapes;

namespace WidgetImage
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void loadMediaPlayer(object sender, System.Windows.RoutedEventArgs e)
        {
            HandleMediaElement newWindow = new HandleMediaElement();

            newWindow.Show();
            this.Close();
            
        }

        private void loadLibrary(object sender, System.Windows.RoutedEventArgs e)
        {
            Library newWindow = new Library();

            newWindow.Show();
            this.Close();

        }
    }

}
