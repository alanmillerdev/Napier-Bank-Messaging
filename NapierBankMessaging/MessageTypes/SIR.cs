using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessaging.MessageTypes
{
    //SIR message type, inherits from Email parent class.
    public class SIR: Email
    {
        //Declares attributes and initalises getters and setters.
        public DateTime Date { get; set; }
        public string sortCode { get; set; }
        public string Incident { get; set; }

        //Constructor
        public SIR()
        {

            sortCode = null;
            Incident = null;

        }

        //Constructor
        public SIR(DateTime date, string sortCode, string incident, string emailAddress, string emailSubject, string[] urls, string messageID, string messageBody)
        {
            this.Date = date;
            this.sortCode = sortCode;
            this.Incident = incident;
            this.eAddress = emailAddress;
            this.eSubject = emailSubject;
            this.qURLS = urls;
            this.messageID = messageID;
            this.messageBody = messageBody;
        }
    }
}
