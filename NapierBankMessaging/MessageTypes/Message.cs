using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessaging.MessageTypes
{
    //Message Class that holds the base attributes of all of the child message types within the system.
    //Including MessageID and MessageBody
    public class Message
    {
        //Declaring the Class Attributes
        public string messageID { get; set; }
        public string messageBody { get; set; }

        //Constructor
        public Message()
        {

            messageID = null;
            messageBody = null;

        }

        //Constructor
        public Message(string msgID, string msgBody)
        {

            this.messageID = msgID;
            this.messageBody = msgBody;

        }
    }
}
