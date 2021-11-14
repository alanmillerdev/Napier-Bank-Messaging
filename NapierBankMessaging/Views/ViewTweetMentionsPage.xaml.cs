﻿using NapierBankMessaging.SystemController;
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

        Controller ControllerInstance;

        public ViewTweetMentionsPage(Controller controllerInstance)
        {
            InitializeComponent();

            ControllerInstance = controllerInstance;

            PopulateMentionsList();

        }

        private void MainMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenuPage(ControllerInstance));
        }

        public void PopulateMentionsList()
        {

            Dictionary<string, int> trendList = ControllerInstance.GetMentions();

            foreach (KeyValuePair<string, int> entry in trendList)
            {
                TweetMentionsList.Items.Add(entry);
            }
        }
    }
}
