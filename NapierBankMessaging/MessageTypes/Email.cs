using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessaging.MessageTypes
{
    class Email: Message
    {

        public string eAddress { get; set; }
        public string[] qURLS { get; set; }

    }
}
