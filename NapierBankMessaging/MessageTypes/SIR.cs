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
        }

        public SIR(DateTime incidentDate, string sortcode, string incident, string email, string subject, string[] urls, string msgID, string msgBody) : base(email, subject, urls, msgID, msgBody)
        {
            Date = incidentDate;
            sortCode = sortcode;
            Incident = incident;
            eAddress = email;
            eSubject = subject;
            qURLS = urls;
            messageID = msgID;
            messageBody = msgBody;
        }
    }
}
