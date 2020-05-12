using System;
using System.Collections.Generic;
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
    public sealed partial class Settings : Page
    {
        public Settings()
        {
            this.InitializeComponent();
        }
        private void Play()
        {
            //string soundFile = @"Music/swordartmusic.wav";
            //_player = new System.Media.SoundPlayer(soundFile);
            //_player.Play();
        }

        private void Stop()
        {
            //if (_player != null)
            //{
            //    _player.Stop();
            //}
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            var frame = new Frame();
            frame.Navigate(typeof(Menu));
            Window.Current.Content = frame;
        }
    }


}
