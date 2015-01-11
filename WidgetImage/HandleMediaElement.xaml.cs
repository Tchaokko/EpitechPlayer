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
        private List<string> myPlayList;
        private int itePlayList;
        private bool ifPlaylist;
        private bool autoMove = false;
        private bool isPLaying = false;

        public HandleMediaElement(MainWindow window)
        {
            DoubleClickTimer.Interval = TimeSpan.FromMilliseconds(GetDoubleClickTime());
            DoubleClickTimer.Tick += (s, e) => DoubleClickTimer.Stop();
            InitializeComponent();
            timeline.Width = (gridControl.ActualWidth * 75) / 100;
            _window = window;
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
            if (myMedia.NaturalDuration.HasTimeSpan)
            {
                totalTime.Content = myMedia.NaturalDuration.TimeSpan.ToString();
                string interm = myMedia.Position.ToString();
                try
                {
                    interm = interm.Substring(0, interm.LastIndexOf("."));
                    autoMove = true;
                    timeline.Value = (myMedia.Position.TotalSeconds / myMedia.NaturalDuration.TimeSpan.TotalSeconds) * 100;
                    autoMove = false;
                }
                catch 
                {
                    return; 
                }
                currentTime.Content = interm;
            }
            CommandManager.InvalidateRequerySuggested();
        }



        private void loadTheFile(string pathFile)
        {
            myDispatcher = new System.Windows.Threading.DispatcherTimer();
            myDispatcher.Tick += new EventHandler(dispatcherTimer_Tick);
            myDispatcher.Interval = new TimeSpan(0, 0, 1);
            myDispatcher.Start();
            myMedia.Source = new Uri(pathFile);
            isPLaying = true;
            myMedia.Play();
            if (myMedia.NaturalDuration.HasTimeSpan)
                totalTime.Content = myMedia.NaturalDuration.TimeSpan.ToString();
            InitializePropertyValues();
            var newX = (gridMedia.ActualWidth * 5) / 100;
            myMedia.Margin = new Thickness(newX, 0, 0, 0);
            myMedia.Height = gridMedia.ActualHeight;
            myMedia.Width = (gridMedia.ActualWidth * 90) / 100;
        }

        private void pauseMedia(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isPLaying)
                {
                    myMedia.Pause();
                    isPLaying = false;
                }
            }
            catch { return; }
        }

        private void playMedia(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isPLaying)
                {
                    myMedia.Play();
                    isPLaying = true;
                }
                else
                {
                    myMedia.Pause();
                    isPLaying = false;
                }
                 totalTime.Content = myMedia.NaturalDuration.ToString();
            }
            catch { return; }

        }

        private void soundChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try { myMedia.Volume = ((double)volumeSlider.Value / 100); }
            catch { return; }
            
        }


        private void moveVideo(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int SliderValue = (int)timeline.Value;
            if (!dragStarted && !autoMove)
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

        
        private void loadMenu(object sender, System.Windows.RoutedEventArgs e)
        {
            _window.Show();
            myMedia.Pause();
            isPLaying = false;
            this.Hide();

        }

        private void loadLibrary(object sender, System.Windows.RoutedEventArgs e)
        {
            _library.Show();
            myMedia.Pause();
            isPLaying = false;
            this.Hide();
        }     

        private void timeline_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            int SliderValue = (int)timeline.Value;
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
            if (ifPlaylist && (itePlayList < (myPlayList.Count - 1)))
            {
                itePlayList += 1;
                if (!String.IsNullOrEmpty(myPlayList.ElementAt(itePlayList)))
                    loadTheFile(myPlayList.ElementAt(itePlayList));
                else
                {
                    itePlayList = 1;
                    ifPlaylist = false;
                }
            }
        }

        private void buttonPrev_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ifPlaylist && itePlayList > 0)
            {
                itePlayList -= 1;
                if (!String.IsNullOrEmpty(myPlayList.ElementAt(itePlayList)))
                    loadTheFile(myPlayList.ElementAt(itePlayList));
            }
        }

        private void buttonFaster_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try {
                if (myMedia.SpeedRatio.CompareTo(3) != 1)
                {
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
                    myMedia.SpeedRatio -= (double)1; 
                }
            }
            catch { return; }
        }

        private void loadPlaylist(object sender, RoutedEventArgs e)
        {
            if (_playlistSelected != -1)
            {
                itePlayList = 0;
                ifPlaylist = true;
                myPlayList = (_playlist.ElementAt(_playlistSelected))._playlist;
                loadTheFile(myPlayList.ElementAt(itePlayList));
            }
        }

        private void loadFile(object sender, RoutedEventArgs e)
        {
            String pathFile = "";

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document";
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                pathFile = dlg.FileName;
                ifPlaylist = false;
                loadTheFile(pathFile);
            }
        }

        private void myMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (!ifPlaylist)
            {
                myMedia.Stop();
            }
            else
            {
                if ((itePlayList < (myPlayList.Count - 1)))
                {
                    itePlayList += 1;
                    if (!String.IsNullOrEmpty(myPlayList.ElementAt(itePlayList)))
                        loadTheFile(myPlayList.ElementAt(itePlayList));
                    else
                    {
                        itePlayList = 1;
                        ifPlaylist = false;
                    }
                }
            }
        }

        private void myWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Space)
            {
                if (isPLaying)
                {
                    isPLaying = false;
                    myMedia.Pause();
                }
                else
                {
                    isPLaying = true;
                    myMedia.Play();
                }
            }
        }
    
    }
}