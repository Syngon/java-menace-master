using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using java_menace.Itens.Hardwares;

namespace java_menace.Itens.Potions
{
    class ManaPotion : Potion
    {
        public ManaPotion(double Price)
        {
            this.Bonus = 15;
            this.ID = 15;
            this.Name = "Mana Potion";
            this.Type = "Consumable";
            this.Price = Price;
        }
        public override void Effect(Character user)
        {
            if (user.Notebook.CurrentMana + Bonus > user.Notebook.MaxMana) user.Notebook.CurrentMana = user.Notebook.MaxMana;
            else user.Notebook.CurrentMana += Bonus;

        }
    }
}
