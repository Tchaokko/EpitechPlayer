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
        HandleMediaElement _mediaPlayer;
        Library            _library;


        public MainWindow()
        {
            InitializeComponent();
            _mediaPlayer = new HandleMediaElement(this);
            _library = new Library(this);
            _mediaPlayer._library = _library;
            _library._mediaPlayer = _mediaPlayer;
        }

        private void loadMediaPlayer(object sender, System.Windows.RoutedEventArgs e)
        {         
            _mediaPlayer.Show();
            this.Hide();            
        }

        private void loadLibrary(object sender, System.Windows.RoutedEventArgs e)
        {        
            _library.Show();
            this.Hide();
        }
    }

}
