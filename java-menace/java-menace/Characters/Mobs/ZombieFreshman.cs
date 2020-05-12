using java_menace.Itens.Hardwares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace java_menace.Characters.Mobs
{
    class ZombieFreshman : Mob
    {
        public ZombieFreshman(int Level)
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


        //skin character
        public bool DrainEnergy(Notebook PlayerNotebook)
        {
            Random randNum = new Random();
            if (randNum.Next(3) != 2)
            {
                return false;
            }
            else
            {
                int x = randNum.Next(30, 60);
                if (Notebook.CurrentMana <= x)
                {
                    Notebook.CurrentMana = 0;
                }
                else
                {
                    Notebook.CurrentMana -= x;
                }
                return true;
            }
        }
    }
}