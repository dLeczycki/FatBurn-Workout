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

namespace Workout.Pushups
{
    /// <summary>
    /// Interaction logic for PushupsExplanationPage.xaml
    /// </summary>
    public partial class PushupsExplanationPage : Page
    {
        private MainWindow mainWindow;
        public PushupsExplanationPage(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.setWindow(MainWindow.PUSHUPS_MAIN_PAGE);
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.setWindow(MainWindow.PUSHUPS_SETTINGS_PAGE);
        }
    }
}
