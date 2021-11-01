using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessaging.MessageTypes
{
    class SIR: Email
    {

        public DateTime Date { get; set; }
        public string sortCode { get; set; }
        public string Incident { get; set; }

        public SIR()
        {

            sortCode = null;
            Incident = null;

        }

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
