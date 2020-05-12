using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace java_menace.Itens.Potions
{
    class LifePotion : Potion
    {
        public LifePotion(double Price)
        {
            this.Bonus = 15;
            this.ID = 15;
            this.Name = "Life Potion";
            this.Type = "Consumable";
            this.Price = Price;
        }

        public override void Effect(Character user)
        {
            if (user.Notebook.CurrentLife + Bonus > user.Notebook.MaxLife) user.Notebook.CurrentLife = user.Notebook.MaxLife;
            else user.Notebook.CurrentLife += Bonus;

        }
    }
}
