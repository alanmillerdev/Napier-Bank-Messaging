﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessaging.MessageTypes
{
    public class Tweet : Message
    {

        public string tUsername { get; set; }
        public string [] hashtags {get; set;}
        public string[] mentions { get; set; }

        public Tweet() : base()
        {
            tUsername = null;
            hashtags = null;
            mentions = null;
            messageID = null;
            messageBody = null;

        }

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