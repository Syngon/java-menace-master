using java_menace.Itens;
using java_menace.Itens.Hardwares;
using java_menace.Itens.Potions;
using java_menace.Movement;
using java_menace.Terminal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace java_menace.Characters
{
    class PlayableCharacter : Character
    {
        public Legs MovementHandler { get; set; }

        protected new void StartObject(BitmapImage Image, double ImageWidth, double ImageHeight)
        {
            StartImage(Image, ImageWidth, ImageHeight);
            StartMovement();
            StartCollision();
            StartNotebook();
            this.Inventory = new Inventory();
            Inventory.AddPotion(PotionDAO.GetPotions()[0]);
            Inventory.AddPotion(PotionDAO.GetPotions()[1]);
        }

        private void StartMovement()
        {
            MovementHandler = new Legs(this);
        }

        private void StartCollision()
        {
            ColliderHandler = new Collider(ObjectImage.Width, ObjectImage.Height, 0, 0);
            MovementHandler.MobMoved += ColliderHandler.OnMobMoved;
        }

        public new void ChangeImageAsCharacterMove(string Direction)
        {
            ObjectImage.Source = Views[Direction];
        }
    }
}

