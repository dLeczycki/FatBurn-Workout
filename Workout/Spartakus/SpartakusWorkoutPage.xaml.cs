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
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Workout.Spartakus
{
    /// <summary>
    /// Interaction logic for SpartakusWorkoutPage.xaml
    /// </summary>
    public partial class SpartakusWorkoutPage : Page
    {
        private MainWindow mainWindow;

        public const int EXERCISES_NUMBER = 10;
        public const int SERIES_NUMBER = 3;
        public const int TIME_OF_PREPARATION = 4;
        public const int PREPARATION_STAGE = 0;
        public const int EXERCISE_STAGE = 1;
        public const int BREAK_STAGE = 2;
        public const int LONG_BREAK_STAGE = 3;
        public const int TRAINING_FINISHED = -1;
        public static readonly string[] EXERCISE_NAMES = new string[10] { "PRZYSIADY", "PRZEJŚCIE Z NOGI NA NOGĘ", "WZNOSY RAMION", "PODSKOKI ZE ZMIANĄ NOGI", "SKRĘTY TUŁOWIA", "BIEG GÓRSKI", "PRZEJŚCIE Z NOGI NA NOGĘ ZE SKRĘTEM", "WYRZUTY HANTLI", "SKRĘTY TUŁOWIA Z PODNOSZENIEM NÓG", "DESKA" };

        public int trainingPreparationTime;
        public int exTime;
        public int brTime;
        public int lngBrTime;
        public int currTimeValue;

        public int trainingStage;
        public int exSeries;
        public int exNumber;

        DispatcherTimer dt;

        public SpartakusWorkoutPage(MainWindow mainWindow, int exTime, int brTime, int lngBrTime)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.exTime = exTime;
            this.brTime = brTime;
            this.lngBrTime = lngBrTime;
            resetTrainingParameters();
            train();
        }

        /// <summary>
        /// Starts training
        /// </summary>
        private void train()
        {
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += trainingTimeCounter;
            dt.Start();
        }

        /// <summary>
        /// Stops timer and resets parameters;
        /// </summary>
        private void stopTraining()
        {
            dt.Stop();
            resetTrainingParameters();
        }

        /// <summary>
        /// Manages training variables and sets current counter value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trainingTimeCounter(object sender, EventArgs e)
        {
            mainWindow.speechCountThree(currTimeValue-1);
            if (trainingStage == PREPARATION_STAGE) // short time for preparation
            {
                if (currTimeValue > 1) currTimeValue--;
                else
                {
                    currTimeValue = exTime;
                    trainingStage = EXERCISE_STAGE;
                    setExerciseWindow();
                    setSeriesLabel();
                    Console.Beep();
                }
            }
            else if (trainingStage == EXERCISE_STAGE) // training exercise
            {
                if (currTimeValue > 1) currTimeValue--;
                else if (exNumber < EXERCISES_NUMBER)
                {
                    currTimeValue = brTime;
                    trainingStage = BREAK_STAGE;
                    setExerciseWindowToBreak(true);
                    exNumber++;
                    Console.Beep();
                }
                else if (exSeries < SERIES_NUMBER)
                {
                    currTimeValue = lngBrTime;
                    trainingStage = LONG_BREAK_STAGE;
                    setExerciseWindowToBreak(false);
                    exNumber = 1;
                    exSeries++;
                    Console.Beep();
                }
                else if (exNumber == EXERCISES_NUMBER && exSeries == SERIES_NUMBER)
                {
                    resetTrainingParameters();
                    setExerciseWindowToFinish();
                    trainingStage = TRAINING_FINISHED;
                    dt.Stop();
                }
            }
            else if (trainingStage == BREAK_STAGE) // training short break
            {
                if (currTimeValue > 1) currTimeValue--;
                else
                {
                    currTimeValue = exTime;
                    trainingStage = EXERCISE_STAGE;
                    setExerciseWindow();
                    Console.Beep();
                }
            }
            else if (trainingStage == LONG_BREAK_STAGE) //training long break
            {
                if (currTimeValue > 1) currTimeValue--;
                else
                {
                    currTimeValue = exTime;
                    trainingStage = EXERCISE_STAGE;
                    setExerciseWindow();
                    setSeriesLabel();
                    Console.Beep();
                }
            }
            else //training finished
            {

            }

            labelCounter.Content = currTimeValue;
        }

        /// <summary>
        /// Resets training parameters to default values
        /// </summary>
        private void resetTrainingParameters()
        {
            currTimeValue = 0;
            trainingStage = 0;
            trainingPreparationTime = 4;
            currTimeValue = trainingPreparationTime;
            exNumber = 1;
            exSeries = 1;
            labelCounter.Content = 0;
            setExerciseWindowToPreparation();
            setSeriesLabel();
        }

        /// <summary>
        /// Sets exercise name
        /// </summary>
        private void setExerciseWindow()
        {
            //Exercise name label
            labelExName.Content = EXERCISE_NAMES[exNumber - 1];

            //Exercise image
            setExImage("/img/ex" + exNumber + ".png");

            //Exercise number label
            labelEx.Content = "Ćwiczenie: " + exNumber + "/" + EXERCISES_NUMBER;
        }

        /// <summary>
        /// Sets exercise window to initial value
        /// </summary>
        private void setExerciseWindowToPreparation()
        {
            labelExName.Content = "";
            setExImage("/img/preparation.png");
        }

        /// <summary>
        /// Sets exercise window with break label and photo
        /// </summary>
        /// <param name="isShort"></param>
        private void setExerciseWindowToBreak(bool isShort)
        {
            //Exercise name label
            labelExName.Content = "";

            //Exercise Image
            if (isShort) setExImage("/img/break.png");
            else setExImage("/img/longBreak.png");
        }

        /// <summary>
        /// Sets exercise window with finished photo
        /// </summary>
        private void setExerciseWindowToFinish()
        {
            labelExName.Content = "";
            setExImage("/img/trainingFinished.png");
            labelEx.Content = "";
            labelSeries.Content = "";
            labelCounter.Content = "BRAWO!";
        }

        /// <summary>
        /// Sets image of exercise with image of declared path
        /// </summary>
        /// <param name="imgPath"></param>
        private void setExImage(string imgPath)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(imgPath, UriKind.Relative);
            image.EndInit();

            imageEx.Source = image;
        }

        /// <summary>
        /// Sets series label beneath exercise label
        /// </summary>
        private void setSeriesLabel()
        {
            labelSeries.Content = "Seria: " + exSeries + "/" + SERIES_NUMBER;
        }

        /// <summary>
        /// Goes to previous page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.setWindow(MainWindow.SPARTAKUS_SETTINGS_PAGE);
        }

        /// <summary>
        /// Stops training and resets its progress.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            backButton.IsEnabled = true;
            stopTraining();
            pauseButton.IsEnabled = false;
            pauseImage.Source = imageSourceOfString("/icons/pause-dark.png");
            playButton.IsEnabled = true;
            playImage.Source = imageSourceOfString("/icons/play.png");
        }

        /// <summary>
        /// Pauses training
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            dt.Stop();
            pauseButton.IsEnabled = false;
            pauseImage.Source = imageSourceOfString("/icons/pause-dark.png");
            playButton.IsEnabled = true;
            playImage.Source = imageSourceOfString("/icons/play.png");
        }

        /// <summary>
        /// Plays training after pause
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            dt.Start();
            pauseButton.IsEnabled = true;
            pauseImage.Source = imageSourceOfString("/icons/pause.png");
            playButton.IsEnabled = false;
            playImage.Source = imageSourceOfString("/icons/play-dark.png");
        }

        /// <summary>
        /// Changes string path to image source
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private ImageSource imageSourceOfString(string path)
        {
            Uri imageUri = new Uri(path, UriKind.Relative);
            return new BitmapImage(imageUri);
        }

    }
}
