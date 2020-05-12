using java_menace.Movement;
using java_menace.Physical_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace java_menace.Characters
{
    abstract class Mob : Character
    {
        public string Name { get; set; }

        public void ReceiveDamage(int Damage, Character User)
        {
            Random NumRandom = new Random();
            int RandomXp = NumRandom.Next(10, 50);

            if (Notebook.CurrentLife - Damage <= 0)
            {
                User.Notebook.AddXP(User, RandomXp * Notebook.Level);
                User.WalletBTC += 10 * Notebook.Level;
                // Destroy this instance
            }
            else
            {
                Notebook.CurrentLife -= Damage;
            }
        }

        public void SetStatus()
        {
            Notebook.AttackPower = 100 + 10 * (Notebook.Level - 1);
            Notebook.DefensePower = 100 + 10 * (Notebook.Level - 1);
            Notebook.MaxLife = 100 + 10 * (Notebook.Level - 1);
            Notebook.MaxMana = 100 + 10 * (Notebook.Level - 1);
            Notebook.CurrentLife = Notebook.MaxLife;
            Notebook.CurrentMana = Notebook.MaxMana;
        }

    }
}