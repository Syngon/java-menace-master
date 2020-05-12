using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace java_menace.Economy.Shop
{
    class CoffeMachine : Jolishop
    {
        public CoffeMachine(string Direction)
        {
            Views = new Dictionary<string, BitmapImage>()
            {
                { "Top", new BitmapImage(new Uri("ms-appx:///Assets/CoffeeMachine/vending_machine1.png")) },
                { "Bottom", new BitmapImage(new Uri("ms-appx:///Assets/CoffeeMachine/vending_machine2.png")) },
                { "Left", new BitmapImage(new Uri("ms-appx:///Assets/JoliRaposa/char_left_fox.png")) },
                { "Right", new BitmapImage(new Uri("ms-appx:///Assets/JoliRaposa/char_right_fox.png")) }
            };

            if(Direction == "Top")
            {
                StartObject(Views[Direction], 96, 96, 96);
            }
            else
            {
                StartObject(Views[Direction], 48, 96, 48);
            }
            
        }
    }
}
