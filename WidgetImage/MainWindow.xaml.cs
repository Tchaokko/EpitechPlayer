﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
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

    public partial class MainWindow : Window
    {
        private HandleMediaElement _mediaPlayer;
        private Library            _library;
        private bool _charlie;
        private List<Playlist> _playlist;
        private List<Button> _listButton;
        private int _playlistSelected;

        public MainWindow()
        {
            InitializeComponent();
            _playlist = new List<Playlist>();
            _listButton = new List<Button>();
            _mediaPlayer = new HandleMediaElement(this);
            _library = new Library(this);           
            _mediaPlayer._library = _library;
            _library._mediaPlayer = _mediaPlayer;

            _library._playlist = _playlist;
            _mediaPlayer._playlist = _playlist;

            _playlistSelected = -1;
            _library._playlistSelected = _playlistSelected;
            _mediaPlayer._playlistSelected = _playlistSelected;
            
            _charlie = false;
            Charlie.Source = null;            
        }

        private void loadMediaPlayer(object sender, System.Windows.RoutedEventArgs e)
        {         
            _mediaPlayer.Show();
            this.Hide();            
        }

        private void loadLibrary(object sender, System.Windows.RoutedEventArgs e)
        {        
            _library.Show();
            this.Hide();
        }

        private void loadCharlie(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Charlie.Source == null)
            {
                BitmapImage src = new BitmapImage();
                src.BeginInit();
                src.UriSource = new Uri("../../ressources/Charlie.jpg", UriKind.Relative);
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.EndInit();
                Charlie.Source = src;
            }

            if (_charlie == false)
                Charlie.Visibility = Visibility.Visible;                            
            else            
                Charlie.Visibility = Visibility.Hidden;                            
            _charlie = (_charlie ? false : true);
        }

        private void renamePlaylist(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_playlistSelected == -1)
                return;
            TextRename.Visibility = Visibility.Visible;
            ValidRenameBTN.Visibility = Visibility.Visible;
        }
        private void validRename(object sender, System.Windows.RoutedEventArgs e)
        {
            TextRename.Visibility = Visibility.Hidden;
            ValidRenameBTN.Visibility = Visibility.Hidden;
            String str = TextRename.Text;
            if ((str.Length > 3) && (str.Length < 10))
            {
                _playlist[_playlistSelected]._name = str;
                _listButton[_playlistSelected].Content = str;
                TextRename.Text = "";
                Selection.Text = str;
            }
        }

        private void selectPlaylist(object sender, System.Windows.RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            _playlistSelected = Convert.ToInt16(btn.Uid);
            _library._playlistSelected = _playlistSelected;
            Selection.Text = /*_playlist[_playlistSelected]._name;*/ "" + _playlistSelected;
            if (_playlistSelected != -1)
            {
                Console.WriteLine("select : "+ _playlistSelected);
                Playlist mylist = _playlist.ElementAt(_playlistSelected);                
                ListContent.ItemsSource = "";
                ListContent.ItemsSource = mylist._playlist;                
            }
            _library._playlistSelected = _playlistSelected;
            _mediaPlayer._playlistSelected = _playlistSelected;
        }

        private void addPlaylist(object sender, System.Windows.RoutedEventArgs e)
        {
            Button btn;

            if (_playlist.Count >= 10)
                return;       
            btn = new Button();
            btn.Content = "Playlist " + _playlist.Count();
            btn.Uid = ("" + _playlist.Count);
            btn.Click += new RoutedEventHandler(selectPlaylist);
            _listButton.Add(btn);
            ListPlaylist.ItemsSource = _listButton;
            ListPlaylist.Items.Refresh();
            _playlist.Add(new Playlist(_playlist.Count() + 1));            
            ListPlaylist.Height += 22;
            _library._playlist = _playlist;
            _mediaPlayer._playlist = _playlist;
        }

        private void removePlaylist(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_playlist.Count() <= 0)
                return;
            if (_playlistSelected != -1)
            {
                try
                {                   
                    ListPlaylist.Height -= 22;
                    _playlist.RemoveAt(_playlistSelected);
                    _listButton.RemoveAt(_playlistSelected);                    
                    while (_playlistSelected < _listButton.Count())
                    {                        
                        int i = Convert.ToInt16(_listButton[_playlistSelected].Uid) - 1;
                        _listButton[_playlistSelected].Uid = "" + i;                     
                        ++_playlistSelected;
                    }
                }
                catch { };
                _playlistSelected = -1;
                Selection.Text = "";
            }
            else
            {
                try
                {                    
                    ListPlaylist.Height -= 22;
                    _playlist.RemoveAt(_playlist.Count - 1);
                    _listButton.RemoveAt(_listButton.Count - 1);                 
                }
                catch { };
            }
            ListPlaylist.ItemsSource = "";
            ListPlaylist.ItemsSource = _listButton;
            if (_playlistSelected >= _listButton.Count)
            {
                _playlistSelected = -1;
                Selection.Text = "";
            }
            _library._playlistSelected = _playlistSelected;
            _mediaPlayer._playlistSelected = _playlistSelected;
        }

        private void loadTheme(object sender, System.Windows.RoutedEventArgs e)
        {
            MenuItem elem = (MenuItem)sender;
            String str = elem.Header as String;
            if (str != null && str != "")
            {
                switch (str)
                {
                    case ("Violet Theme"):
                        appBackground.Background = Brushes.DarkViolet;
                        _library.appBackground.Background = Brushes.DarkViolet;
                        _mediaPlayer.appBackground.Background = Brushes.DarkViolet;
                        break;
                    case ("Blue Theme"):
                        appBackground.Background = Brushes.LightSkyBlue;
                        _library.appBackground.Background = Brushes.LightSkyBlue;
                        _mediaPlayer.appBackground.Background = Brushes.LightSkyBlue;
                        break;                        
                    default:
                        appBackground.Background = Brushes.DimGray;
                        _library.appBackground.Background = Brushes.DimGray;
                        _mediaPlayer.appBackground.Background = Brushes.DimGray;
                        break;
                }
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            mainMenu.Width = this.Width;
        }
    }

}
