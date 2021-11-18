using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Windows;
using NapierBankMessaging.MessageTypes;
using NapierBankMessaging.Serialisation;

namespace NapierBankMessaging.InputParser
{

    public class TxtParser
    {

        List<Message> ParsedMessages = new List<Message>();

        Random random = new Random();

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
                        MessageBox.Show("Invalid Data Entry Starting with: " + line.Split()[0] + ". Please try again.");
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

            for (int i = 0; i < stringChars.Length; i++)
            {
                
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return MessageID = type + new String(stringChars);

        }

        public string AbbreviationCheck(string msgBody)
        {

            var CSVHandlerInstance = new CSVHandler();

            string[] splitMsgBody = msgBody.Split();

            //NEEDS BETTER IMPLEMENTATION
            string csvFilePath = "textwords.csv";

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

        public void EmailParser(string preParseLine)
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

                try
                {
                    //Email Subject
                    emailSubject = splitLine[1];

                    if (emailSubject.Length > 20)
                    {

                        MessageBox.Show("Email subject exceeds supported bounds.");
                        break;

                    }

                    if (emailSubject == "SIR")
                    {

                        SIRParser(preParseLine);

                    }
                    else
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

                            MessageBox.Show("Email body exceeds supported bounds.");
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

                        if (messageBody == string.Empty)
                        {
                            MessageBox.Show("Invalid Message Body, Try Again.");
                            break;
                        }

                        messageBody = string.Join(" ", checkedMessageBody);

                        //Quarantined URLs
                        quarantinedURLs = tempURLList.ToArray();

                        EmailParsed = new Email(emailAddress, emailSubject, quarantinedURLs, messageID, messageBody);

                        ParsedMessages.Add(EmailParsed);
                    }
#pragma warning disable CS0168 // The variable 'err' is declared but never used
                } catch (Exception err)
#pragma warning restore CS0168 // The variable 'err' is declared but never used
                {
                    MessageBox.Show("Invalid Message Body, Please Try Again.");
                    break;
                }
            }
            while (EmailParsed == null);
        }

        private void SIRParser(string preParseLine)
        {

            string messageID = string.Empty;
            string messageBody = string.Empty;
            string[] urls = null;
            string emailAddress = string.Empty;
            string emailSubject = string.Empty;
            DateTime date = DateTime.MinValue;
            string sortCode = string.Empty;
            string incident = string.Empty;

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

                try
                {

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

                    if (checkedMessageBody != null)
                    {
                        messageBody = string.Join(" ", checkedMessageBody);
                    }

                    //Quarantined URLs
                    urls = tempURLList.ToArray();

                    SIRParsed = new SIR(date, sortCode, incident, emailAddress, emailSubject, urls, messageID, messageBody);

                    ParsedMessages.Add(SIRParsed);

#pragma warning disable CS0168 // The variable 'err' is declared but never used
                } catch (Exception err)
#pragma warning restore CS0168 // The variable 'err' is declared but never used
                {
                    MessageBox.Show("Invalid Message Body, Please Try Again.");
                    break;
                }

            } while (SIRParsed == null);

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

                try
                {

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

                    SMSParsed = new SMS(sender, messageID, messageBody);

                    ParsedMessages.Add(SMSParsed);

#pragma warning disable CS0168 // The variable 'err' is declared but never used
                } catch (Exception err)
#pragma warning restore CS0168 // The variable 'err' is declared but never used
                {
                    MessageBox.Show("Invalid Message Body, Please Try Again.");
                    break;
                }
            }
            while (SMSParsed == null);
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

                //Username
                username = splitLine[0];

                if (username.Length > 16)
                {
                    MessageBox.Show("The inputted username is outwidth Twitters username bounds, are you sure you have entered it correctly?");
                    break;
                }

                try
                {
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

                        MessageBox.Show("The inputted message exceeds Twitters message bounds, are you sure you have entered it correctly?");
                        break;
                    }

                    //Message Body Text Speak Conversion
                    string checkResult = AbbreviationCheck(messageBody);

                    messageBody = checkResult;

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
#pragma warning disable CS0168 // The variable 'err' is declared but never used
                catch (Exception err)
#pragma warning restore CS0168 // The variable 'err' is declared but never used
                {
                    MessageBox.Show("Invalid Message Body, Please Try Again.");
                    break;
                }
            }
            while (TweetParsed == null);
        }
    }
}