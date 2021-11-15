using NapierBankMessaging.MessageTypes;
using NapierBankMessaging.SystemController;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace NapierBankMessaging.Views
{
    /// <summary>
    /// Interaction logic for ViewQuarantineListPage.xaml
    /// </summary>
    public partial class ViewQuarantineListPage : Page
    {
        //Initalising ControllerInstance.
        Controller ControllerInstance;

        //Constructor for ViewQuarantineListPage
        public ViewQuarantineListPage(SystemController.Controller controllerInstance)
        {
            InitializeComponent();

            //Sets the ControllerInstance variable to the passed in controllerInstance
            ControllerInstance = controllerInstance;

            //Method to Populates the list view with data.
            populateListView();

        }

        //Method that allows the user to navigate back to the main menu.
        private void MainMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenuPage(ControllerInstance));
        }

        //Method that populates the list view based on return data from the called Controller Method, GetQuarantineList
        private void populateListView()
        {
            List<Tuple<string, string>> urlList = ControllerInstance.GetQuarantineList();

            for (int i = 0; i < urlList.Count; i++)
            {
                QuarantineList.Items.Add(urlList[i]);
            }
        }
    }
}