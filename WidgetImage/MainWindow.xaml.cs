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

        private void Element_MediaEnded(object sender, EventArgs e)
        {
            myMedia.Stop();
        }

        private void loadFile(object sender, RoutedEventArgs e)
        {
            String pathFile = "";
            
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document";
           // dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            Nullable<bool> result = dlg.ShowDialog();
            
            if (result == true)
            {
                pathFile = dlg.FileName;
                fileName.Text = pathFile;
                myMedia.Source = new Uri(pathFile);
        }
        }

        private void pauseMedia(object sender, RoutedEventArgs e)
        {
            // The Pause method pauses the media if it is currently running. 
            // The Play method can be used to resume.
            Console.WriteLine("Pause");
            myMedia.Pause();
        }

        private void playMedia(object sender, RoutedEventArgs e)
        {
            // The Play method will begin the media if it is not currently active or  
            // resume media if it is paused. This has no effect if the media is 
            // already running.
            Console.WriteLine("Play");
            myMedia.Play();

            // Initialize the MediaElement property values.
            //InitializePropertyValues();
        }

        private void prevMedia(object sender, RoutedEventArgs e)
        {
            // The Play method will begin the media if it is not currently active or  
            // resume media if it is paused. This has no effect if the media is 
            // already running.
            //myMedia.Play();

            // Initialize the MediaElement property values.
            //InitializePropertyValues();
        }

        private void buttonPrev_MouseDown(object sender, MouseButtonEventArgs e)
        {
            String pathFile = "";
            
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document";
           // dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            
            Nullable<bool> result = dlg.ShowDialog();
            
            if (result == true)
            {
                pathFile = dlg.FileName;
                fileName.Text = pathFile;
                myMedia.Source = new Uri(pathFile);
            }
        }
    }
}
