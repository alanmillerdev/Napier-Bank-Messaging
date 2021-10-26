using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessaging.MessageTypes
{
    public class Message
    {

        public string messageID { get; set; }
        public string messageBody { get; set; }

        public Message(string msgID, string msgBody)
        {

            messageID = msgID;
            messageBody = msgBody;

        }
    }
}
