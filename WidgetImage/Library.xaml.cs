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
        public List<Playlist> _playlist { get; set; }
        public int _playlistSelected { get; set; }
        public List<MyData> list { get; set; }

        public Library(MainWindow window)
        {
            InitializeComponent();
            _window = window;
        }

        private void Button_Musics(object sender, RoutedEventArgs e)
        {
            try
            {
                ImageAperçus.Visibility = System.Windows.Visibility.Hidden;
                string path;
                path = Environment.GetEnvironmentVariable("USERPROFILE") + "\\Music";
                DirectoryInfo info = new DirectoryInfo(path);
                if (info.Exists)
                {
                    List<MyData> tmplist = new List<MyData>();
                    var converter = new BrushConverter();
                    Int16 _uid = 0;
                    foreach (string str in Directory.GetFiles(path))
                    {
                        if (str.EndsWith(".mp3") || str.EndsWith(".wma"))
                        {
                            FileInfo _info = new FileInfo(str);
                            MyData tmp = new MyData();
                            tmp.myPath = new Label();
                            tmp.myName = new Label();
                            tmp.myName.Content = _info.Name;
                            tmp.Add = new Button();
                            tmp.myPath.Content = str;
                            tmp.myName.MouseDown += new MouseButtonEventHandler(Label_Music);
                            tmp.Data1.MouseDown += new MouseButtonEventHandler(Label_Music);
                            tmp.Add.Content = "Add to Playlist";
                            tmp.Add.Click += new RoutedEventHandler(Add_To_Playlist);
                            tmp.Add.FontWeight = System.Windows.FontWeights.Bold;
                            tmp.Add.Uid = _uid.ToString();
                            tmp.Add.Background = (Brush)converter.ConvertFrom("#FFE2E2E2");
                            tmplist.Add(tmp);
                            _uid += 1;
                        }
                    }
                    list = tmplist;
                    myListBox.ItemsSource = list;
                }
            }
            catch
            {
                return ;
            }
        }

        private void Label_Music(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ImageAperçus.Source = new BitmapImage(new Uri("../../ressources/note.png", UriKind.Relative));
                ImageAperçus.Visibility = System.Windows.Visibility.Visible;
            }
            catch
            {
                return ;
            }
        }

        private void Button_Videos(object sender, RoutedEventArgs e)
        {
            //try
            //{
                ImageAperçus.Visibility = System.Windows.Visibility.Hidden;
                string path;
                path = Environment.GetEnvironmentVariable("USERPROFILE") + "\\Videos";
                DirectoryInfo info = new DirectoryInfo(path);
                if (info.Exists)
                {
                    List<MyData> tmplist = new List<MyData>();
                    var converter = new BrushConverter();
                    Int16 _uid = 0;
                    foreach (string str in Directory.GetFiles(path))
                    {
                        if (str.EndsWith(".mp4") || str.EndsWith(".mkv")
                            || str.EndsWith(".avi") || str.EndsWith(".wmv"))
                        {
                            FileInfo _info = new FileInfo(str);
                            MyData tmp = new MyData();
                            tmp.myPath = new Label();
                            tmp.Add = new Button();
                            tmp.myName = new Label();
                            tmp.myName.Content = _info.Name; tmp.myPath.Content = str;
                            tmp.myName.MouseDown += new MouseButtonEventHandler(Label_Video);
                            tmp.Add.Content = "Add to Playlist";
                            tmp.Add.FontWeight = System.Windows.FontWeights.Bold;
                            tmp.Add.Background = (Brush)converter.ConvertFrom("#FFE2E2E2");
                            tmp.Add.Uid = _uid.ToString();
                            tmp.Add.Click += new RoutedEventHandler(Add_To_Playlist);
                            tmplist.Add(tmp);
                            _uid += 1;
                        }
                    }
                    list = tmplist;
                    myListBox.ItemsSource = list;
                }
            //}
            //catch
            //{
            //    return ;
            //}
        }

        private void Label_Video(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ImageAperçus.Source = new BitmapImage(new Uri("../../ressources/Pellicule.png", UriKind.Relative));
                ImageAperçus.Visibility = System.Windows.Visibility.Visible;
            }
            catch
            {
                return ;
            }
        }

        private void Button_Image(object sender, RoutedEventArgs e)
        {
            try
            {
                ImageAperçus.Visibility = System.Windows.Visibility.Hidden;
                string path;
                path = Environment.GetEnvironmentVariable("USERPROFILE") + "\\Pictures";
                DirectoryInfo info = new DirectoryInfo(path);
                if (info.Exists)
                {
                    List<MyData> tmplist = new List<MyData>();
                    var converter = new BrushConverter();
                    Int16 _uid = 0;
                    foreach (string str in Directory.GetFiles(path))
                    {
                        if (str.EndsWith(".png") || str.EndsWith(".bmp")
                            || str.EndsWith(".jpeg") || str.EndsWith(".jpg"))
                        {
                            FileInfo _info = new FileInfo(str);
                            MyData tmp = new MyData();
                            tmp.myPath = new Label();
                            tmp.Add = new Button();
                            tmp.Add.Content = "Add to Playlist";
                            tmp.myName = new Label();
                            tmp.myName.Content = _info.Name;
                            tmp.Add.Click += new RoutedEventHandler(Add_To_Playlist);
                            tmp.myPath.Content = str;
                            tmp.Add.FontWeight = System.Windows.FontWeights.Bold;
                            tmp.Add.Background = (Brush)converter.ConvertFrom("#FFE2E2E2");
                            tmp.Add.Uid = _uid.ToString();
                            tmp.myName.Uid = _uid.ToString();
                            tmp.myName.MouseDown += new MouseButtonEventHandler(Label_Picture);
                            tmplist.Add(tmp);
                            _uid += 1;
                        }
                    }
                    list = tmplist;
                    myListBox.ItemsSource = list;
                }
            }
            catch
            {
                return;
            }
        }

        private void Add_To_Playlist(object sender, RoutedEventArgs e)
        {
            try
            {
                int tmp = Convert.ToInt16(((Button)sender).Uid);
                _playlist[_playlistSelected]._playlist.Add(list[tmp].myPath.Content.ToString());
            }
            catch
            {
                return;
            }
        }

        private void Label_Picture(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Int16 tmp = Convert.ToInt16(((Label)sender).Uid);
                ImageAperçus.Source = new BitmapImage(new Uri(list[tmp].myPath.Content.ToString()));
                ImageAperçus.Visibility = System.Windows.Visibility.Visible;  
            }
            catch
            {
                return ;
            }
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

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            mainMenu.Width = this.Width;
        }

    }
}