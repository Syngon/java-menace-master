using java_menace.Itens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace java_menace.Characters
{
    class Veteran : Mob
    {
        public Veteran(int Level)
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
        public bool StealItem(Inventory PlayerInveotry)
        {
            Random randNum = new Random();
            if (randNum.Next(6) != 5)
            {
                return false;
            }
            else
            {
                int qnt = Inventory.HardwareInventory.Count;
                int index = randNum.Next(qnt);
                Inventory.HardwareInventory.RemoveAt(index);
            }
            return true;
        }
    }
}
