using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace java_menace.Itens.Potions
{
    abstract class Potion : Item
    {
        abstract public void Effect(Character user);
    }

    static class PotionDAO
    {
        static List<Potion> Potions = new List<Potion>()
        {
            new LifePotion(15),
            new ManaPotion(15)

        };

        public static List<Potion> GetPotions()
        {
            return Potions;
        }
    }
}
