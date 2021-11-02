using System;
using System.Windows.Input;
using System.Windows;
using NapierBankMessaging.Commands;
using NapierBankMessaging.ViewModels;
using NapierBankMessaging.Serialisation;
using NapierBankMessaging.InputParser;
using Microsoft.Win32;
using System.Collections.Generic;
using NapierBankMessaging.MessageTypes;

namespace NapierBankMessaging.Views
{
    class MainWindowViewModel : BaseViewModel
    {

        public string HeaderText { get; private set; }
        public string ImportBtnText { get; private set; }

        public ICommand ImportJSONDataCommand { get; private set; }

        public MainWindowViewModel()
        {
            HeaderText = "Napier Bank Messaging";

            ImportBtnText = "Import txt File";

            ImportJSONDataCommand = new RelayCommand(ImportBtnClick);

        }

        private void ImportBtnClick()
        {
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
                List<Message> returnedMessages = ParserInstance.TXTParser(returnedTxt);

                Console.WriteLine();

            }
        }
    }
}