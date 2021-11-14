using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using NapierBankMessaging.SystemController;
using NapierBankMessaging.MessageTypes;

namespace NapierBankMessaging.Views
{
    public partial class InputDataViaTxtFilePage : Page
    {
        private Controller ControllerInstance;

        private List<Message> MessageList = new List<Message>();

        private int currentMessageIndex = 0;

        private int maxMessageIndex = 0;

        public InputDataViaTxtFilePage(Controller controllerInstance)
        {
            InitializeComponent();

            ControllerInstance = controllerInstance;

            InstructionBox.Text = "To start, upload a txt file. \n" +
                "WARNING: This file must follow the same formatting as the manual input page. " +
                "Each line of the file will be output as one message.";
        }

        private void MainMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenuPage(ControllerInstance));
        }

        private void TxtFileInput_Click(object sender, RoutedEventArgs e)
        {

            MessageList = ControllerInstance.TxtFileUploadMessageParser();

            if (MessageList.Count == 0)
            {

            }
            else
            {
                //Initial Output
                MessageIDOutput.Text = MessageList[0].messageID;
                MessageBodyOutput.Text = MessageList[0].messageBody;

                //Information Output
                NumberOfMsgParsedLbl.Content = "Number of Messages Parsed: " + MessageList.Count;

                //Max Message Count Update
                maxMessageIndex = MessageList.Count;
            }
        }

        private void NextMessageBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentMessageIndex >= maxMessageIndex)
            {
                MessageBox.Show("No more messages to show!");
            } else
            {
                MessageIDOutput.Text = MessageList[currentMessageIndex].messageID;
                MessageBodyOutput.Text = MessageList[currentMessageIndex].messageBody;
                currentMessageIndex++;
            }
        }

        private void BackMessageBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentMessageIndex <= 0)
            {
                MessageBox.Show("No more messages to show!");
            }
            else
            {
                currentMessageIndex--;
                MessageIDOutput.Text = MessageList[currentMessageIndex].messageID;
                MessageBodyOutput.Text = MessageList[currentMessageIndex].messageBody;
            }
        }
    }
}
