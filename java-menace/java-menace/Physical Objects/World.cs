using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace java_menace.Physical_Objects
{
    class World : PhysicalObject
    {
        public World(string Scenario)
        {
            var Scene = new BitmapImage(new Uri("ms-appx:///Assets/Worlds/" + Scenario + ".png"));
            StartObject(Scene, 1200, 768, 0);
        }

        public new void StartObject(BitmapImage Image, double ImageWidth, double ImageHeight, float Adjustment)
        {
            this.Adjustment = Adjustment;
            StartImage(Image, ImageWidth, ImageHeight);
        }
    }
}
