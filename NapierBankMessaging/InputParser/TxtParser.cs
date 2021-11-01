using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Windows;
using NapierBankMessaging.MessageTypes;
using NapierBankMessaging.Serialisation;

namespace NapierBankMessaging.InputParser
{
    class TxtParser
    {

        string[] parsedData;

        public string [] TXTParser(string[] TxtData)
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

            return parsedData;

        }

        private string MessageIDBuilder(string type)
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
            string csvFilePath = @"\\napier-mail.napier.ac.uk\students\school of computing\user data\40478448\My Profile\Downloads\textwords.csv";

            IDictionary<string, string> abbreviationList = CSVHandler.AbbreviationInput(csvFilePath);

            foreach(string val in splitMsgBody)
            {

                if (abbreviationList.TryGetValue(val.ToUpper(), out string value))
                {

                    string replacementValue = "<" + value + "> ";

                    splitMsgBody = splitMsgBody.Select(s => s.Replace(val, replacementValue)).ToArray();

                }
            }
            return msgBody = string.Join(" ", splitMsgBody);
        }

        private Email EmailParser(string preParseLine)
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

                    messageID = MessageIDBuilder("S");

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

                }
            }
            while (EmailParsed == null);

            return EmailParsed;
        }

        private SIR SIRParser(string preParseLine)
        {
            SIR SIRParsed = new SIR();



            return SIRParsed;
        }

        private SMS SMSParser(string preParseLine)
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

            return SMSParsed;
        }

        private Tweet TweetParser(string preParseLine)
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
            }
            while (TweetParsed == null);

            return TweetParsed;
        }
    }
}
