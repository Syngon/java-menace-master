using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace java_menace
{
    class MobMovedArgs
    {
        public double Difference { get; set; }
        public VirtualKey Key { get; set; }
    }

    class Legs
    {
        public float Velocity { get; set; }
        private int Cartesian { get; set; }
        private Character Owner { get; set; }

        private delegate bool Acessor();
        public event EventHandler<MobMovedArgs> MobMoved;
        Dictionary<VirtualKey, Acessor> MoveByKey = new Dictionary<VirtualKey, Acessor>();

        public Legs(Character Owner)
        {
            this.Owner = Owner;
            this.Velocity = 24f;
            StartMoveByKey();
        }

        public void Action(VirtualKey e)
        {
            GetCartesian(e);
            Parallel.Invoke(() => RunAsync(() =>
            {
                Owner.ColliderHandler.OnCheckCollisionRequest(e);
                if (!Owner.ColliderHandler.IsColliding[e])
                {
                    if (MoveByKey.ContainsKey(e))
                    {
                        if (MoveByKey[e]())
                        {
                            AdjustZIndex();
                            OnMobMoved(Cartesian * this.Velocity, e);
                        }
                    }
                }

            }), () => RunAsync(() => UpdateCharacterImage(e)));
        }

        private void GetCartesian(VirtualKey e)
        {
            Cartesian = ((e == VirtualKey.W || e == VirtualKey.A) ? -1 : 1);
        }

        private bool MoveVertically()
        {
            var NewLocationY = Owner.ObjectImage.Margin.Top + (Velocity * Cartesian);

            if (!IsOutOfScreen("y", NewLocationY))
            {
                Owner.ObjectImage.Margin = new Thickness(Owner.ObjectImage.Margin.Left,
                NewLocationY, Owner.ObjectImage.Margin.Right, Owner.ObjectImage.Margin.Bottom);
                return true;
            }

            return false;
        }

        private bool MoveHorizontally()
        {
            var NewLocationX = Owner.ObjectImage.Margin.Left + (Velocity * Cartesian);

            if (!IsOutOfScreen("x", NewLocationX))
            {
                Owner.ObjectImage.Margin = new Thickness(NewLocationX,
                Owner.ObjectImage.Margin.Top, Owner.ObjectImage.Margin.Right, Owner.ObjectImage.Margin.Bottom);
                return true;
            }

            return false;
        }

        private bool IsOutOfScreen(string Axys, double NewLocation)
        {
            if (Axys == "y")
            {
                if (NewLocation > (768 - Owner.ObjectImage.Height) || NewLocation < 0)
                {
                    return true;
                }
            }

            else
            {
                if (NewLocation > (1200 - Owner.ObjectImage.Height) || NewLocation < 0)
                {
                    return true;
                }
            }

            return false;
        }

        private void AdjustZIndex()
        {
            this.Owner.ObjectImage.SetValue(Canvas.ZIndexProperty,
                                                       this.Owner.ObjectImage.Margin.Top);
        }

        private void UpdateCharacterImage(VirtualKey e)
        {
            if (e == VirtualKey.W || e == VirtualKey.S)
            {
                Owner.ChangeImageAsCharacterMove(Cartesian == -1 ? "Top" : "Bottom");
            }
            else if (e == VirtualKey.A || e == VirtualKey.D)
            {
                Owner.ChangeImageAsCharacterMove(Cartesian == -1 ? "Left" : "Right");
            }
        }

        private async void RunAsync(DispatchedHandler e)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, e);
        }

        private void StartMoveByKey()
        {
            MoveByKey.Add(VirtualKey.W, MoveVertically);
            MoveByKey.Add(VirtualKey.S, MoveVertically);
            MoveByKey.Add(VirtualKey.A, MoveHorizontally);
            MoveByKey.Add(VirtualKey.D, MoveHorizontally);
        }

        public void KeyHandle(CoreWindow sender, KeyEventArgs e)
        {
            Task.Run(() => Action(e.VirtualKey));
        }

        public virtual void OnMobMoved(double Difference, VirtualKey Key)
        {
            MobMoved?.Invoke(this, new MobMovedArgs() { Difference = Difference, Key = Key });
        }
    }
}
