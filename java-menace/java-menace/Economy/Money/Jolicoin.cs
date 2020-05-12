using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace java_menace.Economy
{
    class Jolicoin
    {
        public double ValueBTC { get; set; }
        public double ValueJLC { get; set; }

        public Jolicoin()
        {
            Bitcoin Btc = new Bitcoin();
            ValueBTC = double.Parse(Btc.Quotation) / 100;
            ValueJLC = ValueBTC * 10;
        }
    }
}
