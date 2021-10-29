using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Windows;
using NapierBankMessaging.MessageTypes;

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

                    //SMSParser(line);

                } else if (line.Split()[0].StartsWith("@"))
                {
                    TweetParser(line);
                }
                else
                {
                    try
                    {

                        new MailAddress(line.Split()[0]);

                        //EmailParser(line);

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

        /*
        private string[] EmailParser(string preParseLine)
        {

            string[] parsedEmail;



            return parsedEmail;
        }

        

        private string[] SMSParser(string preParseLine)
        {

            string[] SMSParsed;



            return SMSParsed;
        }
         
        */


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
                for (int i = 1; i < splitLine.Length - 1; i++)
                {
                    tempList.Add(splitLine[i]);
                }

                messageBody = string.Join(" ", tempList.ToArray());

                //Implement Properly
                if (messageBody.Length > 140)
                {

                    MessageBox.Show("Tweet body exceeds supported bounds.");
                    break;
                }

                //Username
                username = splitLine[0];

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
