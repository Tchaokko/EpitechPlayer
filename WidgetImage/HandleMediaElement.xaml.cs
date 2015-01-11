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
    public partial class HandleMediaElement : Window
    {
        [DllImport("user32.dll")]
        private static extern uint GetDoubleClickTime();
        private bool fullscreen = false;
        private DispatcherTimer DoubleClickTimer = new DispatcherTimer();
        private DispatcherTimer myDispatcher;
        private bool dragStarted = false;
        private MainWindow _window;
        public Library    _library {set; get;}
        public List<Playlist>     _playlist { set; get; }
        public int _playlistSelected { get; set; }


        public HandleMediaElement(MainWindow window)
        {
            DoubleClickTimer.Interval = TimeSpan.FromMilliseconds(GetDoubleClickTime());
            DoubleClickTimer.Tick += (s, e) => DoubleClickTimer.Stop();
            InitializeComponent();
            timeline.Width = (gridControl.ActualWidth * 75) / 100;
            _window = window;
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
                    
                    this.WindowStyle = WindowStyle.None;
                    this.WindowState = WindowState.Maximized;
                    gridMedia.Margin = new Thickness(0, 0, 0, 0);
                    gridMedia.Height = System.Windows.SystemParameters.PrimaryScreenWidth;
                    gridMedia.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
                    myMedia.Margin = new Thickness(0, 0, 0, 0);
                    myMedia.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
                    myMedia.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
                    buttonPrev.Visibility = Visibility.Collapsed;
                    buttonPlay.Visibility = Visibility.Collapsed;
                    buttonNext.Visibility = Visibility.Collapsed;
                    buttonPause.Visibility = Visibility.Collapsed;
                    volumeSlider.Visibility = Visibility.Collapsed;
                    timeline.Visibility = Visibility.Collapsed;
                    mainMenu.Visibility = Visibility.Collapsed;
                    gridControl.Visibility = Visibility.Collapsed;
                }
                else
                {
                    var newY = (myWindow.ActualHeight * 10) / 100;
                    gridMedia.Margin = new Thickness(0, newY, 0, 0);
                    this.WindowStyle = WindowStyle.SingleBorderWindow;
                    this.WindowState = WindowState.Normal;
                    buttonPrev.Visibility = Visibility.Visible;
                    buttonPlay.Visibility = Visibility.Visible;
                    buttonNext.Visibility = Visibility.Visible;
                    buttonPause.Visibility = Visibility.Visible;
                    volumeSlider.Visibility = Visibility.Visible;
                    timeline.Visibility = Visibility.Visible;
                    mainMenu.Visibility = Visibility.Visible;
                    gridControl.Visibility = Visibility.Visible;
                    newY = (myWindow.ActualHeight * 10) / 100;
                    gridMedia.Margin = new Thickness(0, newY, 0, 0);
                    gridMedia.Height = (myWindow.ActualHeight * 60) / 100;
                    gridMedia.Width = myWindow.ActualWidth;
                    var newX = (gridMedia.ActualWidth * 5) / 100;
                    myMedia.Margin = new Thickness(newX, 0, 0, 0);
                    myMedia.Height = gridMedia.ActualHeight;
                    myMedia.Width = (gridMedia.ActualWidth * 90) / 100;
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
                Console.WriteLine(interm.Length);
                try
                {
                    interm = interm.Substring(0, interm.LastIndexOf(".")); //here need expetion
                }
                catch 
                {
                    Console.WriteLine("error = ", interm);
                    return; 
                }
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
                var newX = (gridMedia.ActualWidth * 5) / 100;
                myMedia.Margin = new Thickness(newX, 0, 0, 0);
                myMedia.Height = gridMedia.ActualHeight;
                myMedia.Width = (gridMedia.ActualWidth * 90) / 100;
                //myMedia.Stretch = Stretch.Fill;
            }
        }

        private void pauseMedia(object sender, RoutedEventArgs e)
        {
            // The Pause method pauses the media if it is currently running. 
            // The Play method can be used to resume.
            try
            {
                myMedia.Pause();
            }
            catch { return; }
        }

        private void playMedia(object sender, RoutedEventArgs e)
        {
            // The Play method will begin the media if it is not currently active or  
            // resume media if it is paused. This has no effect if the media is 
            // already running.
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

 

        private void soundChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Console.WriteLine((int)volumeSlider.Value);
            try { myMedia.Volume = ((double)volumeSlider.Value / 100); }
            catch { return; }
            
        }


        private void moveVideo(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int SliderValue = (int)timeline.Value;
            if (!dragStarted)
            {
                if (myMedia.NaturalDuration.HasTimeSpan)
                {
                    TimeSpan interm = myMedia.NaturalDuration.TimeSpan;
                    var totalTime = interm.TotalSeconds;
                    var newTime = 0;
                    if (SliderValue > 0)
                        newTime = ((int)totalTime * SliderValue) / 100;
                    TimeSpan ts = new TimeSpan(0, 0, 0, newTime, 0);
                    myMedia.Position = ts;
                    currentTime.Content = ts.ToString();
                }
            }
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
            myMedia.SpeedRatio = (double)1;
            if (myMedia.NaturalDuration.HasTimeSpan)
                totalTime.Content = myMedia.NaturalDuration.TimeSpan.ToString();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var newX = (myWindow.ActualWidth * 5) / 100;
            var newY = (myWindow.ActualHeight * 10) / 100;
            myMedia.Margin = new Thickness(newX, newY, 0, 0);
            myMedia.Height = (myWindow.ActualHeight * 80) / 100;
            myMedia.Width = (myWindow.ActualWidth * 90) / 100;
            newX = 0;
            newY = myWindow.ActualHeight - 100;
            gridControl.Margin = new Thickness(newX, newY, 0, 0);
            gridControl.Height = 60;
            gridControl.Width = myWindow.ActualWidth;
            newX = 0;
            newY = (myWindow.ActualHeight * 10) / 100;
            gridMedia.Margin = new Thickness(newX, newY, 0, 0);
            gridMedia.Height = (myWindow.ActualHeight * 60) / 100;
            gridMedia.Width = myWindow.ActualWidth;
            newX = (gridMedia.ActualWidth * 5) / 100;
            myMedia.Margin = new Thickness(newX, 0, 0, 0);
            myMedia.Height = gridMedia.ActualHeight;
            myMedia.Width = (gridMedia.ActualWidth * 90) / 100;
            timeline.Width = (gridControl.ActualWidth * 75) / 100;
            volumeSlider.Width = (gridControl.ActualWidth * 10) / 100;
            mainMenu.Width = this.Width;
        }
        // When the media playback is finished. Stop() the media to seek to media start.

        
        private void loadMenu(object sender, System.Windows.RoutedEventArgs e)
        {
            _window.Show();
            this.Hide();

        }

        private void loadLibrary(object sender, System.Windows.RoutedEventArgs e)
        {
            _library.Show();
            this.Hide();
        }     

        private void timeline_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            int SliderValue = (int)timeline.Value;
            Console.WriteLine("check");
            if (myMedia.NaturalDuration.HasTimeSpan)
            {
                TimeSpan interm = myMedia.NaturalDuration.TimeSpan;
                var totalTime = interm.TotalSeconds;
                var newTime = 0;
                if (SliderValue > 0)
                    newTime = ((int)totalTime * SliderValue) / 100;
                TimeSpan ts = new TimeSpan(0, 0, 0, newTime, 0);
                myMedia.Position = ts;
                currentTime.Content = ts.ToString();
            }
            dragStarted = false;
        }

        private void timeline_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            dragStarted = true;
        }

        private void buttonNext_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void buttonPrev_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void buttonFaster_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try {
                if (myMedia.SpeedRatio.CompareTo(3) != 1)
                {
                    Console.WriteLine("check 1");
                    myMedia.SpeedRatio += (double)1;
                }
             }
            catch { return; }
        }

        private void buttonSlower_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try {
                if (myMedia.SpeedRatio.CompareTo(1) != -1)
                {
                    Console.WriteLine("check 2");
                    myMedia.SpeedRatio -= (double)1; 
                }
            }
            catch { return; }
        }
    }
}