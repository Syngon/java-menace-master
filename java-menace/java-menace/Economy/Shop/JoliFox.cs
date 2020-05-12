using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace java_menace.Economy.Shop
{
    class JoliFox : Character
    {
        public Jolishop Jolishop { get; set; }

        public JoliFox()
        {
            this.Jolishop = new Jolishop();

            Views = new Dictionary<string, BitmapImage>()
            {
                { "Top", new BitmapImage(new Uri("ms-appx:///Assets/JoliRaposa/char_top_fox.png")) },
                { "Bottom", new BitmapImage(new Uri("ms-appx:///Assets/JoliRaposa/char_bottom_fox.png")) },
                { "Left", new BitmapImage(new Uri("ms-appx:///Assets/JoliRaposa/char_left_fox.png")) },
                { "Right", new BitmapImage(new Uri("ms-appx:///Assets/JoliRaposa/char_right_fox.png")) }
            };

            StartObject(Views["Bottom"], 48, 48);
        }
    }
}
