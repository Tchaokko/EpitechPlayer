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
        [DllImport("user32.dll")]
        private static extern uint GetDoubleClickTime();
        private bool fullscreen = false;
        private DispatcherTimer DoubleClickTimer = new DispatcherTimer();
        private DispatcherTimer myDispatcher;
        public MainWindow()
        {
            DoubleClickTimer.Interval = TimeSpan.FromMilliseconds(GetDoubleClickTime());
            DoubleClickTimer.Tick += (s, e) => DoubleClickTimer.Stop();
            InitializeComponent();
            myMedia.Width = 500;
            myMedia.Height = 500;
            myMedia.Margin = new Thickness(100, 25, 0, 0);
        }

        private void Element_MediaEnded(object sender, EventArgs e)
        {
            myMedia.Stop();
        }

        private void doubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!DoubleClickTimer.IsEnabled)
            {
                DoubleClickTimer.Start();
            }
            else
            {
                if (!fullscreen)
                {
                    myMedia.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
                    myMedia.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
                    this.WindowStyle = WindowStyle.None;
                    this.WindowState = WindowState.Maximized;
                    myMedia.Margin = new Thickness(0, 0, 0, 0);
                    buttonPrev.Visibility = Visibility.Collapsed;
                    buttonPlay.Visibility = Visibility.Collapsed;
                    buttonNext.Visibility = Visibility.Collapsed;
                    volumeSlider.Visibility = Visibility.Collapsed;
                    timeline.Visibility = Visibility.Collapsed;
                    speedRatio.Visibility = Visibility.Collapsed;
                    mainMenu.Visibility = Visibility.Collapsed;
                }
                else
                {
                    myMedia.Width = 500;
                    myMedia.Height = 500;
                    myMedia.Margin = new Thickness(100,25,0,0);
                    this.WindowStyle = WindowStyle.SingleBorderWindow;
                    this.WindowState = WindowState.Normal;
                    buttonPrev.Visibility = Visibility.Visible;
                    buttonPlay.Visibility = Visibility.Visible;
                    buttonNext.Visibility = Visibility.Visible;
                    volumeSlider.Visibility = Visibility.Visible;
                    timeline.Visibility = Visibility.Visible;
                    speedRatio.Visibility = Visibility.Visible;
                    mainMenu.Visibility = Visibility.Visible;
                }

                fullscreen = !fullscreen;
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Updating the Label which displays the current second
            if (myMedia.NaturalDuration.HasTimeSpan)
            {
                totalTime.Content = myMedia.NaturalDuration.TimeSpan.ToString();
                string interm = myMedia.Position.ToString();
                if (interm.Length > 0)
                    interm = interm.Substring(0, interm.LastIndexOf("."));
                currentTime.Content = interm;
            }
            // Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested();
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
                myDispatcher = new System.Windows.Threading.DispatcherTimer();
                myDispatcher.Tick += new EventHandler(dispatcherTimer_Tick);
                myDispatcher.Interval = new TimeSpan(0, 0, 1);
                myDispatcher.Start();
                pathFile = dlg.FileName;
                myMedia.Source = new Uri(pathFile);
                myMedia.Play();
                if (myMedia.NaturalDuration.HasTimeSpan)
                 totalTime.Content = myMedia.NaturalDuration.TimeSpan.ToString();
                InitializePropertyValues();
                //myMedia.Stretch = Stretch.Fill;
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
            totalTime.Content = myMedia.NaturalDuration.ToString();
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

        }

        private void soundChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Console.WriteLine((int)volumeSlider.Value);

            myMedia.Volume = ((double)volumeSlider.Value / 100);
        }

 
        private void moveVideo(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int SliderValue = (int)timeline.Value;
            if (myMedia.NaturalDuration.HasTimeSpan)
            {
                TimeSpan interm = myMedia.NaturalDuration.TimeSpan;
                var totalTime = interm.TotalSeconds;
                Console.WriteLine(interm.TotalSeconds);
                var newTime = 0;
                if (SliderValue > 0)
                    newTime = ((int)totalTime * SliderValue) / 100;
                Console.WriteLine(newTime);
                TimeSpan ts = new TimeSpan(0, 0, 0, newTime, 0);
                myMedia.Position = ts;
                currentTime.Content = ts.ToString();
            }
        }

         private void speedRatioFunc(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            myMedia.SpeedRatio = (double)speedRatio.Value;
        }

        private void Element_MediaOpened(object sender, EventArgs e)
        {
            timeline.Maximum = myMedia.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        void InitializePropertyValues()
        {
            // Set the media's starting Volume and SpeedRatio to the current value of the
            // their respective slider controls.
            myMedia.Volume = (double)volumeSlider.Value;
            myMedia.SpeedRatio = (double)speedRatio.Value;
            if (myMedia.NaturalDuration.HasTimeSpan)
                totalTime.Content = myMedia.NaturalDuration.TimeSpan.ToString();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var newX = (myWindow.ActualHeight * 10) / 100;
            var newY = (myWindow.ActualHeight * 5) / 100;
            myMedia.Margin = new Thickness(newX, newY, 0, 0);
            myMedia.Height = (myWindow.ActualHeight * 80) / 100;
            myMedia.Width = (myWindow.ActualWidth * 90) / 100;
        }
        // When the media playback is finished. Stop() the media to seek to media start.
    }

}
