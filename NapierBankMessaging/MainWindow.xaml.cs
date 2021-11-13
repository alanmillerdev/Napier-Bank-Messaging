using NapierBankMessaging.SystemController;
using NapierBankMessaging.Views;
using System;
using System.ComponentModel;
using System.Windows;

namespace NapierBankMessaging
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Controller ControllerInstance = new Controller();

        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;

            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new MainMenuPage(ControllerInstance));
        }

        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
           
            string msg = "Are you sure you want to quit?";
            MessageBoxResult result = MessageBox.Show(msg,"Close Napier Bank Messaging?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                } 
            else
                {
                ControllerInstance.saveApplicationData();
            }
            }
        }
    }
