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
    /// Interaction logic for PushupsWorkoutPage.xaml
    /// </summary>
    public partial class PushupsWorkoutPage : Page
    {
        private MainWindow mainWindow;

        private int trainingDay;
        private int testResult;

        private int currentSeries;
        private int[] pushups;
        private int excess;

        private List<int[]> exerciseSeriesList;
        public PushupsWorkoutPage(MainWindow mainWindow, int trainingDay, int testResult)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.trainingDay = trainingDay;
            this.testResult = testResult;
            setExerciseSeriesList();
            resetTrainingParameters();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.setWindow(MainWindow.PUSHUPS_SETTINGS_PAGE);
        }

        private void nextSeriesButton_Click(object sender, RoutedEventArgs e)
        {
            currentSeries++;
            setTrainingParameters();
        }

        private void setTrainingParameters()
        {
            try
            {
                if (excess == 0)
                {
                    labelSeriesNumber.Content = "Seria " + currentSeries + "/" + pushups.Length;
                    labelCounter.Content = pushups[currentSeries];
                    labelCounterNext.Content = pushups[currentSeries + 1];
                }
                else if (excess == 1)
                {
                    labelSeriesNumber.Content = "Seria " + currentSeries + "/" + pushups.Length;
                    labelCounterNext.FontSize = 80;
                    labelCounterNext.Content = "Koniec";
                    labelCounter.FontSize = 180;
                    labelCounter.Content = "MAX";
                    excess++;
                }
                else if (excess == 2)
                {
                    labelCounterNext.Content = "";
                    labelCounter.Content = "Koniec";
                    nextSeriesButton.IsEnabled = false;
                }

            }
            catch (IndexOutOfRangeException ex)
            {
                labelCounterNext.FontSize = 100;
                labelCounterNext.Content = "MAX";
                excess++;
            }

        }

        private void resetTrainingParameters()
        {
            pushups = exerciseSeriesList[trainingDay - 1];
            currentSeries = 1;
            excess = 0;
            try
            {
                setTrainingParameters();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                setTestDayLayout();
            }
        }

        /// <summary>
        /// Sets pushups list according to parameters
        /// </summary>
        private void setExerciseSeriesList()
        {

            exerciseSeriesList = new List<int[]>();
            if (testResult <= 5)
            {
                exerciseSeriesList.Add(new int[] { 2, 3, 2, 2 });
                exerciseSeriesList.Add(new int[] { 3, 4, 2, 3 });
                exerciseSeriesList.Add(new int[] { 4, 5, 4, 4 });
                exerciseSeriesList.Add(new int[] { 5, 6, 4, 4 });
                exerciseSeriesList.Add(new int[] { 5, 6, 4, 4 });
                exerciseSeriesList.Add(new int[] { 5, 7, 5, 5 });
            }
            else if (testResult <= 10)
            {
                exerciseSeriesList.Add(new int[] { 5, 6, 4, 4 });
                exerciseSeriesList.Add(new int[] { 6, 7, 6, 6 });
                exerciseSeriesList.Add(new int[] { 8, 10, 7, 7 });
                exerciseSeriesList.Add(new int[] { 9, 11, 8, 8 });
                exerciseSeriesList.Add(new int[] { 10, 12, 9, 9 });
                exerciseSeriesList.Add(new int[] { 12, 13, 10, 10 });
            }
            else if (testResult <= 20)
            {
                exerciseSeriesList.Add(new int[] { 8, 9, 7, 7 });
                exerciseSeriesList.Add(new int[] { 9, 10, 8, 8 });
                exerciseSeriesList.Add(new int[] { 11, 13, 9, 9 });
                exerciseSeriesList.Add(new int[] { 12, 14, 10, 10 });
                exerciseSeriesList.Add(new int[] { 13, 15, 11, 11 });
                exerciseSeriesList.Add(new int[] { 14, 16, 13, 13 });
            }
            else if (testResult <= 25)
            {
                exerciseSeriesList.Add(new int[] { 12, 17, 13, 13 });
                exerciseSeriesList.Add(new int[] { 14, 19, 14, 14 });
                exerciseSeriesList.Add(new int[] { 16, 21, 15, 15 });
                exerciseSeriesList.Add(new int[] { 18, 22, 16, 16 });
                exerciseSeriesList.Add(new int[] { 20, 25, 20, 20 });
                exerciseSeriesList.Add(new int[] { 23, 28, 22, 22 });
            }
            else if (testResult <= 30)
            {
                exerciseSeriesList.Add(new int[] { 14, 18, 14, 14 });
                exerciseSeriesList.Add(new int[] { 20, 25, 15, 15 });
                exerciseSeriesList.Add(new int[] { 20, 27, 18, 18 });
                exerciseSeriesList.Add(new int[] { 21, 25, 21, 21 });
                exerciseSeriesList.Add(new int[] { 25, 29, 25, 25 });
                exerciseSeriesList.Add(new int[] { 29, 33, 29, 29 });
            }
            else if (testResult <= 35)
            {
                exerciseSeriesList.Add(new int[] { 17, 19, 15, 15 });
                exerciseSeriesList.Add(new int[] { 10, 10, 13, 13, 10, 10, 9 });
                exerciseSeriesList.Add(new int[] { 13, 13, 15, 15, 12, 12, 10 });
            }
            else if (testResult <= 40)
            {
                exerciseSeriesList.Add(new int[] { 22, 24, 20, 20 });
                exerciseSeriesList.Add(new int[] { 15, 15, 18, 18, 15, 15, 14 });
                exerciseSeriesList.Add(new int[] { 18, 18, 20, 20, 17, 17, 15 });
            }
            else if (testResult <= 45)
            {
                exerciseSeriesList.Add(new int[] { 27, 29, 25, 25 });
                exerciseSeriesList.Add(new int[] { 19, 19, 22, 22, 18, 18, 22 });
                exerciseSeriesList.Add(new int[] { 20, 20, 24, 24, 20, 20, 22 });
            }
            else if (testResult <= 50)
            {
                exerciseSeriesList.Add(new int[] { 30, 34, 30, 30 });
                exerciseSeriesList.Add(new int[] { 19, 19, 23, 23, 19, 19, 22 });
                exerciseSeriesList.Add(new int[] { 20, 20, 27, 27, 21, 21, 21 });
            }
            else if (testResult <= 55)
            {
                exerciseSeriesList.Add(new int[] { 30, 39, 35, 35 });
                exerciseSeriesList.Add(new int[] { 20, 20, 23, 23, 20, 20, 18, 18 });
                exerciseSeriesList.Add(new int[] { 22, 22, 30, 30, 25, 25, 18, 18 });
            }
            else if (testResult <= 60)
            {
                exerciseSeriesList.Add(new int[] { 30, 44, 40, 40 });
                exerciseSeriesList.Add(new int[] { 22, 22, 27, 27, 24, 23, 18, 18 });
                exerciseSeriesList.Add(new int[] { 26, 26, 33, 33, 26, 26, 22, 22 });
            }
            else if (testResult > 60)
            {
                exerciseSeriesList.Add(new int[] { 35, 49, 45, 45 });
                exerciseSeriesList.Add(new int[] { 22, 22, 30, 30, 24, 24, 18, 18 });
                exerciseSeriesList.Add(new int[] { 28, 28, 35, 35, 27, 27, 23, 23 });
            }
        }

        private void setTestDayLayout()
        {
            labelCounter.FontSize = 92;
            labelCounter.Content = "Dzisiaj wykonaj test!";
            labelSeriesNumber.Content = "";
            nextSeriesButton.IsEnabled = false;
        }


    }
}
