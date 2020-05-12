using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace java_menace.Economy
{
    class Bitcoin
    {
        public string Quotation
        {
            get
            {
                string json;

                using (var web = new System.Net.WebClient())
                {
                    var url = @"https://blockchain.info/tobtc?currency=USD&value=1";
                    json = web.DownloadString(url);
                }
                return json;
            }
        }
    }
}
