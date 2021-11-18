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
        //Initalising a new Instance of JSONHandler to be used throughout different system functionality.
        JSONHandler jsonHandler = new JSONHandler();

        //Initalising a new List of Messages called MessageList.
        List<Message> MessageList = new List<Message>();

        //Constructor
        public Controller()
        {
            //MessageList loads in the applicationData if available.
            MessageList = loadApplicationData();

        }

        //loadApplicationData methods, responsible for loading in data to the system.
        public List<Message> loadApplicationData()
        {
            
            //Stores the Read in application data in the MessageList variable.
            MessageList = jsonHandler.ReadApplicationData();

            if(MessageList == null)
            {
                MessageList = new List<Message>();
            }

            //Returns Message List.
            return MessageList;

        }

        //saveApplicationData method, responsible for saving the data of the application on close.
        public void saveApplicationData()
        {
            //Calls the JSONOutput method from the JSON Handler Instance.
            jsonHandler.JSONOutput(MessageList);
        }

        //TxtFileUploadMessageParser method responsible for returning a MessageList obtained from a txt file upload.
        public List<Message> TxtFileUploadMessageParser()
        {

            //Initalising returnedMessages 
            List<Message> returnedMessages = new List<Message>();

            //Opens a file explorer window to allow the user to locate a file.
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "txt Files (*.txt)|*.txt";
            file.InitialDirectory = @"c:\";
            file.FilterIndex = 1;
            file.Multiselect = false;

            //If a filepath was obtained, parse the messages from the file and return them as part of the returnedMessages list.
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

            return returnedMessages;
        }

        //ManualInputMessageParser method, responsible for handling parsing of manual entries
        public List<Message> ManualInputMessageParser(string msgHeader, string msgBody)
        {

            //Initalises the messageReturn message list.
            List<Message> messageReturn = new List<Message>();

            //Trys to parse the message data.
            //If the no message is able to be returned an error will be caught.
            try
            {
                string[] data = { msgHeader + " " + msgBody };
                var ParserInstance = new TxtParser();
                messageReturn = ParserInstance.TXTParser(data);
                MessageList.Add(messageReturn[0]);
            } catch (ArgumentOutOfRangeException e)
            {
               
            }

            //Returns messageReturn.
            return messageReturn;
        }

        //GetQuarantineList variable, method responsible for returning a string Tuple containing all QuarantinedURLs and the MessageID that they are apart of.
        public List<Tuple<string, string>> GetQuarantineList()
        {
            //Creates a List of Emails
            List<Email> emailList = new List<Email>();

            //Creates a List of SIR's
            List<SIR> sirList = new List<SIR>();

            //For each message in the MessageList if the message tpye is email or SIR it is added to it's respective list. 
            foreach (Message msg in MessageList)
            {
                if (msg.GetType() == typeof(Email))
                {
                    emailList.Add((Email)msg);
                }

                if (msg.GetType() == typeof(SIR))
                {
                    sirList.Add((SIR)msg);
                }
            }

            //Declares and Initalises the urlList tuple list that will contain all of the instances of quarantined emails.
            List<Tuple<string, string>> urlList = new List<Tuple<string, string>>();

            //for each email in email list add the quarantined url and messageID to the urlList.
            foreach (Email email in emailList)
            {
                foreach (string url in email.qURLS)
                {
                    urlList.Add(new Tuple<string, string>(email.messageID, url));
                }
            }

            //for each SIR in SIR list add the quarantined url and messageID to the urlList.
            foreach (SIR sir in sirList)
            {
                foreach (string url in sir.qURLS)
                {
                    urlList.Add(new Tuple<string, string>(sir.messageID, url));
                }
            }

            //Return the list of URL Tuples.
            return urlList;

        }

        //getSIRList method, responsible for returning all of the Significant Incident Reports as a list to be displayed.
        public List<SIR> getSIRList()
        {

            List<SIR> sirList = new List<SIR>();

            //For each message in the message list, if the message type is SIR add it to the sirList.
            foreach (Message msg in MessageList)
            {

                if (msg.GetType() == typeof(SIR))
                {
                    sirList.Add((SIR)msg);
                }

            }

            //Return list of a Significant Incident Reports from the system.
            return sirList;
        }

        //GetTrends method, responsible for returning a list of all trends from the tweets stored within the system.
        public Dictionary<string, int> GetTrends()
        {
            //Declares and Initalises tweetList which stores a list of all tweets.
            List<Tweet> tweetList = new List<Tweet>();

            //Declares and Initalises trendList which stores the hashtag and the count of it occuring.
            Dictionary<string, int> trendList = new Dictionary<string, int>();

            //For each message in messageList add Tweets to tweetList.
            foreach (Message msg in MessageList)
            {

                if (msg.GetType() == typeof(Tweet))
                {

                    tweetList.Add((Tweet)msg);

                }
            }

            //For each tweet in tweetList.
            foreach (Tweet tweet in tweetList)
            {
                //for each hashtag in the tweets hashtag array
                foreach (string hashtag in tweet.hashtags)
                {
                    //If trendList contains that hashtag, increment the count by one.
                    if (trendList.ContainsKey(hashtag))
                    {
                        trendList[hashtag] = trendList[hashtag] + 1;
                    }
                    //else add the hashtag and give it a count of 1.
                    else
                    {
                        trendList.Add(hashtag, 1);
                    }
                }
            }

            //Return trendList
            return trendList;
        }

        //GetMentions method, responsible for returning a list of all mentions from the tweets stored within the system.
        //Follows the same structure as GetTrends.
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
