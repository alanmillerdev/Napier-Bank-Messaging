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
        public string eSubject { get; set; }
        public string[] qURLS { get; set; }

        public Email()
        {

            eAddress = null;
            eSubject = null;
            qURLS = null;
            messageID = null;
            messageBody = null;

        }

        public Email(string email, string subject, string[] urls, string msgID, string msgBody) : base(msgID, msgBody)
        {

            this.eAddress = email;
            this.eSubject = subject;
            this.qURLS = urls;
            this.messageID = msgID;
            this.messageBody = msgBody;

        }

    }
}
