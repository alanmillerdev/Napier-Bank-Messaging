using NapierBankMessaging.SystemController;
using System;
using System.Windows;
using System.Windows.Controls;

namespace NapierBankMessaging.Views
{
    /// <summary>
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        //Initalises the ControllerInstance.
        private Controller ControllerInstance;

        //Initalises the TimeOfDay variable.
        private string TimeOfDay;

        public MainMenuPage(Controller controllerInstance)
        {
            InitializeComponent();

            //Sets the value of the local ControllerInstance to the passed in controllerInstance.
            ControllerInstance = controllerInstance;

            //Gets the current time and sets a message according to what time it is.
            if (DateTime.Now.Hour <= 12)
            {
                TimeOfDay = "Good Morning, what would you like to do today?";
            }   
            else if (DateTime.Now.Hour <= 16)
            {
                TimeOfDay = "Good Afternoon, what would you like to do today?";
            }
            else if (DateTime.Now.Hour <= 24)
            {
                TimeOfDay = "Good Evening, what would you like to do today?";
            }
            //Sets the header content to the calculated TimeOfDay message.
            Header.Content = TimeOfDay;
        }

        //Method that allows the user to Navigate to the Data Input Page.
        private void InputDataNavigationBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new InputDataPage(ControllerInstance));
        }

        //Method that allows the user to navigate to the Data Input Via Text File page.
        private void InputDataViaTxtFileNavigationBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new InputDataViaTxtFilePage(ControllerInstance));
        }

        //Method that allows the user to navigate to the View Significant Incidents page.
        private void ViewSignificantIncidentsNavigationBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewSignificantIncidentsPage(ControllerInstance));
        }

        //Method that allows the user to navigate to the View Tweet Trends page.
        private void ViewTweetTrendsNavigationBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewTweetTrendsPage(ControllerInstance));
        }

        //Method that allows the user to navigate to the View Tweet Mentions page.
        private void ViewTweetMentionsNavigationBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewTweetMentionsPage(ControllerInstance));
        }

        //Method that allows the user to navigate to the View Quarantined URL's page.
        private void ViewQuarantineNavigationBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewQuarantineListPage(ControllerInstance));
        }
    }
}
