using java_menace.Itens.Potions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace java_menace.Itens.Hardwares
{
    class Hardware : Item
    {
        public Hardware(string Type, string Name, string Rarity, int Bonus, double Price)
        {
            this.Type = Type;
            this.Name = Name;
            this.Rarity = Rarity;
            this.Bonus = Bonus;
            this.Price = Price;
        }
    }

    static class HardwareDAO
    {
        static List<List<Hardware>> HardwareByCategory = new List<List<Hardware>>()
        {
                new List<Hardware>()
                {
                    new Hardware("Processor", "i3", "Comum", 100, 50),
                    new Hardware("Processor", "i5", "Raro", 300, 100),
                    new Hardware("Processor", "i7", "Lendario", 500, 150)
                },

                new List<Hardware>()
                {
                    new Hardware("GPU", "GTX_1050", "Comum", 100, 50),
                    new Hardware("GPU", "GTX_1060", "Raro", 300, 100),
                    new Hardware("GPU", "GTX_1070", "Lendario", 500, 150)
                },

                new List<Hardware>()
                {
                    new Hardware("RAM", "8GB DDR4", "Comum", 100, 50),
                    new Hardware("RAM", "16GB DDR4", "Raro", 300, 100),
                    new Hardware("RAM", "64GB DDR4", "Lendario", 500, 150)
                },

                new List<Hardware>()
                {
                    new Hardware("HardDisk", "500GB HDD", "Comum", 100, 50),
                    new Hardware("HardDisk", "1TB HDD", "Raro", 300, 100),
                    new Hardware("HardDisk", "1TB SSD", "Lendario", 500, 150)
                },

                new List<Hardware>()
                {
                    new Hardware("MotherBoard", "ASUS B350", "Comum", 100, 50),
                    new Hardware("MotherBoard", "AORUS Z370", "Raro", 300, 100),
                    new Hardware("MotherBoard", "GALAX P220", "Lendario", 500, 150)
                }
        };

        public static List<List<Hardware>> GetHardware()
        {
            return HardwareByCategory;
        }
    }
}
