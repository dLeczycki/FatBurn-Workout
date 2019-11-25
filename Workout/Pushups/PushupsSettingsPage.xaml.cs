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
    /// Interaction logic for PushupsSettingsPage.xaml
    /// </summary>
    public partial class PushupsSettingsPage : Page
    {
        private MainWindow mainWindow;
        public PushupsSettingsPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        /// <summary>
        /// Goes back to previous page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.setWindow(MainWindow.PUSHUPS_EXPLANATION_PAGE);
        }

        /// <summary>
        /// Goes forward to the next page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!setPushupsParameters()) throw new Exception();

                mainWindow.setWindow(MainWindow.PUSHUPS_WORKOUT_PAGE);
            }
            catch (Exception ex)
            {
                MainWindow.Message_WrongSpartakusParameters();
            }
        }

        /// <summary>
        /// Reads training parameters from form.
        /// </summary>
        /// <returns></returns>
        private bool setPushupsParameters()
        {
            int[] values = new int[2];
            string[] parameters = new string[2];
            parameters[0] = textTestResult.Text;
            parameters[1] = textTrainingDay.Text;

            for (int i = 0; i < 2; i++)
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

            mainWindow.testResult = values[0];
            mainWindow.trainingDay = values[1];
            return true;
        }
    }
}
