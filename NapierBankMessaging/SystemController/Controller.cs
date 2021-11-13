using Microsoft.Win32;
using NapierBankMessaging.InputParser;
using NapierBankMessaging.MessageTypes;
using NapierBankMessaging.Serialisation;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Message = NapierBankMessaging.MessageTypes.Message;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace NapierBankMessaging.SystemController
{
    public class Controller
    {
        JSONHandler jsonHandler = new JSONHandler();

        List<Message> MessageList = new List<Message>();

        public Controller()
        {
            MessageList = jsonHandler.ReadApplicationData();
        }

        public List<Message> loadApplicationData()
        {

            MessageList = jsonHandler.ReadApplicationData();

            return MessageList;

        }

        public void saveApplicationData()
        {
            jsonHandler.JSONOutput(MessageList);
        }

        public List<Message> TxtFileUploadMessageParser()
        {

            List<Message> returnedMessages = new List<Message>();

            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "txt Files (*.txt)|*.txt|All files (*.*)|*.*";
            file.InitialDirectory = @"c:\";
            file.FilterIndex = 1;
            file.Multiselect = false;

            if (file.ShowDialog() == true)
            {
                var TxtHandlerInstance = new TXTHandler();
                string[] returnedTxt = TxtHandlerInstance.TXTInput(file.FileName);
                var ParserInstance = new TxtParser();
                returnedMessages = ParserInstance.TXTParser(returnedTxt);
            }

            foreach (Message msg in returnedMessages)
            {
                MessageList.Add(msg);
            }

            //jsonHandler.JSONOutput(returnedMessages);

            return returnedMessages;
        }

        public List<Message> ManualInputMessageParser(string msgHeader, string msgBody)
        {

                string[] data = { msgHeader + " " + msgBody };
                List<Message> messageReturn = new List<Message>();
                var ParserInstance = new TxtParser();
                messageReturn = ParserInstance.TXTParser(data);
                MessageList.Add(messageReturn[0]);

            return messageReturn;
        }

        public List<Tuple<string, string>> GetQuarantineList()
        {

            List<Email> emailList = new List<Email>();

            List<SIR> sirList = new List<SIR>();

            foreach (Message msg in MessageList)
            {
                if(msg.GetType() == typeof(Email))
                {
                    emailList.Add((Email)msg);
                }

                if (msg.GetType() == typeof(SIR))
                {
                    sirList.Add((SIR)msg);
                }

            }

            List<Tuple<string, string>> urlList = new List<Tuple<string, string>>();

            foreach(Email email in emailList)
            {
                foreach(string url in email.qURLS)
                {
                    urlList.Add(new Tuple<string, string>(email.messageID, url));
                }
            }

            foreach (SIR sir in sirList)
            {
                foreach (string url in sir.qURLS)
                {
                    urlList.Add(new Tuple<string, string>(sir.messageID, url));
                }
            }

            return urlList;

        }

        public List<SIR> getSIRList()
        {

            List<SIR> sirList = new List<SIR>();

            foreach (Message msg in MessageList)
            {

                if (msg.GetType() == typeof(SIR))
                {
                    sirList.Add((SIR)msg);
                }

            }

            return sirList;
        }

        public Dictionary<string, int> GetTrends()
        {
            List<Tweet> tweetList = new List<Tweet>();

            Dictionary<string, int> trendList = new Dictionary<string, int>();

            foreach (Message msg in MessageList)
            {

                if(msg.GetType() == typeof(Tweet))
                {

                    tweetList.Add((Tweet)msg);

                }
            }

            foreach(Tweet tweet in tweetList)
            {
                foreach(string hashtag in tweet.hashtags)
                {
                    if(trendList.ContainsKey(hashtag))
                    {
                        trendList[hashtag] = trendList[hashtag] + 1;
                    }
                    else
                    {
                        trendList.Add(hashtag, 1);
                    }
                }
            }
            return trendList;
        }

        public Dictionary<string, int> GetMentions()
        {
            List<Tweet> tweetList = new List<Tweet>();

            Dictionary<string, int> mentionsList = new Dictionary<string, int>();

            foreach (Message msg in MessageList)
            {

                if (msg.GetType() == typeof(Tweet))
                {

                    tweetList.Add((Tweet)msg);

                }
            }

            foreach (Tweet tweet in tweetList)
            {
                foreach (string mention in tweet.mentions)
                {
                    if (mentionsList.ContainsKey(mention))
                    {
                        mentionsList[mention] = mentionsList[mention] + 1;
                    }
                    else
                    {
                        mentionsList.Add(mention, 1);
                    }
                }
            }
            return mentionsList;
        }
    }
}
