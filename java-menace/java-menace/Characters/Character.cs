using java_menace.Itens;
using java_menace.Itens.Hardwares;
using java_menace.Itens.Potions;
using java_menace.Movement;
using java_menace.Physical_Objects;
using java_menace.Terminal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml.Media.Imaging;

namespace java_menace
{
    abstract class Character : PhysicalObject
    {
        public Notebook Notebook { get; set; }
        public Inventory Inventory { get; set; }
        public string Username { get; set; }
        public double WalletBTC { get; set; } = 10000000;
        public double WalletJLC { get; set; } = 500;

        protected void StartObject(BitmapImage Image, double ImageWidth, double ImageHeight)
        {
            StartImage(Image, ImageWidth, ImageHeight);
            StartCollision();
            StartNotebook();
        }   

        protected void StartNotebook()
        {
            Notebook = new Notebook(this);
            Notebook.Bash = new Bash(Notebook);
            GiveInitialItems();
        }

        private void GiveInitialItems()
        {
            var Hardware = HardwareDAO.GetHardware();
            foreach(var List in Hardware)
            {
                Notebook.PutPiece(List[0]);
            }
        }

        private void StartCollision()
        {
            ColliderHandler = new Collider(ObjectImage.Width, ObjectImage.Height, 0, 0);
        }

        public void ChangeImageAsCharacterMove(string Direction)
        {
            ObjectImage.Source = Views[Direction];
        }
    }
}
