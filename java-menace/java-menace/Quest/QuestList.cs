using java_menace.Itens.Hardwares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace java_menace.Quest
{
    class QuestList
    {
        QuestBase Quest1 = new QuestBase("Poder 1", "Alcance a marca de 200 de AtkPower", Character =>
        {
            if (Character.Notebook.AttackPower >= 200)
            {
                Character.WalletJLC += 100;
                return true;
            }
            return false;
        });

        QuestBase Quest2 = new QuestBase("JoliCoin 1", "Reúna 1000 JoliCoins", Character =>
        {
            
            if (Character.WalletJLC >= 1000)
            {
                Character.WalletJLC += 100;
                return true;
            }

            return false;
        });

        QuestBase Quest3 = new QuestBase("Entregue uma GTX_1050", "Entregar uma GTX_1050 para Bacon", Character =>
        {
            if (Character.Inventory.HardwareInventory.Contains(HardwareDAO.GetHardware()[1][0]))
            {
                Character.WalletJLC += 100;
                return true;
            }
            return false;
        });
    }
}
