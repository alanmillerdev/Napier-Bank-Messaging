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
        //Initalises ControllerInstance.
        private Controller ControllerInstance;

        //Initalises Message List
        private List<Message> MessageList = new List<Message>();

        //Initalises the currentMessageIndex int.
        private int currentMessageIndex = 0;

        //Initalises the maxMessageIndex int.
        private int maxMessageIndex = 0;

        //InputDataViewTxtFilePage Constructor
        public InputDataViaTxtFilePage(Controller controllerInstance)
        {
            InitializeComponent();

            //Sets the ControllerInstance variable to the passed in controllerInstance.
            ControllerInstance = controllerInstance;

            //Sets the Instruction Text that will show up on the UI.
            InstructionBox.Text = "To start, upload a txt file. \n" +
                "WARNING: This file must follow the same formatting as the manual input page. " +
                "Each line of the file will be output as one message.";
        }

        //Method that allows the user to navigate back to the Main Menu.
        private void MainMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenuPage(ControllerInstance));
        }

        //Method that allows the user to upload a file to the application for parsing.
        private void TxtFileInput_Click(object sender, RoutedEventArgs e)
        {

            //MessageList is set to the returned value of the TxtFileUploadMessageParser.
            MessageList = ControllerInstance.TxtFileUploadMessageParser();

            //If the message list returns empty, nothing is updated.
            if (MessageList.Count == 0)
            {
                
            }
            else
            {
                //Outputs the inital data.
                MessageIDOutput.Text = MessageList[0].messageID;
                MessageBodyOutput.Text = MessageList[0].messageBody;

                //Information Output
                NumberOfMsgParsedLbl.Content = "Number of Messages Parsed: " + MessageList.Count;

                //Max Message Count Update
                maxMessageIndex = MessageList.Count;
            }
        }

        //Method to update the UI to display the next message.
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

        //Method to update the UI to display the previous message.
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
