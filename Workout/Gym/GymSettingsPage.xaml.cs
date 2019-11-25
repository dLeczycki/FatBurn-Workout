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
            mainWindow.setWindow(MainWindow.GYM_EXPLANATION_PAGE);
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!setGymParameters()) throw new Exception();

                mainWindow.setWindow(MainWindow.GYM_WORKOUT_PAGE);
            }
            catch (Exception ex)
            {
                MainWindow.Message_WrongSpartakusParameters();
            }
        }

        /// <summary>
        /// Sets gym training, and breaks times.
        /// </summary>
        /// <returns></returns>
        private bool setGymParameters()
        {
            int[] values = new int[3];
            string[] parameters = new string[3];
            parameters[0] = textExTime.Text;
            parameters[1] = textBrTime.Text;
            parameters[2] = textLngBrTime.Text;

            for (int i = 0; i < 3; i++)
            {
                try
                {
                    values[i] = Int32.Parse(parameters[i]);
                    if (values[i] < 1) throw new Exception();
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            mainWindow.exTime = values[0];
            mainWindow.brTime = values[1];
            mainWindow.lngBrTime = values[2];
            return true;
        }
    }
}
