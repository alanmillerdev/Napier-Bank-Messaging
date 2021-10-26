using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessaging.MessageTypes
{
    class Tweet : Message
    {

        public string tUsername { get; set; }
        public string [] hashtags {get; set;}
        public string [] mentions { get; set; }

    }
}
