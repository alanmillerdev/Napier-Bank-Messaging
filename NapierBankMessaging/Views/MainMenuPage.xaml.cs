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

        private Controller ControllerInstance = new Controller();

        public MainMenuPage()
        {
            InitializeComponent();
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

        private void ViewQuarantineNavigationBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewQuarantineListPage(ControllerInstance));
        }
    }
}
