using java_menace.Quest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace java_menace.Characters
{
    class NPC : Character
    {
        public string[] Chat { get; set; }
        public QuestBase Quest { get; set; }

        public NPC(string String, string[] Chat, QuestBase Quest)
        {
            this.Chat = Chat;
            this.Quest = Quest;
            Views = new Dictionary<string, BitmapImage>()
            {
                { "0", new BitmapImage(new Uri("ms-appx:///Assets/npc/araguinho.png")) },
                { "1", new BitmapImage(new Uri("ms-appx:///Assets/npc/bacon.png")) },
                { "2", new BitmapImage(new Uri("ms-appx:///Assets/npc/ederum.png")) },
                { "3", new BitmapImage(new Uri("ms-appx:///Assets/npc/jolinho.png")) },
                { "4", new BitmapImage(new Uri("ms-appx:///Assets/npc/pede.png")) },
                { "5", new BitmapImage(new Uri("ms-appx:///Assets/npc/pike.png")) },
            };
            StartObject(Views[String], 48, 48, 0);
        }

    }
}
