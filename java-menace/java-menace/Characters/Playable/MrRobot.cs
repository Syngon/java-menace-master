using java_menace.Characters;
using java_menace.Itens.Hardwares;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Media.Imaging;

namespace java_menace
{
    class MrRobot : PlayableCharacter
    { 
        public MrRobot(string Username)
        {
            this.Username = Username;

            Views = new Dictionary<string, BitmapImage>()
            {
                { "Top", new BitmapImage(new Uri("ms-appx:///Assets/MrRobot/char_top_robot.png")) },
                { "Bottom", new BitmapImage(new Uri("ms-appx:///Assets/MrRobot/char_bottom_robot.png")) },
                { "Left", new BitmapImage(new Uri("ms-appx:///Assets/MrRobot/char_left_robot.png")) },
                { "Right", new BitmapImage(new Uri("ms-appx:///Assets/MrRobot/char_right_robot.png")) }
            };

            StartObject(Views["Bottom"], 48, 48);
        }
    }
}
