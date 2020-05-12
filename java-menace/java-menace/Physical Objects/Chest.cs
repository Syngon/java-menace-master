using java_menace.Itens;
using java_menace.Itens.Hardwares;
using java_menace.Itens.Potions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace java_menace.Physical_Objects
{
    class Chest : PhysicalObject
    {
        public bool WasEverOpened { get; set; } = false;
        public int Times { get; set; } = 0;

        public Chest()
        {
            StartObject(new BitmapImage(new Uri("ms-appx:///Assets/Chest/chest.png")), 48, 48, 0);
        }

        private const int MAXIMUM_SLOTS_IN_CHEST = 15;
        public List<Item> ChestItems = new List<Item>(MAXIMUM_SLOTS_IN_CHEST);

        private void GenerateItem()
        {
            if (ChestItems.Count < MAXIMUM_SLOTS_IN_CHEST)
            {
                Random NumRandom = new Random();
                Random NumRandom2 = new Random();

                int RandomNumber = NumRandom.Next(8);
                int RandomNumber2 = NumRandom2.Next(100);

                switch (RandomNumber)
                {
                    case 1:
                        if (RandomNumber2 <= 75)
                        {
                            ChestItems.Add(HardwareDAO.GetHardware()[0][0]);
                        }
                        else if (RandomNumber2 > 75 && RandomNumber2 <= 95)
                        {
                            ChestItems.Add(HardwareDAO.GetHardware()[0][1]);
                        }
                        else
                        {
                            ChestItems.Add(HardwareDAO.GetHardware()[0][2]);

                        }
                        break;
                    case 2:
                        if (RandomNumber2 <= 75)
                        {
                            ChestItems.Add(HardwareDAO.GetHardware()[1][0]);
                        }
                        else if (RandomNumber2 > 75 && RandomNumber2 <= 95)
                        {
                            ChestItems.Add(HardwareDAO.GetHardware()[1][1]);
                        }
                        else
                        {
                            ChestItems.Add(HardwareDAO.GetHardware()[1][2]);
                        }
                        break;
                    case 3:
                        if (RandomNumber2 <= 75)
                        {
                            ChestItems.Add(HardwareDAO.GetHardware()[2][0]);
                        }
                        else if (RandomNumber2 > 75 && RandomNumber2 <= 95)
                        {
                            ChestItems.Add(HardwareDAO.GetHardware()[2][1]);
                        }
                        else
                        {
                            ChestItems.Add(HardwareDAO.GetHardware()[2][2]);
                        }
                        break;
                    case 4:
                        if (RandomNumber2 <= 75)
                        {
                            ChestItems.Add(HardwareDAO.GetHardware()[3][0]);
                        }
                        else if (RandomNumber2 > 75 && RandomNumber2 <= 95)
                        {
                            ChestItems.Add(HardwareDAO.GetHardware()[3][1]);
                        }
                        else
                        {
                            ChestItems.Add(HardwareDAO.GetHardware()[3][2]);
                        }
                        break;
                    case 5:
                        if (RandomNumber2 <= 75)
                        {
                            ChestItems.Add(HardwareDAO.GetHardware()[4][0]);
                        }
                        else if (RandomNumber2 > 75 && RandomNumber2 <= 95)
                        {
                            ChestItems.Add(HardwareDAO.GetHardware()[4][1]);
                        }
                        else
                        {
                            ChestItems.Add(HardwareDAO.GetHardware()[4][2]);
                        }
                        break;
                    case 6:
                        ChestItems.Add(PotionDAO.GetPotions()[0]);
                        break;
                    case 7:
                        ChestItems.Add(PotionDAO.GetPotions()[1]);
                        break;
                    default:
                        break;
                }
            }
        }
        public List<Item> ChestOpened()
        {
            if (!WasEverOpened)
            {
                Random NumRandom = new Random();
                int Randomnumber = NumRandom.Next(3, 10);

                while (Randomnumber != 0)
                {
                    GenerateItem();
                    Randomnumber--;
                }
            }

            WasEverOpened = true;
            Times++;
            return ChestItems;
        }

    }
}
