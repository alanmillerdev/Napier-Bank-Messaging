﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Windows;
using NapierBankMessaging.MessageTypes;
using NapierBankMessaging.Serialisation;

namespace NapierBankMessaging.InputParser
{

    //TODO: Convert Parser to return one parsed message at a time
    //Output object to JSON file at the same time for storage.
    //Display parsed result via UI.
    //This approach will allow for conversion of message with ease for manual input and file based input
    //Once this is complete implement unit tests for serialisation and parsing
    //Finally merge branches then start working on the UI.

    public class TxtParser
    {

        List<Message> ParsedMessages = new List<Message>();

        public List<Message> TXTParser(string[] TxtData)
        {

            foreach (string line in TxtData)
            {

                if (line.Split()[0].StartsWith("+"))
                {

                    SMSParser(line);

                } else if (line.Split()[0].StartsWith("@"))
                {
                    TweetParser(line);
                }
                else
                {
                    try
                    {

                        new MailAddress(line.Split()[0]);

                        EmailParser(line);

                    } catch (FormatException)
                    {
                        //Invalid Data Error
                        MessageBox.Show("It doesn't work");
                    }
                }
            }

            return ParsedMessages;

        }

        public string MessageIDBuilder(string type)
        {

            string MessageID;

            var chars = "0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return MessageID = type + new String(stringChars);

        }

        private string AbbreviationCheck(string msgBody)
        {

            var CSVHandlerInstance = new CSVHandler();

            string[] splitMsgBody = msgBody.Split();

            //Needs better implementation
            string csvFilePath = @"C:\Users\alanm\Downloads\textwords.csv";

            IDictionary<string, string> abbreviationList = CSVHandler.AbbreviationInput(csvFilePath);

            foreach(string val in splitMsgBody)
            {

                if (abbreviationList.TryGetValue(val.ToUpper(), out string value))
                {

                    string replacementValue = "<" + value + ">";

                    splitMsgBody = splitMsgBody.Select(s => s.Replace(val, replacementValue)).ToArray();

                }
            }
            return msgBody = string.Join(" ", splitMsgBody);
        }

        private void EmailParser(string preParseLine)
        {

            string messageID;
            string messageBody;
            string emailAddress;
            string emailSubject;
            string[] quarantinedURLs;

            Email EmailParsed = new Email();

            string[] splitLine = preParseLine.Split();

            string[] checkedMessageBody = null;

            List<string> tempURLList = new List<string>();

            do
            {

                //Email Subject
                emailSubject = splitLine[1];

                if(emailSubject == "SIR")
                {

                    SIRParser(preParseLine);

                } else
                {

                    //MessageID

                    messageID = MessageIDBuilder("E");

                    //Email Address
                    emailAddress = splitLine[0];

                    //MessageBody

                    List<string> tempList = new List<string>();
                    for (int i = 2; i < splitLine.Length; i++)
                    {
                        tempList.Add(splitLine[i]);
                    }

                    messageBody = string.Join(" ", tempList.ToArray());

                    //Message Body Length Check
                    if (messageBody.Length > 1028)
                    {

                        MessageBox.Show("Tweet body exceeds supported bounds.");
                        break;
                    }

                    //URL Check

                    string[] splitMessageBody = messageBody.Split();

                    foreach (string val in splitMessageBody)
                    {
                        if (val.StartsWith("http://") || val.StartsWith("https://"))
                        {

                            tempURLList.Add(val);

                            string replacementValue = "<URL Quarantined>";

                            checkedMessageBody = splitMessageBody.Select(s => s.Replace(val, replacementValue)).ToArray();

                        }
                    }

                    messageBody = string.Join(" ", checkedMessageBody);

                    //Quarantined URLs
                    quarantinedURLs = tempURLList.ToArray();

                    EmailParsed = new Email(emailAddress, emailSubject, quarantinedURLs, messageID, messageBody);

                    ParsedMessages.Add(EmailParsed);
                }
            }
            while (EmailParsed == null);
        }

        private void SIRParser(string preParseLine)
        {

            string messageID;
            string messageBody;
            string[] urls;
            string emailAddress;
            string emailSubject;
            DateTime date;
            string sortCode;
            string incident;

            SIR SIRParsed = new SIR();

            string[] checkedMessageBody = null;

            List<string> tempURLList = new List<string>();

            string[] splitLine = preParseLine.Split();

            do
            {

                //MessageID

                messageID = MessageIDBuilder("E");

                //Email Address
                emailAddress = splitLine[0];

                //Subject
                emailSubject = splitLine[1];

                //Date
                date = DateTime.Parse(splitLine[2]);

                //SortCode
                sortCode = splitLine[3];

                //Incident
                incident = splitLine[4];

                //MessageBody
                List<string> tempList = new List<string>();
                for (int i = 5; i < splitLine.Length; i++)
                {
                    tempList.Add(splitLine[i]);
                }

                messageBody = string.Join(" ", tempList.ToArray());

                //URL Check

                string[] splitMessageBody = messageBody.Split();

                foreach (string val in splitMessageBody)
                {
                    if (val.StartsWith("http://") || val.StartsWith("https://"))
                    {

                        tempURLList.Add(val);

                        string replacementValue = "<URL Quarantined>";

                        checkedMessageBody = splitMessageBody.Select(s => s.Replace(val, replacementValue)).ToArray();

                    }
                }

                messageBody = string.Join(" ", checkedMessageBody);

                //Quarantined URLs
                urls = tempURLList.ToArray();

            } while (SIRParsed == null);

            SIRParsed = new SIR(date, sortCode, incident, emailAddress, emailSubject, urls, messageID, messageBody);

            ParsedMessages.Add(SIRParsed);
        }

        private void SMSParser(string preParseLine)
        {

            string messageID;
            string messageBody;
            string sender;

            SMS SMSParsed = new SMS();

            string[] splitLine = preParseLine.Split();

            do
            {

                //MessageID

                messageID = MessageIDBuilder("S");

                //Sender
                sender = splitLine[0];

                //MessageBody

                List<string> tempList = new List<string>();
                for (int i = 1; i < splitLine.Length; i++)
                {
                    tempList.Add(splitLine[i]);
                }

                messageBody = string.Join(" ", tempList.ToArray());

                //Message Body Text Speak Conversion
                string checkResult = AbbreviationCheck(messageBody);

                messageBody = checkResult;

            }
            while (SMSParsed == null);

            SMSParsed = new SMS(sender, messageID, messageBody);

            ParsedMessages.Add(SMSParsed);
        }

        private void TweetParser(string preParseLine)
        {

            string messageID;
            string messageBody;
            string username;
            string[] mentions;
            string[] hashtags;

            Tweet TweetParsed = new Tweet();

            string[] splitLine = preParseLine.Split();

            do
            {
                //MessageID

                messageID = MessageIDBuilder("T");

                //MessageBody

                List<string> tempList = new List<string>();
                for (int i = 1; i < splitLine.Length; i++)
                {
                    tempList.Add(splitLine[i]);
                }

                messageBody = string.Join(" ", tempList.ToArray());

                //Message Body Length Check
                if (messageBody.Length > 140)
                {

                    MessageBox.Show("Tweet body exceeds supported bounds.");
                    break;
                }

                //Message Body Text Speak Conversion
                string checkResult = AbbreviationCheck(messageBody);

                messageBody = checkResult;

                //Username
                username = splitLine[0];

                if(username.Length > 16)
                {
                    MessageBox.Show("Twitter Username exceeds supported bounds.");
                    break;
                }

                //Mentions

                List<String> tempMentionsList = new List<string>();
                foreach (string val in messageBody.Split())
                {

                    if (val.StartsWith("@"))
                    {
                        tempMentionsList.Add(val);
                    }
                }

                mentions = tempMentionsList.ToArray();

                //Hashtags

                List<String> tempHashtagList = new List<string>();
                foreach (string val in messageBody.Split())
                {

                    if (val.StartsWith("#"))
                    {
                        tempHashtagList.Add(val);
                    }
                }

                hashtags = tempHashtagList.ToArray();

                TweetParsed = new Tweet(username, hashtags, mentions, messageID, messageBody);

                ParsedMessages.Add(TweetParsed);

            }
            while (TweetParsed == null);
        }
    }
}