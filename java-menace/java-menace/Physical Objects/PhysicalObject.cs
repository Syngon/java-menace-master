using java_menace.Movement;
using System.Collections.Generic;
using System.Numerics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace java_menace.Physical_Objects
{
    abstract class PhysicalObject
    {
        protected Dictionary<string, BitmapImage> Views { get; set; }
        public Image ObjectImage { get; set; }
        public Collider ColliderHandler { get; set; }
        public float Adjustment { get; set; }

        protected void StartObject(BitmapImage Image, double ImageWidth, double ImageHeight, float Adjustment)
        {
            this.Adjustment = Adjustment;
            StartImage(Image, ImageWidth, ImageHeight);
            StartCollision(Adjustment);
        }

        protected void StartImage(BitmapImage Image, double ImageWidth, double ImageHeight)
        {
            ObjectImage = new Image()
            {
                HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top,
                Width = ImageWidth,
                Height = ImageHeight,
                Source = Image
            };
        }

        protected void StartCollision(float Adjustment)
        {
            ColliderHandler = new Collider(ObjectImage.Width, ObjectImage.Height - Adjustment, -48, Adjustment);
        }

        public Vector2 GetLocation()
        {
            var Location = new Vector2((float) ObjectImage.Margin.Left, (float) ObjectImage.Margin.Top);
            return Location;
        }
    }
}
