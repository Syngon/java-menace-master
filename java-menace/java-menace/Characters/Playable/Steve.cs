using java_menace.Itens.Hardwares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace java_menace.Characters
{
    class Steve : PlayableCharacter
    {
        public Steve(string Username)
        {
            this.Username = Username;

            Views = new Dictionary<string, BitmapImage>()
            {
                { "Top", new BitmapImage(new Uri("ms-appx:///Assets/Steve/char_top_jobs.png")) },
                { "Bottom", new BitmapImage(new Uri("ms-appx:///Assets/Steve/char_bottom_jobs.png")) },
                { "Left", new BitmapImage(new Uri("ms-appx:///Assets/Steve/char_left_jobs.png")) },
                { "Right", new BitmapImage(new Uri("ms-appx:///Assets/Steve/char_right_jobs.png")) }
            };

            StartObject(Views["Bottom"], 48, 48);
        }
    }
}
