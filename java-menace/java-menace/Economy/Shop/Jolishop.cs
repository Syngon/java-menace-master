﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using java_menace.Itens;
using java_menace.Itens.Potions;
using java_menace.Itens.Hardwares;
using java_menace.Physical_Objects;
using Windows.UI.Xaml.Media.Imaging;

namespace java_menace.Economy
{
    class Jolishop : PhysicalObject
    {
        private const int MAXIMUM_SLOTS_IN_ESTOQUE = 15;
        public List<Item> Estoque = new List<Item>(MAXIMUM_SLOTS_IN_ESTOQUE);

        private void GenerateItem()
        {
            if (Estoque.Count < MAXIMUM_SLOTS_IN_ESTOQUE)
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
                            Estoque.Add(HardwareDAO.GetHardware()[0][0]);
                        }
                        else if (RandomNumber2 > 75 && RandomNumber2 <= 95)
                        {
                            Estoque.Add(HardwareDAO.GetHardware()[0][1]);
                        }
                        else
                        {
                            Estoque.Add(HardwareDAO.GetHardware()[0][2]);

                        }
                        break;
                    case 2:
                        if (RandomNumber2 <= 75)
                        {
                            Estoque.Add(HardwareDAO.GetHardware()[1][0]);
                        }
                        else if (RandomNumber2 > 75 && RandomNumber2 <= 95)
                        {
                            Estoque.Add(HardwareDAO.GetHardware()[1][1]);
                        }
                        else
                        {
                            Estoque.Add(HardwareDAO.GetHardware()[1][2]);
                        }
                        break;
                    case 3:
                        if (RandomNumber2 <= 75)
                        {
                            Estoque.Add(HardwareDAO.GetHardware()[2][0]);
                        }
                        else if (RandomNumber2 > 75 && RandomNumber2 <= 95)
                        {
                            Estoque.Add(HardwareDAO.GetHardware()[2][1]);
                        }
                        else
                        {
                            Estoque.Add(HardwareDAO.GetHardware()[2][2]);
                        }
                        break;
                    case 4:
                        if (RandomNumber2 <= 75)
                        {
                            Estoque.Add(HardwareDAO.GetHardware()[3][0]);
                        }
                        else if (RandomNumber2 > 75 && RandomNumber2 <= 95)
                        {
                            Estoque.Add(HardwareDAO.GetHardware()[3][1]);
                        }
                        else
                        {
                            Estoque.Add(HardwareDAO.GetHardware()[3][2]);
                        }
                        break;
                    case 5:
                        if (RandomNumber2 <= 75)
                        {
                            Estoque.Add(HardwareDAO.GetHardware()[4][0]);
                        }
                        else if (RandomNumber2 > 75 && RandomNumber2 <= 95)
                        {
                            Estoque.Add(HardwareDAO.GetHardware()[4][1]);
                        }
                        else
                        {
                            Estoque.Add(HardwareDAO.GetHardware()[4][2]);
                        }
                        break;
                    case 6:
                        Estoque.Add(PotionDAO.GetPotions()[0]);
                        break;
                    case 7:
                        Estoque.Add(PotionDAO.GetPotions()[1]);
                        break;
                    default:
                        break;
                }
            }
        }

        public List<Item> ShopOpened()
        {
            Random NumRandom = new Random();
            int Randomnumber = NumRandom.Next(3, 10);

            while (Randomnumber != 0)
            {
                GenerateItem();
                Randomnumber--;
            }
            return Estoque;
        }
    }
}
