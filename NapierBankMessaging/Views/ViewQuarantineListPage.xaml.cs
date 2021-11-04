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

        Controller ControllerInstance;

        public ViewQuarantineListPage(SystemController.Controller controllerInstance)
        {
            InitializeComponent();

            ControllerInstance = controllerInstance;

            populateListView();

        }

        private void MainMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenuPage(ControllerInstance));
        }


        private void populateListView()
        {
            List<Tuple<string, string>> urlList = ControllerInstance.GetQuarantineList();

            for(int i = 0; i < urlList.Count; i++)
            {
                QuarantineList.Items.Add(urlList[i]);
            }
        }
    }
}
