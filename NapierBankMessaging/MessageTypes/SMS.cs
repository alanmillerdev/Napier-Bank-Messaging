using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessaging.MessageTypes
{
    class SMS: Message
    {

        public string pNumber { get; set; }
        public string countryOrigin { get; set; }

    }
}
