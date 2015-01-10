using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
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

    public partial class MainWindow : Window
    {
        HandleMediaElement _mediaPlayer;
        Library            _library;
        private bool _charlie;


        public MainWindow()
        {
            InitializeComponent();
            _mediaPlayer = new HandleMediaElement(this);
            _library = new Library(this);
            _mediaPlayer._library = _library;
            _library._mediaPlayer = _mediaPlayer;
            _charlie = false;
            Charlie.Source = null;
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

        private void loadCharlie(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Charlie.Source == null)
            {
                BitmapImage src = new BitmapImage();
                src.BeginInit();
                src.UriSource = new Uri("../../ressources/Charlie.jpg", UriKind.Relative);
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.EndInit();
                Charlie.Source = src;
            }

            if (_charlie == false)
                Charlie.Visibility = Visibility.Visible;                            
            else            
                Charlie.Visibility = Visibility.Hidden;                            
            _charlie = (_charlie ? false : true);
        }

        private void loadTheme(object sender, System.Windows.RoutedEventArgs e)
        {
            MenuItem elem = (MenuItem)sender;
            String str = elem.Header as String;
            if (str != null && str != "")
            {
                switch (str)
                {
                    case ("Violet Theme"):
                        appBackground.Background = Brushes.DarkViolet;
                        _library.appBackground.Background = Brushes.DarkViolet;
                        _mediaPlayer.appBackground.Background = Brushes.DarkViolet;
                        break;
                    case ("Blue Theme"):
                        appBackground.Background = Brushes.DarkBlue;
                        _library.appBackground.Background = Brushes.DarkBlue;
                        _mediaPlayer.appBackground.Background = Brushes.DarkBlue;
                        break;                        
                    default:
                        appBackground.Background = Brushes.DimGray;
                        _library.appBackground.Background = Brushes.DimGray;
                        _mediaPlayer.appBackground.Background = Brushes.DimGray;
                        break;
                }
            }
        }
    }

}
