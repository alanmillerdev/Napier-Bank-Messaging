using NapierBankMessaging.ViewModels;
using System;
using System.Windows.Input;
using NapierBankMessaging.Commands;
using System.Windows;

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
            MessageBox.Show("Functionality Working");
        }
    }
}