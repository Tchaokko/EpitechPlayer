using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private void loadFile(object sender, RoutedEventArgs e)
        {
            String pathFile = "";            
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document";
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            
            Nullable<bool> result = dlg.ShowDialog();
            
            if (result == true)
            {
                pathFile = dlg.FileName;
                System.Console.WriteLine("this =" + pathFile);
                fileName.Text = pathFile;
                myImage.Source = new BitmapImage(new Uri(pathFile));
            }
        }
    }
}
