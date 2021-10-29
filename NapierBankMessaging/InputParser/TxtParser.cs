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

            //TODO: Ask Zakwan about CSV Format Input

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

        private string TweetParser(string preParseLine)
        {

            string[] splitLine = preParseLine.Split();

            string messageID;
            string messageBody;
            string username;
            string[] mentions;
            string[] hashtags;

            //Username
            username = splitLine[0];

            //MessageBody

            List<string> tempList = new List<string>();
            for (int i = 1; i < splitLine.Length - 1; i++)
            {
                tempList.Add(splitLine[i]);
            }

            messageBody = string.Join(" ", tempList.ToArray());

            MessageBox.Show(messageBody);

            //....

            foreach (string val in preParseLine.Split())
            {



            }

            //Tweet TweetParsed = new Tweet();

            //return TweetParsed;

            return preParseLine;
        }
    }
}
