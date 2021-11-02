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
        public MainMenuPage()
        {
            InitializeComponent();
        }

        private void InputDataNavigationBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new InputDataPage());
        }

        private void InputDataViaTxtFileNavigationBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new InputDataViaTxtFilePage());
        }

        private void ViewSignificantIncidentsNavigationBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewSignificantIncidentsPage());
        }

        private void ViewTweetTrendsNavigationBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewTweetTrendsPage());
        }

        private void ViewQuarantineNavigationBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewQuarantineListPage());
        }
    }
}
