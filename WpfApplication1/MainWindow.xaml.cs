using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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
    }
}
