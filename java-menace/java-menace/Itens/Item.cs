using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace java_menace.Itens
{
    public abstract class Item
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public string Rarity { get; set; }
        public int Bonus { get; set; }

        protected Item()
        {
        }
    }
}
