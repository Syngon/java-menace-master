using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Windows.System;
using System.Numerics;

namespace java_menace.Movement
{
    class CollisionRequestArgs
    {
        public VirtualKey Key { get; set; }
    }

    class Collider
    {
        public Rectangle BoxCollider { get; }
        public Dictionary<string, Vector2> VerticesCoordinates;
        public Dictionary<VirtualKey, bool> IsColliding;
        public event EventHandler<CollisionRequestArgs> CheckCollisionRequest;

        public double AdjustmentX { get; set; }
        public double AdjustmentY { get; set; }

        public Collider(double Width, double Height, double AdjustmentX, double AdjustmentY)
        {
            BoxCollider = new Rectangle
            {
                Width = Width - AdjustmentX,
                Height = Height,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Visibility = Visibility.Collapsed,
                Stroke = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0))
            };

            VerticesCoordinates = new Dictionary<string, Vector2>()
            {
                { "Top-Left", new Vector2()},
                { "Top-Right", new Vector2()},
                { "Bottom-Right", new Vector2()},
                { "Bottom-Left", new Vector2()},
                { "Center", new Vector2()}
            };

            IsColliding = new Dictionary<VirtualKey, bool>()
            {
                { VirtualKey.W, false },
                { VirtualKey.S, false },
                { VirtualKey.A, false },
                { VirtualKey.D, false },
            };

            this.AdjustmentX = AdjustmentX;
            this.AdjustmentY = AdjustmentY;

            AdjustColliderLocation(AdjustmentX, AdjustmentY);
            UpdateVerticesCoordinates();
        }

        public void AdjustColliderLocation(double AdjustmentX, double AdjustmentY)
        {
            BoxCollider.Margin = new Thickness(BoxCollider.Margin.Left + AdjustmentX/2, 
                                               BoxCollider.Margin.Top + AdjustmentY,
                                               BoxCollider.Margin.Right, BoxCollider.Margin.Bottom);
        }

        private void ChangeBoxColliderLocation(double Difference, VirtualKey Key)
        {
            if(Key == VirtualKey.A || Key == VirtualKey.D)
            {
                BoxCollider.Margin = new Thickness(BoxCollider.Margin.Left + Difference, BoxCollider.Margin.Top,
                                               BoxCollider.Margin.Right, BoxCollider.Margin.Bottom);
            }

            else
            {
                BoxCollider.Margin = new Thickness(BoxCollider.Margin.Left, BoxCollider.Margin.Top + Difference,
                                               BoxCollider.Margin.Right, BoxCollider.Margin.Bottom);
            }
        }

        public void UpdateVerticesCoordinates()
        {
            var MarginLeft = (float) this.BoxCollider.Margin.Left;
            var MarginTop = (float) this.BoxCollider.Margin.Top;
            var BoxWidht = (float) this.BoxCollider.Width;
            var BoxHeight = (float) this.BoxCollider.Height;

            VerticesCoordinates["Top-Left"] = new Vector2(MarginLeft, MarginTop);
            VerticesCoordinates["Top-Right"] = new Vector2(MarginLeft + BoxWidht, MarginTop);
            VerticesCoordinates["Bottom-Right"] = new Vector2(MarginLeft + BoxWidht, MarginTop + BoxHeight);
            VerticesCoordinates["Bottom-Left"] = new Vector2(MarginLeft, MarginTop + BoxHeight);
            VerticesCoordinates["Center"] = new Vector2(MarginLeft + (BoxWidht/2), MarginTop + (BoxHeight/2));
        }

        public void OnMobMoved(object sender, MobMovedArgs e)
        {
            ChangeBoxColliderLocation(e.Difference, e.Key);
            UpdateVerticesCoordinates();
        }

        public virtual void OnCheckCollisionRequest(VirtualKey Key)
        {
            CheckCollisionRequest?.Invoke(this, new CollisionRequestArgs() {  Key = Key });
        }
    }
}
