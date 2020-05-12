using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace java_menace
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class CharSelection : Page
    {
        public string name;
        public int Index { get; set; } = 0;

        private List<BitmapImage> Faces = new List<BitmapImage>()
        {
            new BitmapImage(new Uri("ms-appx:///Assets/Menu/Personagens/Linus.png")),
            new BitmapImage(new Uri("ms-appx:///Assets/Menu/Personagens/MrRobot.png")),
            new BitmapImage(new Uri("ms-appx:///Assets/Menu/Personagens/SteveJobs.png"))
        };

        private List<BitmapImage> Chars = new List<BitmapImage>() {
           new BitmapImage(new Uri("ms-appx:///Assets/Linus/char_bottom_linux.png")),
           new BitmapImage(new Uri("ms-appx:///Assets/MrRobot/char_bottom_robot.png")),
           new BitmapImage(new Uri("ms-appx:///Assets/Steve/char_bottom_jobs.png"))
        };

        private List<BitmapImage> Names = new List<BitmapImage>()
        {
            new BitmapImage(new Uri("ms-appx:///Assets/Menu/NomePersonagens/Linus.png")),
            new BitmapImage(new Uri("ms-appx:///Assets/Menu/NomePersonagens/MrRobot.png")),
            new BitmapImage(new Uri("ms-appx:///Assets/Menu/NomePersonagens/SteveJobs.png"))
        };

        public CharSelection()
        {
            this.InitializeComponent();
            img_face.Source = Faces[Index];
            img_char.Source = Chars[Index];
            img_char_name.Source = Names[Index];
            Name = tb_name.Text;
        }

        private void SetImg(int Index)
        {
            img_face.Source = Faces[Index];
            img_char.Source = Chars[Index];
            img_char_name.Source = Names[Index];
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            var frame = new Frame();
            frame.Navigate(typeof(Menu));
            Window.Current.Content = frame;
        }

        private void Prev(object sender, RoutedEventArgs e)
        {
            if (Index - 1 < 0) Index = 2;
            else Index--;
            SetImg(Index);
        }

        private void Next(object sender, RoutedEventArgs e)
        {
            if (Index + 1 > 2) Index = 0;
            else Index++;
            SetImg(Index);
        }

        private void Play(object sender, RoutedEventArgs e)
        {
            var frame = new Frame();
            frame.Navigate(typeof(MainPage), new Tuple<int, string>(Index, tb_name.Text));
            Window.Current.Content = frame;
        }
    }
}
