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

        private Controller ControllerInstance = new Controller();

        private List<Message> MessageList = new List<Message>();

        public InputDataPage()
        {
            InitializeComponent();
        }

        private void MainMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenuPage());
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
                MessageIDOutput.Text = MessageList[0].messageID;
                MessageBodyOutput.Text = MessageList[0].messageBody;
            }
        }
    }
}