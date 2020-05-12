using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace java_menace.Characters
{
    class TruffleMan : Character
    {
        //Skin character
        public bool SellTruffle(Double PlayerCarteiraJLC)
        {
            Random randNum = new Random();
            if (randNum.Next(5) != 5)
            {
                return false;
            }
            if (PlayerCarteiraJLC < 50)
            {
                PlayerCarteiraJLC = 0;
                return true;
            }
            else
            {
                PlayerCarteiraJLC -= 50;
                return true;
            }
        }
    }
}