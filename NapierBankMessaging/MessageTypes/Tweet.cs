using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessaging.MessageTypes
{
    //Tweet message type, inherits from Message parent class.
    public class Tweet : Message
    {
        //Declares attributes and initalises getters and setters.
        public string tUsername { get; set; }
        public string [] hashtags {get; set;}
        public string[] mentions { get; set; }

        //Constructor
        public Tweet() : base()
        {
            tUsername = null;
            hashtags = null;
            mentions = null;
            messageID = null;
            messageBody = null;

        }

        //Constructor
        public Tweet(string username, string [] tags, string [] ments, string msgID, string msgBody) : base(msgID, msgBody)
        {

            this.tUsername = username;
            this.hashtags = tags;
            this.mentions = ments;
            this.messageID = msgID;
            this.messageBody = msgBody;

        }
    }
}