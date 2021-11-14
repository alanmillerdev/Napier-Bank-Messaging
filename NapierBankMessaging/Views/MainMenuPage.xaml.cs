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

        private Controller ControllerInstance;

        private string TimeOfDay;

        public MainMenuPage(Controller controllerInstance)
        {
            InitializeComponent();

            ControllerInstance = controllerInstance;

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

            Header.Content = TimeOfDay;
        }

        private void InputDataNavigationBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new InputDataPage(ControllerInstance));
        }

        private void InputDataViaTxtFileNavigationBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new InputDataViaTxtFilePage(ControllerInstance));
        }

        private void ViewSignificantIncidentsNavigationBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewSignificantIncidentsPage(ControllerInstance));
        }

        private void ViewTweetTrendsNavigationBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewTweetTrendsPage(ControllerInstance));
        }

        private void ViewTweetMentionsNavigationBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewTweetMentionsPage(ControllerInstance));
        }

        private void ViewQuarantineNavigationBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewQuarantineListPage(ControllerInstance));
        }
    }
}
