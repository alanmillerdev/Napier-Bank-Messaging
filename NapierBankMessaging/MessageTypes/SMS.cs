using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessaging.MessageTypes
{
    public class SMS : Message
    {

        public string pNumber { get; set; }

        public SMS()
        {

            pNumber = null;
            messageID = null;
            messageBody = null;

        }

        public SMS(string phoneNo, string msgID, string msgBody) : base(msgID, msgBody)
        {

            this.pNumber = phoneNo;
            this.messageID = msgID;
            this.messageBody = msgBody;

        }
    }
}