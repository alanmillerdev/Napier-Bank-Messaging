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



        public SMS(string phoneNo, string msgID, string msgBody) : base(msgID, msgBody)
        {

            pNumber = phoneNo;
            messageID = msgID;
            messageBody = msgBody;

        }

        public SMS()
        {



        }
    }
    }