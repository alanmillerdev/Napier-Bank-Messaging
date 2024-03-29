﻿using System;
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

        //Declares and Initalises ParsedMessages that is returned at the end of the parsing process.
        List<Message> ParsedMessages = new List<Message>();

        //Creates a new Random instance that is used to generate Uniqiue MessageID's
        Random random = new Random();

        //TXTParser Method that is used to Parse and Return Messages.
        public List<Message> TXTParser(string[] TxtData)
        {
            //For each line in txtData
            foreach (string line in TxtData)
            {
                //If the first character is +, the system will think it's a number.
                if (line.Split()[0].StartsWith("+"))
                {

                    SMSParser(line);

                    //If the first character is an @, the system will think it's a twitter username.
                } else if (line.Split()[0].StartsWith("@"))
                {
                    TweetParser(line);
                }
                //Else, the system will try and parse a email.
                else
                {
                    try
                    {

                        new MailAddress(line.Split()[0]);

                        EmailParser(line);
                    //If none of the types are met, a message will be displayed to the user.
                    } catch (FormatException)
                    {
                        //Invalid Data Error
                        MessageBox.Show("Invalid Data Entry Starting with: " + line.Split()[0] + ". Please try again.");
                    }
                }
            }

            //Return ParsedMessages List.
            return ParsedMessages;

        }

        //MessageIDBuilder method, uses random to generate random ID's for messages.
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

        //AbbreviationCheck method, is used to check certain message bodys for textspeak abbreviations.
        public string AbbreviationCheck(string msgBody)
        {

            //Creates a CSVHandler instance.
            var CSVHandlerInstance = new CSVHandler();

            //Splits the message body by spaces and stores in an array.
            string[] splitMsgBody = msgBody.Split();

            //NEEDS BETTER IMPLEMENTATION
            //CSV File Path
            string csvFilePath = "textwords.csv";

            //Creates a Dictionary of abbreviations.
            IDictionary<string, string> abbreviationList = CSVHandler.AbbreviationInput(csvFilePath);

            //for each value in the split message body.
            foreach(string val in splitMsgBody)
            {
                //If the abbreviation list has a value that matches the current val, replace the textspeak with the real value.
                if (abbreviationList.TryGetValue(val.ToUpper(), out string value))
                {

                    string replacementValue = "<" + value + ">";

                    splitMsgBody = splitMsgBody.Select(s => s.Replace(val, replacementValue)).ToArray();

                }
            }
            //Join the message body and return it.
            return msgBody = string.Join(" ", splitMsgBody);
        }

        //EmailParser Method, used to parse email, is passed a line of data that has not been parsed at all.
        public void EmailParser(string preParseLine)
        {

            //Declare the data required as part of the constructor for the email.
            string messageID;
            string messageBody;
            string emailAddress;
            string emailSubject;
            string[] quarantinedURLs;

            //Creates a new Email Object.
            Email EmailParsed = new Email();

            //Splits the passed in data.
            string[] splitLine = preParseLine.Split();

            //Declares checkedMessageBody as null.
            string[] checkedMessageBody = null;

            //Temp URL List, holds URL's that have been found within the message body.
            List<string> tempURLList = new List<string>();

            do
            {
                //Try to parse the body, catches any errors that could occur and displays an error message.
                try
                {
                    //Email Subject
                    emailSubject = splitLine[1];

                    //If the email subject length is greater than 20 then the system will display an error message and move on to the next message.
                    if (emailSubject.Length > 20)
                    {

                        MessageBox.Show("Email subject exceeds supported bounds.");
                        break;

                    }

                    //If the email Subject is equal to SIR, the SIRParser is called.
                    if (emailSubject == "SIR")
                    {

                        SIRParser(preParseLine);

                    }
                    else
                    {

                        //Gets the messageID for the message, passes it the message type "E".
                        messageID = MessageIDBuilder("E");

                        //Sets the email address.
                        emailAddress = splitLine[0];

                        //Sets the message body.
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

                        //Checks the body of the message for URLs
                        string[] splitMessageBody = messageBody.Split();

                        //For each value in split message body, if the value starts with http:// or https:// the value is added to the quarantine list
                        //The value is then replaced in the message body to the replacementValue.
                        foreach (string val in splitMessageBody)
                        {
                            if (val.StartsWith("http://") || val.StartsWith("https://"))
                            {

                                tempURLList.Add(val);

                                string replacementValue = "<URL Quarantined>";

                                checkedMessageBody = splitMessageBody.Select(s => s.Replace(val, replacementValue)).ToArray();

                            }
                        }

                        //If the messageBody is empty, skip the current entry.
                        if (messageBody == string.Empty)
                        {
                            MessageBox.Show("Invalid Message Body, Try Again.");
                            break;
                        }

                        //Else join the message body with the parsed checkedMessageBody.
                        messageBody = string.Join(" ", checkedMessageBody);

                        //Set Quarantined URLs to quarantinedURLs variable
                        quarantinedURLs = tempURLList.ToArray();

                        //Build Email Object.
                        EmailParsed = new Email(emailAddress, emailSubject, quarantinedURLs, messageID, messageBody);

                        //Add the new Email Object to the ParsedMessages List.
                        ParsedMessages.Add(EmailParsed);
                    }
                    //Catch any exceptions from the message body.
                } catch (Exception err)
                {
                    MessageBox.Show("Invalid Message Body, Please Try Again.");
                    break;
                }
            }
            while (EmailParsed == null);
        }

        private void SIRParser(string preParseLine)
        {
            //Declares SIR attributes for use in the constructor
            string messageID = string.Empty;
            string messageBody = string.Empty;
            string[] urls = null;
            string emailAddress = string.Empty;
            string emailSubject = string.Empty;
            DateTime date = DateTime.MinValue;
            string sortCode = string.Empty;
            string incident = string.Empty;

            //Creates new SIR object.
            SIR SIRParsed = new SIR();

            //Declares checkedMessageBody as null.
            string[] checkedMessageBody = null;

            //Declares new Temp URL list.
            List<string> tempURLList = new List<string>();

            //declares and initalises the splitLine array which holds the split preParseLine
            string[] splitLine = preParseLine.Split();

            do
            {

                //Gets the messageID for the message, passes it the message type "E".
                messageID = MessageIDBuilder("E");

                //Sets the email address.
                emailAddress = splitLine[0];

                try
                {

                    //sets the emailSubject
                    emailSubject = splitLine[1];

                    //date parses the splitLine data.
                    date = DateTime.Parse(splitLine[2]);

                    //sets the sortCode
                    sortCode = splitLine[3];

                    //sets the Incident
                    incident = splitLine[4];

                    //Combines the MessageBody
                    List<string> tempList = new List<string>();
                    for (int i = 5; i < splitLine.Length; i++)
                    {
                        tempList.Add(splitLine[i]);
                    }

                    messageBody = string.Join(" ", tempList.ToArray());

                    //URL Check - Same as Email section
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

                    //Builds SIR using Constructor
                    SIRParsed = new SIR(date, sortCode, incident, emailAddress, emailSubject, urls, messageID, messageBody);

                    ParsedMessages.Add(SIRParsed);
                    //If any Exception occurs within the message body
                } catch (Exception err)
                {
                    MessageBox.Show("Invalid Message Body, Please Try Again.");
                    break;
                }

            } while (SIRParsed == null);

        }

        private void SMSParser(string preParseLine)
        {
            //Declares SMS attributes for use in the constructor
            string messageID;
            string messageBody;
            string sender;

            //Decalres a new SMS and stores it as SMS Parsed
            SMS SMSParsed = new SMS();

            //Declares and Initalises splitLine as the split preParseLine
            string[] splitLine = preParseLine.Split();

            do
            {
                //Gets the messageID for the message, passes it the message type "S".
                messageID = MessageIDBuilder("S");

                //Sets the phone number
                sender = splitLine[0];

                try
                {

                    //Recombine the message body.
                    List<string> tempList = new List<string>();
                    for (int i = 1; i < splitLine.Length; i++)
                    {
                        tempList.Add(splitLine[i]);
                    }

                    messageBody = string.Join(" ", tempList.ToArray());

                    //Message Body Text Speak Conversion
                    string checkResult = AbbreviationCheck(messageBody);

                    messageBody = checkResult;

                    //Build and output SMS object.
                    SMSParsed = new SMS(sender, messageID, messageBody);

                    ParsedMessages.Add(SMSParsed);
                    //If any errors occur in the message body parsing process, the error is caught and
                    //an error message is displayed 
                } catch (Exception err)
                {
                    MessageBox.Show("Invalid Message Body, Please Try Again.");
                    break;
                }
            }
            while (SMSParsed == null);
        }

        //TweetParser Method, responsible for parsing tweets.
        private void TweetParser(string preParseLine)
        {

            //Declares and Initalises the attributes used to build a tweet object.
            string messageID;
            string messageBody;
            string username;
            string[] mentions;
            string[] hashtags;

            //Creates a TweetParsed object.
            Tweet TweetParsed = new Tweet();

            //Splits the preParseLine and stores it in splitLine.
            string[] splitLine = preParseLine.Split();

            do
            {
                //Gets the messageID for the message, passes it the message type "T".
                messageID = MessageIDBuilder("T");

                //Sets the username
                username = splitLine[0];

                //If the username length is greater than 16 characters display an error message to the user.
                if (username.Length > 16)
                {
                    MessageBox.Show("The inputted username is outwidth Twitters username bounds, are you sure you have entered it correctly?");
                    break;
                }

                try
                {
                    //Rebuild the message body as a string.

                    List<string> tempList = new List<string>();
                    for (int i = 1; i < splitLine.Length; i++)
                    {
                        tempList.Add(splitLine[i]);
                    }

                    messageBody = string.Join(" ", tempList.ToArray());

                    //Message Body Length Check
                    //If the message body length is greater than 140 an error message is displayed to the user.
                    if (messageBody.Length > 140)
                    {

                        MessageBox.Show("The inputted message exceeds Twitters message bounds, are you sure you have entered it correctly?");
                        break;
                    }

                    //Message Body Text Speak Conversion
                    string checkResult = AbbreviationCheck(messageBody);

                    //Sets the message body to the Abbreviation Checked Body.
                    messageBody = checkResult;

                    //Checks the message body for mentions.
                    List<String> tempMentionsList = new List<string>();
                    foreach (string val in messageBody.Split())
                    {
                        //If a word starts with @, the system will add that word to the mentions array.
                        if (val.StartsWith("@"))
                        {
                            tempMentionsList.Add(val);
                        }
                    }

                    mentions = tempMentionsList.ToArray();

                    //Checks the message body for hashtags. 
                    List<String> tempHashtagList = new List<string>();
                    foreach (string val in messageBody.Split())
                    {
                        //If a word starts with #, the system will add that word to the hashtag array.
                        if (val.StartsWith("#"))
                        {
                            tempHashtagList.Add(val);
                        }
                    }

                    hashtags = tempHashtagList.ToArray();

                    //Outputting the Processed Tweet.
                    TweetParsed = new Tweet(username, hashtags, mentions, messageID, messageBody);

                    ParsedMessages.Add(TweetParsed);
                }
                //If any errors occur in the message body processing they will be caught here.
                catch (Exception)
                {
                    MessageBox.Show("Invalid Message Body, Please Try Again.");
                    break;
                }
            }
            while (TweetParsed == null);
        }
    }
}