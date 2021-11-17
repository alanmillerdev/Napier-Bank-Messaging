using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessaging.MessageTypes
{
    //SMS message type, inherits from Message parent class.
    public class SMS : Message
    {
        //Declares attributes and initalises getters and setters.
        public string pNumber { get; set; }

        //Constructor
        public SMS()
        {

            pNumber = null;
            messageID = null;
            messageBody = null;

        }

        //Constructor
        public SMS(string phoneNo, string msgID, string msgBody) : base(msgID, msgBody)
        {

            this.pNumber = phoneNo;
            this.messageID = msgID;
            this.messageBody = msgBody;

        }
    }
}