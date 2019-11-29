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
    /// Interaction logic for GymSettingsPage.xaml
    /// </summary>
    public partial class GymSettingsPage : Page
    {
        private MainWindow mainWindow;
        public GymSettingsPage(MainWindow mainWindow)
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
            setGymParameters();
            mainWindow.setWindow(MainWindow.GYM_WORKOUT_PAGE);      
        }

        /// <summary>
        /// Sets gym training, and breaks times.
        /// </summary>
        /// <returns></returns>
        private bool setGymParameters()
        {
            if (cbSchoulder.IsChecked == true) mainWindow.muscles.Add("schoulder");
            if (cbAbs.IsChecked == true) mainWindow.muscles.Add("abs");
            if (cbArm.IsChecked == true) mainWindow.muscles.Add("arm");
            if (cbChest.IsChecked == true) mainWindow.muscles.Add("chest");
            if (cbLeg.IsChecked == true) mainWindow.muscles.Add("leg");
            if (cbBack.IsChecked == true) mainWindow.muscles.Add("back");

            return true;
        }
    }
}
