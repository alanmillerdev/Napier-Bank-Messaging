﻿using Microsoft.Win32;
using NapierBankMessaging.InputParser;
using NapierBankMessaging.MessageTypes;
using NapierBankMessaging.Serialisation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            //jsonHandler.JSONOutput(returnedMessages);

            return returnedMessages;
        }

        public List<Message> ManualInputMessageParser(string msgHeader, string msgBody)
        {

                string[] data = { msgHeader + " " + msgBody };
                List<Message> messageReturn = new List<Message>();
                var ParserInstance = new TxtParser();
                messageReturn = ParserInstance.TXTParser(data);

            return messageReturn;
        }

        
    }
}
