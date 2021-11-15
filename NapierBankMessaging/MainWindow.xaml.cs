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
        //Creates a new instance of the Controller and stores it within the ControllerInstance variable.
        private Controller ControllerInstance = new Controller();

        //MainWindow constructor, is responsible for loading the main menu page into the application window on start.
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;

            this.Closing += MainWindow_Closing;
        }

        //Method that creates a new instance of the Main Menu Page and passes it the Controller Instance for use.
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new MainMenuPage(ControllerInstance));
        }

        //Upon an attempt to close the Window, this method will run, prompting the user with a choice.
        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
           
            string msg = "Are you sure you want to quit?";
            //Message Box that asks the user if they would like to quit the application or not.
            MessageBoxResult result = MessageBox.Show(msg,"Close Napier Bank Messaging?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            //If the user selects no, the program will not close, otherwise, the application will close and save the data.
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
