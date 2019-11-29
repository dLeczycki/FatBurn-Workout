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

namespace Workout.Spartakus
{
    /// <summary>
    /// Interaction logic for SpartakusSettingsPage.xaml
    /// </summary>
    public partial class SpartakusSettingsPage : Page
    {
        private MainWindow mainWindow;
        public SpartakusSettingsPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.setWindow(MainWindow.SPARTAKUS_EXPLANATION_PAGE);
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(!setSpartacusParameters()) throw new Exception();

                mainWindow.setWindow(MainWindow.SPARTAKUS_WORKOUT_PAGE);
            }
            catch(Exception ex)
            {
                MainWindow.Message_WrongSpartakusParameters();
            }
        }

        /// <summary>
        /// Sets spartakus training, and breaks times.
        /// </summary>
        /// <returns></returns>
        private bool setSpartacusParameters()
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

        public void setSpartakusFirstTime(int number)
        {
            textExTime.Text = number.ToString();
        }

        public void setSpartakusSecondTime(int number)
        {
            textBrTime.Text = number.ToString();
        }

        public void setSpartakusThirdTime(int number)
        {
            textLngBrTime.Text = number.ToString();
        }
    }
}
