using System;
using System.IO;
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
using System.Windows.Navigation;

namespace WidgetImage
{
    public partial class Library : Window
    {
        private MainWindow _window;
        public HandleMediaElement _mediaPlayer { get; set; }

        public Library(MainWindow window)
        {
            InitializeComponent();
            _window = window;
        }

        private void Button_Musics(object sender, RoutedEventArgs e)
        {
            string path;
            path = Environment.GetEnvironmentVariable("USERPROFILE") + "\\Music";
            Console.WriteLine(path);
            DirectoryInfo info = new DirectoryInfo(path);
            if (info.Exists)
            {
                myTree.ItemsSource = info.GetFiles();
            }
        }

        private void Button_Videos(object sender, RoutedEventArgs e)
        {
            string path;
            path = Environment.GetEnvironmentVariable("USERPROFILE") + "\\Videos";
            Console.WriteLine(path);
            DirectoryInfo info = new DirectoryInfo(path);
            if (info.Exists)
            {
                myTree.ItemsSource = info.GetFiles();
            }
        }

        private void Button_Image(object sender, RoutedEventArgs e)
        {
            string path;
            path = Environment.GetEnvironmentVariable("USERPROFILE") + "\\Pictures";
            Console.WriteLine(path);
            DirectoryInfo info = new DirectoryInfo(path);
            if (info.Exists)
            {
                myTree.ItemsSource = info.GetFiles();
            }
        }

        private void MouseDoubleClickMyTree(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine(myTree.ToString());
        }

        private void loadMediaPlayer(object sender, System.Windows.RoutedEventArgs e)
        {
            _mediaPlayer.Show();
            this.Hide();
        }

        private void loadMenu(object sender, System.Windows.RoutedEventArgs e)
        {         
            _window.Show();
            this.Hide();

        }
    }
}