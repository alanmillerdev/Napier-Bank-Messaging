using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessaging.InputParser;
using NapierBankMessaging.MessageTypes;
using System;
using System.Collections.Generic;

namespace Parse.Tests
{
    [TestClass]
    public class ParseTests
    {

        TxtParser TxtParserInstance = new TxtParser();

        [TestMethod]
        //Test Message Builder with Email Variable
        public void MessageIDBuilderEmailTest()
        {

            string result = TxtParserInstance.MessageIDBuilder("E");

            Assert.IsTrue(result.StartsWith("E"));

        }

        [TestMethod]
        //Test Message Builder with SMS Variable
        public void MessageIDBuilderSMSTest()
        {

            string result = TxtParserInstance.MessageIDBuilder("S");

            Assert.IsTrue(result.StartsWith("S"));

        }

        [TestMethod]
        //Test Message Builder with Tweet Variable
        public void MessageIDBuilderTweetTest()
        {

            string result = TxtParserInstance.MessageIDBuilder("T");

            Assert.IsTrue(result.StartsWith("T"));

        }

        [TestMethod]
        //Test Message Builder with Potential Variable
        public void MessageIDBuilderPotentialNewMessageTypeTest()
        {

            string result = TxtParserInstance.MessageIDBuilder("X");

            Assert.IsTrue(result.StartsWith("X"));

        }

        [TestMethod]
        //Test the abbreviation checker for message parsing.
        public void abbreviationCheckTest()
        {

            string actualResult = TxtParserInstance.AbbreviationCheck("hey Alan, hope you're having a great day, last night was crazy lmao");

            string expectedResult = "hey Alan, hope you're having a great day, last night was crazy <Laughing my a** off>";

            Assert.AreEqual(actualResult, expectedResult);

        }

        [TestMethod]
        //Test the email parser.
        public void emailParseTest()
        {

            string[] data = { "alan@miller.com" + " " + "IMPORTANT! Check out this link https://napier.ac.uk" };

            string[] urls = { "https://napier.ac.uk" };

            List<Message> getResult = TxtParserInstance.TXTParser(data);

            Email actualResult = (Email)getResult[0];

            Email expectedResult = new Email("alan@miller.com", "IMPORTANT!", urls, actualResult.messageID , "Check out this link <URL Quarantined>");

            //Testing if the Object is equal by checking if all of the values are the same as arrays seem to break normal Asserts.
            Assert.AreEqual(actualResult.messageID, expectedResult.messageID);
            Assert.AreEqual(actualResult.messageBody, expectedResult.messageBody);
            Assert.AreEqual(actualResult.eAddress, expectedResult.eAddress);
            Assert.AreEqual(actualResult.eSubject, expectedResult.eSubject);
            CollectionAssert.AreEqual(actualResult.qURLS, expectedResult.qURLS);
        }

        [TestMethod]
        //Test the SIR parser.
        public void SIRParseTest()
        {

            string[] data = { "alan@miller.com" + " " + "SIR 01/01/2000 99-99-99 Theft Hello, I would like to report a theft of my bank card." };

            string[] urls = { };

            List<Message> getResult = TxtParserInstance.TXTParser(data);

            SIR actualResult = (SIR)getResult[0];

            SIR expectedResult = new SIR(DateTime.Parse("01/01/2000"), "99-99-99", "Theft", "alan@miller.com", "SIR", urls, actualResult.messageID, "Hello, I would like to report a theft of my bank card.");

            //Testing if the Object is equal by checking if all of the values are the same as arrays seem to break normal Asserts.
            Assert.AreEqual(actualResult.messageID, expectedResult.messageID);
            Assert.AreEqual(actualResult.messageBody, expectedResult.messageBody);
            Assert.AreEqual(actualResult.eAddress, expectedResult.eAddress);
            Assert.AreEqual(actualResult.eSubject, expectedResult.eSubject);
            CollectionAssert.AreEqual(actualResult.qURLS, expectedResult.qURLS);
            Assert.AreEqual(actualResult.Date, expectedResult.Date);
            Assert.AreEqual(actualResult.sortCode, expectedResult.sortCode);
            Assert.AreEqual(actualResult.Incident, expectedResult.Incident);
        }

        [TestMethod]
        //Test the sms parser.
        public void smsParseTest()
        {

            string[] data = { "+447612165800" + " " + "lmao i'll brb" };

            List<Message> getResult = TxtParserInstance.TXTParser(data);

            SMS actualResult = (SMS)getResult[0];

            SMS expectedResult = new SMS("+447612165800", actualResult.messageID, "<Laughing my a** off> i'll <Be right back>");

            //Testing if the Object is equal by checking if all of the values are the same as arrays seem to break normal Asserts.
            Assert.AreEqual(actualResult.messageID, expectedResult.messageID);
            Assert.AreEqual(actualResult.messageBody, expectedResult.messageBody);
            Assert.AreEqual(actualResult.pNumber, expectedResult.pNumber);
        }

        [TestMethod]
        //Test the tweet parser.
        public void tweetParseTest()
        {

            string[] data = { "@alan.miller" + " " + "Good Morning @Twitter hope everyone is doing great #today ttyl" };

            string[] mentions = { "@Twitter" };

            string[] hashtags = { "#today" };

            List<Message> getResult = TxtParserInstance.TXTParser(data);

            Tweet actualResult = (Tweet)getResult[0];

            Tweet expectedResult = new Tweet("@alan.miller", hashtags, mentions, actualResult.messageID, "Good Morning @Twitter hope everyone is doing great #today <Talk to you later>");

            //Testing if the Object is equal by checking if all of the values are the same as arrays seem to break normal Asserts.
            Assert.AreEqual(actualResult.messageID, expectedResult.messageID);
            Assert.AreEqual(actualResult.messageBody, expectedResult.messageBody);
            Assert.AreEqual(actualResult.tUsername, expectedResult.tUsername);
            CollectionAssert.AreEqual(actualResult.hashtags, expectedResult.hashtags);
            CollectionAssert.AreEqual(actualResult.mentions, expectedResult.mentions);

        }

        [TestMethod]
        //Tests Tweet Body Error Prevention
        //If an exception is thrown the test passes.
        public void tweetParseTestFail()
        {
            try
            {
                //Invalid Data
                string[] data = { "@alan.miller" + " " + "" };

                List<Message> getResult = TxtParserInstance.TXTParser(data);

                //If no exception is thrown, the text fails.
                Assert.Fail(); 
            }
            catch (Exception)
            {
                // Upon catch of the Exception the test passes.
            }
        }

        [TestMethod]
        //Tests SMS Body Error Prevention
        //If an exception is thrown the test passes.
        public void SMSParseTestFail()
        {
            try
            {
                //Invalid Data
                string[] data = { "+447612165800" + " " + "" };

                List<Message> getResult = TxtParserInstance.TXTParser(data);

                //If no exception is thrown, the text fails.
                Assert.Fail();
            }
            catch (Exception)
            {
                // Upon catch of the Exception the test passes.
            }
        }

        [TestMethod]
        //Tests Email Body Error Prevention
        //If an exception is thrown the test passes.
        public void EmailParseTestFail()
        {
            try
            {
                //Invalid Data
                string[] data = { "alan@miller.com" + " " + "Subject" };

                List<Message> getResult = TxtParserInstance.TXTParser(data);

                //If no exception is thrown, the text fails.
                Assert.Fail();
            }
            catch (Exception)
            {
                // Upon catch of the Exception the test passes.
            }
        }

        [TestMethod]
        //Tests SIR Body Error Prevention
        //If an exception is thrown the test passes.
        public void SIRParseTestFail()
        {
            try
            {
                //Invalid Data
                string[] data = { "alan@miller.com" + " " + "SIR 01/01/01" };

                List<Message> getResult = TxtParserInstance.TXTParser(data);

                //If no exception is thrown, the text fails.
                Assert.Fail();
            }
            catch (Exception)
            {
                // Upon catch of the Exception the test passes.
            }
        }
    }
}