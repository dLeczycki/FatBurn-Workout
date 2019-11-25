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

namespace Workout.Gym
{
    /// <summary>
    /// Interaction logic for GymExplanationPage.xaml
    /// </summary>
    public partial class GymExplanationPage : Page
    {
        private MainWindow mainWindow;
        public GymExplanationPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.setWindow(MainWindow.GYM_MAIN_PAGE);
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.setWindow(MainWindow.GYM_SETTINGS_PAGE);
        }

    }
}
