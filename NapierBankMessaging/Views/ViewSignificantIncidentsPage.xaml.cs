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
    /// Interaction logic for ViewSignificantIncidentsPage.xaml
    /// </summary>
    public partial class ViewSignificantIncidentsPage : Page
    {

        //Initalising ControllerInstance
        Controller ControllerInstance;

        //ViewSignificantIncidentsPage constructor
        public ViewSignificantIncidentsPage(SystemController.Controller controllerInstance)
        {
            InitializeComponent();

            //Set the value of the ControllerInstance varible to the passed in controllerInstance
            ControllerInstance = controllerInstance;

            //Calls the populateListView method that populates the significant incidents into the list view.
            PopulateListView();

        }

        //Method to allow the user to return to the main menu.
        private void MainMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenuPage(ControllerInstance));
        }

        //PopulateListView method that is responsible for populating the list view with SIR's.
        private void PopulateListView()
        {
            List<SIR> sirList = ControllerInstance.getSIRList();

            for (int i = 0; i < sirList.Count; i++)
            {
                SIRList.Items.Add(sirList[i]);
            }
        }
    }
}
