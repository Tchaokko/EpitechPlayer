﻿using System;
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
                List<Label> list = new List<Label>();
                foreach (string str in Directory.GetFiles(path))
                {
                    Label tmp = new Label();
                    tmp.Content = str;
                    list.Add(tmp);
            }
                myTree.ItemsSource = list;

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
                List<MediaElement> list = new List<MediaElement>();
                foreach (string str in Directory.GetFiles(path))
                {
                    MediaElement tmp = new MediaElement();
                    tmp.Source = new Uri(str);
                    tmp.LoadedBehavior = MediaState.Manual;

                    if (tmp.NaturalDuration.HasTimeSpan)
                    {
                        TimeSpan interm = tmp.NaturalDuration.TimeSpan;
                        if (interm.TotalSeconds > 5)
                        {
                            var newTime = ((int)interm.TotalSeconds * 5) / 100;
                            TimeSpan ts = new TimeSpan(0, 0, 0, newTime, 0);
                            tmp.Position = ts;
                        }
                    }
                    tmp.Height = 100;
                    tmp.Width = 100;
                    tmp.Pause();  
                    //tmp.Content = str;
                    list.Add(tmp);
                 }
                
                myTree.ItemsSource = list;

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
                List<MediaElement> list = new List<MediaElement>();
                foreach (string str in Directory.GetFiles(path))
                {
                    MediaElement tmp = new MediaElement();
                    tmp.Source = new Uri(str);
                    if (tmp.Height > 100)
                        tmp.Height = 100;
                    if (tmp.Width > 100)
                        tmp.Width = 100;
                    list.Add(tmp);
                }
                myTree.ItemsSource = list;
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

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            mainMenu.Width = this.Width;
        }
    }
}