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
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace java_menace
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class Menu : Page
    {
        public Menu()
        {
            this.InitializeComponent();
        }
        private void MoveToSettings(object sender, RoutedEventArgs e)
        {
            var frame = new Frame();
            frame.Navigate(typeof(Settings));
            Window.Current.Content = frame;
        }
        private void Play(object sender, RoutedEventArgs e)
        {
            var frame = new Frame();
            frame.Navigate(typeof(CharSelection));
            Window.Current.Content = frame;
        }

        private void MoveToCredits(object sender, RoutedEventArgs e)
        {
            var frame = new Frame();
            frame.Navigate(typeof(Credits));
            Window.Current.Content = frame;
            
        }
    }
}