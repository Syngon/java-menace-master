using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace java_menace.Characters.Mobs
{
    class TruffleMan : Mob
    {
        public TruffleMan(int Level)
        {
           
            Views = new Dictionary<string, BitmapImage>()
            {
                { "Top", new BitmapImage(new Uri("ms-appx:///Assets/MrRobot/char_top_robot.png")) },
                { "Bottom", new BitmapImage(new Uri("ms-appx:///Assets/MrRobot/char_bottom_robot.png")) },
                { "Left", new BitmapImage(new Uri("ms-appx:///Assets/MrRobot/char_left_robot.png")) },
                { "Right", new BitmapImage(new Uri("ms-appx:///Assets/MrRobot/char_right_robot.png")) }
            };

            StartObject(Views["Bottom"], 48, 48);
            this.Notebook.Level = Level;
            SetStatus();
        }

        //Skin character
        public bool SellTruffle(Double PlayerCarteiraJLC)
        {
            Random randNum = new Random();
            if (randNum.Next(6) != 5)
            {
                return false;
            }
            if (PlayerCarteiraJLC < 50)
            {
                PlayerCarteiraJLC = 0;
                
            }
            else
            {
                PlayerCarteiraJLC -= 50;
                
            }
            return true;
        }
    }
}
