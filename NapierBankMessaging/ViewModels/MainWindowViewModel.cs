using System;
using System.Windows.Input;
using System.Windows;
using NapierBankMessaging.Commands;
using NapierBankMessaging.ViewModels;
using NapierBankMessaging.Serialisation;
using Microsoft.Win32;


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

            ImportBtnText = "Import JSON File";

            ImportJSONDataCommand = new RelayCommand(ImportBtnClick);

        }

        private void ImportBtnClick()
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "JSON Files (*.JSON)|*.JSON|All files (*.*)|*.*";
            file.InitialDirectory = @"c:\";
            file.FilterIndex = 1;
            file.Multiselect = false;

            if (file.ShowDialog() == true)
            {
                //TEMP for CSV Testing
                var Instance = new CSVHandler();
                Instance.CSVInput(file.FileName);
            }
        }
    }
}