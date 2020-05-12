using java_menace.Itens.Hardwares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace java_menace.Characters
{
    class Linus : PlayableCharacter
    {
        public Linus(string Username)
        {
            this.Username = Username;

            Views = new Dictionary<string, BitmapImage>()
            {
                { "Top", new BitmapImage(new Uri("ms-appx:///Assets/Linus/char_top_linux.png")) },
                { "Bottom", new BitmapImage(new Uri("ms-appx:///Assets/Linus/char_bottom_linux.png")) },
                { "Left", new BitmapImage(new Uri("ms-appx:///Assets/Linus/char_left_linux.png")) },
                { "Right", new BitmapImage(new Uri("ms-appx:///Assets/Linus/char_right_linux.png")) }
            };

            StartObject(Views["Bottom"], 48, 48);
        }
    }
}
