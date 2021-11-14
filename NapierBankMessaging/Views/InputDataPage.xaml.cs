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

        private Controller ControllerInstance;

        private List<Message> MessageList = new List<Message>();

        public InputDataPage(Controller controllerInstance)
        {
            InitializeComponent();

            ControllerInstance = controllerInstance;

            InstructionBox.Text = "The message header is to contain the source of the message, for example: \n Tweet: @alan.miller \n SMS: +447797143800 \n Email / SIR: alan @miller.com \n" +
                "Message Body is to contain any other information, for example:\n " +
                "Tweet: Message Body: Good Morning #Twitter you all are all AAS this morning.\n" +
                "SMS: Message Body: lol you should have been there! \n" +
                "Email: Subject: IMPORTANT Message Body: Hey, I hope you have a good day, check out this link: http://scam.com\n" +
                "SIR: Subject: SIR Date:01/01/2000 Sort Code: 99-99-99 Incident Type: Theft Message Body: Hello, I would like to report a theft of my property.";
        }

        private void MainMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenuPage(ControllerInstance));
        }

        private void SubmitInputBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageHeaderInput.Text == string.Empty || MessageBodyInput.Text == string.Empty)
            {
                MessageBox.Show("Please Input Values for both the Message Header and Message Body");
            }
            else
            {
                MessageList = ControllerInstance.ManualInputMessageParser(MessageHeaderInput.Text, MessageBodyInput.Text);

                try
                {
                    MessageIDOutput.Text = MessageList[0].messageID;
                    MessageBodyOutput.Text = MessageList[0].messageBody;
                } catch (ArgumentOutOfRangeException err)
                {

                }
            }
        }
    }
}