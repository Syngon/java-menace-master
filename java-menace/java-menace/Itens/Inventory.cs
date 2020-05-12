using java_menace.Itens.Hardwares;
using java_menace.Itens.Potions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace java_menace.Itens
{
    class Inventory
    {
        private const int MAXIMUM_SLOTS_IN_INVENTORY = 10;
        public List<Hardware> HardwareInventory = new List<Hardware>(MAXIMUM_SLOTS_IN_INVENTORY);
        public List<LifePotion> LifePotionInventory = new List<LifePotion>(MAXIMUM_SLOTS_IN_INVENTORY);
        public List<ManaPotion> ManaPotionInventory = new List<ManaPotion>(MAXIMUM_SLOTS_IN_INVENTORY);

        public bool AddPotion(Potion Potion)
        {
            if (Potion is LifePotion)
            {
                if(!(LifePotionInventory.Count >= MAXIMUM_SLOTS_IN_INVENTORY))
                {
                    LifePotionInventory.Add(Potion as LifePotion);
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            else if (Potion is ManaPotion)
            {
                if (!(ManaPotionInventory.Count >= MAXIMUM_SLOTS_IN_INVENTORY))
                {
                    ManaPotionInventory.Add(Potion as ManaPotion);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
        public void RemovePotion(Potion Potion)
        {
            if (Potion is LifePotion)
            {
                LifePotionInventory.Remove(Potion as LifePotion);

            }
            else if (Potion is ManaPotion)
            {
                ManaPotionInventory.Remove(Potion as ManaPotion);
            }
        }

        public void UseManaPotion(Character User)
        {
            ManaPotionInventory[ManaPotionInventory.Count - 1].Effect(User);
            ManaPotionInventory.Remove(ManaPotionInventory[ManaPotionInventory.Count - 1] as ManaPotion);
        }

        public void UseLifePotion(Character User)
        {
            LifePotionInventory[LifePotionInventory.Count - 1].Effect(User);
            LifePotionInventory.Remove(LifePotionInventory[LifePotionInventory.Count - 1] as LifePotion);
        }

        public bool CheckPotionsQuantity(Potion Potion)
        {
            if (Potion is LifePotion && LifePotionInventory.Count() > 0) return true;
            else if (Potion is ManaPotion && ManaPotionInventory.Count() > 0) return true;

            return false;
        }

        public bool AddItem(Item Item)
        {
            if(Item is Potion)
            {
                AddPotion(Item as Potion);
            }
            else
            {
                if(!(HardwareInventory.Count >= MAXIMUM_SLOTS_IN_INVENTORY))
                {
                    HardwareInventory.Add(Item as Hardware);
                }
                else
                {
                    return false;
                }
                
            }
            return true;
        }
    }
}
