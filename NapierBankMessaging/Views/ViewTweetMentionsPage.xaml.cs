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
    /// Interaction logic for ViewTweetMentionsPage.xaml
    /// </summary>
    public partial class ViewTweetMentionsPage : Page
    {
        //Initalises a Controller instance.
        Controller ControllerInstance;

        //ViewTweetMentionsPage constructor
        public ViewTweetMentionsPage(Controller controllerInstance)
        {
            InitializeComponent();

            //Sets the ControllerInstance variable to the passed in instance.
            ControllerInstance = controllerInstance;
            
            //Calls the PopulateMentionsList method that is responsible for populating the list view.
            PopulateMentionsList();

        }

        //Method that allows the user to navigate back to the main menu.
        private void MainMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenuPage(ControllerInstance));
        }

        //Method to populate the the mentions list on the UI.
        public void PopulateMentionsList()
        {
            //Initialises a dictionary that holds a string and int variable.
            //Stores the result of the GetMentions method that returns dictionary data.
            Dictionary<string, int> trendList = ControllerInstance.GetMentions();

            //for each of the values in the dictionary add it to the list view.
            foreach (KeyValuePair<string, int> entry in trendList)
            {
                TweetMentionsList.Items.Add(entry);
            }
        }
    }
}
