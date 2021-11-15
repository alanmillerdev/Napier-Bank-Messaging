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
    /// Interaction logic for ViewTweetTrendsPage.xaml
    /// </summary>
    public partial class ViewTweetTrendsPage : Page
    {
        //Initalises a Controller Instance
        Controller ControllerInstance;

        //View Tweet Trends Page Constructor, is passed the controller instance from the previous page.
        public ViewTweetTrendsPage(Controller controllerInstance)
        {
            InitializeComponent();
            
            //Sets the controller instance to the ControllerInstance variable.
            ControllerInstance = controllerInstance;
            //Method call to populate the Trend List.
            PopulateTrendsList();
        }

        //Method that allows the user to navigate back to the main menu.
        private void MainMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenuPage(ControllerInstance));
        }

        //Method that populates the trend list.
        public void PopulateTrendsList()
        {

            //Initalises a dictionary trendList with a string and int variable.
            //trendList initalisation calls the GetTrends method from the controller.
            //which returns the data required.
            Dictionary<string, int> trendList = ControllerInstance.GetTrends();

            //For each of the values of the dictionary, add it to the TweetTrendsList on the UI.
            foreach (KeyValuePair<string, int> entry in trendList)
            {
                TweetTrendsList.Items.Add(entry);
            }
        }
    }
}