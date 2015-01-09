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
        public Library()
        {
            InitializeComponent();

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
            HandleMediaElement newWindow = new HandleMediaElement();

            newWindow.Show();
            this.Close();

        }

        private void loadMenu(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow newWindow = new MainWindow();

            newWindow.Show();
            this.Close();

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            mainMenu.Width = this.Width;
        }
    }
}