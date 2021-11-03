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

        Controller ControllerInstance;

        public ViewSignificantIncidentsPage(SystemController.Controller controllerInstance)
        {
            InitializeComponent();

            ControllerInstance = controllerInstance;

        }

        private void MainMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenuPage());
        }
    }
}
