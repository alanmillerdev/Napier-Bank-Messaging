using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessaging.MessageTypes
{
    public class Email: Message
    {

        public string eAddress { get; set; }
        public string[] qURLS { get; set; }

        public Email(string email, string[] urls, string msgID, string msgBody) : base(msgID, msgBody)
        {

            eAddress = email;
            qURLS = urls;
            messageID = msgID;
            messageBody = msgBody;

        }
    }
}
