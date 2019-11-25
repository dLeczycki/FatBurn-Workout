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

namespace Workout.Weider
{
    /// <summary>
    /// Interaction logic for WeiderSettingsPage.xaml
    /// </summary>
    public partial class WeiderSettingsPage : Page
    {
        private MainWindow mainWindow;
        public WeiderSettingsPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.setWindow(MainWindow.WEIDER_MAIN_PAGE);
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!setWeiderParameters()) throw new Exception();

                mainWindow.setWindow(MainWindow.WEIDER_WORKOUT_PAGE);
            }
            catch (Exception ex)
            {
                MainWindow.Message_WrongWeiderParameters();
            }
        }

        /// <summary>
        /// Sets weider training, and breaks times.
        /// </summary>
        /// <returns></returns>
        private bool setWeiderParameters()
        {
            int[] values = new int[4];
            string[] parameters = new string[4];
            parameters[0] = textExTime.Text;
            parameters[1] = textBrTime.Text;
            parameters[2] = textLngBrTime.Text;
            parameters[3] = textProgress.Text;

            for (int i = 0; i < 4; i++)
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
            mainWindow.progress = values[3];

            return true;
        }
    }
}
