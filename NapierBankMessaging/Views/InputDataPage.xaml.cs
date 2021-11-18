using NapierBankMessaging.MessageTypes;
using NapierBankMessaging.SystemController;
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

namespace NapierBankMessaging.Views
{
    /// <summary>
    /// Interaction logic for InputDataPage.xaml
    /// </summary>
    public partial class InputDataPage : Page
    {
        //Initalises the ControllerInstance variable.
        private Controller ControllerInstance;

        //Initalises the MessageList variable.
        private List<Message> MessageList = new List<Message>();

        public InputDataPage(Controller controllerInstance)
        {
            InitializeComponent();

            //Updates the ControllerInstance variable to hold the passed in controllerInstance.
            ControllerInstance = controllerInstance;

            //Sets the text of the instruction box on the UI.
            InstructionBox.Text = "The message header is to contain the source of the message, for example: \n Tweet: @alan.miller \n SMS: +447797143800 \n Email / SIR: alan @miller.com \n" +
                "Message Body is to contain any other information, for example:\n " +
                "Tweet: Message Body: Good Morning #Twitter you all are all AAS this morning.\n" +
                "SMS: Message Body: lol you should have been there! \n" +
                "Email: Subject: IMPORTANT Message Body: Hey, I hope you have a good day, check out this link: http://scam.com\n" +
                "SIR: Subject: SIR Date:01/01/2000 Sort Code: 99-99-99 Incident Type: Theft Message Body: Hello, I would like to report a theft of my property.";
        }

        //Method to allow the user to navigate back to the Main Menu
        private void MainMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenuPage(ControllerInstance));
        }

        //Method responsible for submitting data to the Parser for parsing and dealing with the returned value.
        private void SubmitInputBtn_Click(object sender, RoutedEventArgs e)
        {
            //If either of the inputs are blank, display error message.
            if (MessageHeaderInput.Text == string.Empty || MessageBodyInput.Text == string.Empty)
            {
                MessageBox.Show("Please Input Values for both the Message Header and Message Body");
            }
            else
            {
               //MessageList set to hold the result of the parsed passed values.
                MessageList = ControllerInstance.ManualInputMessageParser(MessageHeaderInput.Text, MessageBodyInput.Text);

                //If the MessageList is returned empty, the application will catch the exception thrown caused by the MessageList not having a value at index 0
                try
                {
                    MessageIDOutput.Text = MessageList[0].messageID;
                    MessageBodyOutput.Text = MessageList[0].messageBody;
#pragma warning disable CS0168 // The variable 'err' is declared but never used
                } catch (ArgumentOutOfRangeException err)
#pragma warning restore CS0168 // The variable 'err' is declared but never used
                {

                }
            }
        }
    }
}