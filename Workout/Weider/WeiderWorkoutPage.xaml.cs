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

namespace Workout.Weider
{
    /// <summary>
    /// Interaction logic for WeiderWorkoutPage.xaml
    /// </summary>
    public partial class WeiderWorkoutPage : Page
    {
        private MainWindow mainWindow;

        public const int EXERCISES_NUMBER = 6;
        public int EX_REPEAT_NUMBER = 6;
        public int SERIES_NUMBER = 3;
        public const int TIME_OF_PREPARATION = 5;
        public const int PREPARATION_STAGE = 0;
        public const int EXERCISE_STAGE = 1;
        public const int BREAK_STAGE = 2;
        public const int LONG_BREAK_STAGE = 3;
        public const int TRAINING_FINISHED = -1;
        public static readonly string[] EXERCISE_NAMES = new string[6] { "ĆWICZENIE PIERWSZE", "ĆWICZENIE DRUGIE", "ĆWICZENIE TRZECIE", "ĆWICZENIE CZWARTE", "ĆWICZENIE PIĄTE", "ĆWICZENIE SZÓSTE"};
        public static readonly string[] EXERCISE_DESCRIPTION = new string[6] 
            {   "Kładziemy się na płaskim podłożu z rękami wzdłuż tułowia. Podnosimy na zmianę raz jedną, raz drugą nogę pamiętając o zachowaniu kąta 90 stopni w kolanie i biodrze. Podczas podnoszeń, unosimy jednocześnie barki bez odrywania tułowia od podłoża."
               ,"Kładziemy się na płaskim podłożu, unosimy jednocześnie obie nogi pamiętając o odpowiednim kącie nachylenia i uniesieniu barków. Jeśli to nam pomoże, można objąć kolana dłońmi, lecz nie przytrzymujmy ich zbyt mocno."
               ,"Kładziemy się na płaskim podłożu z rękami splecionymi na karku. Podnosimy na zmianę raz jedną, raz drugą nogę pamiętając o zachowaniu kąta 90 stopni w kolanie i biodrze. Podczas podnoszeń, unosimy jednocześnie barki bez odrywania tułowia od podłoża."
               ,"Kładziemy się na płaskim podłożu z rękami splecionymi na karku. Unosimy jednocześnie obie nogi pamiętając o odpowiednim kącie nachylenia i uniesieniu barków."
               ,"Kładziemy się na płaskim podłożu, zaplatamy ręce na karku i unosimy klatkę piersiową. Podnosimy raz jedną, raz drugą nogę, nie zatrzymujemy ich w momencie największego napięcia mięśni, a wykonujemy ruch przypominający nożyce zmieniając nogi do 15 razy."
               ,"Kładziemy się na płaskim podłożu, unosimy część barkową tułowia, jednocześnie podnosząc obie nogi."
            };

        public int trainingPreparationTime;
        public int exTime;
        public int brTime;
        public int lngBrTime;
        public int progress;
        public int currTimeValue;

        public int trainingStage;
        public int exSeries;
        public int exNumber;
        public int repNumber;

        DispatcherTimer dt;
        public WeiderWorkoutPage(MainWindow mainWindow, int exTime, int brTime, int lngBrTime, int progress)
        {

            InitializeComponent();
            this.mainWindow = mainWindow;
            this.exTime = exTime;
            this.brTime = brTime;
            this.lngBrTime = lngBrTime;
            this.progress = progress;
            resetTrainingParameters();
            train();
        }

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
            SERIES_NUMBER = 3;
            if (progress == 1)  { EX_REPEAT_NUMBER = 6; SERIES_NUMBER = 1; } 
            else if (progress >= 2 && progress <= 3) { EX_REPEAT_NUMBER = 6; SERIES_NUMBER = 2; }
            else if (progress >= 4 && progress <= 6) { EX_REPEAT_NUMBER = 6; }
            else if (progress >= 7 && progress <= 10) { EX_REPEAT_NUMBER = 8; }
            else if (progress >= 11 && progress <= 14) { EX_REPEAT_NUMBER = 10; }
            else if (progress >= 15 && progress <= 18) { EX_REPEAT_NUMBER = 12; }
            else if (progress >= 19 && progress <= 22) { EX_REPEAT_NUMBER = 14; }
            else if (progress >= 23 && progress <= 26) { EX_REPEAT_NUMBER = 16; }
            else if (progress >= 27 && progress <= 30) { EX_REPEAT_NUMBER = 18; }
            else if (progress >= 31 && progress <= 34) { EX_REPEAT_NUMBER = 20; }
            else if (progress >= 35 && progress <= 38) { EX_REPEAT_NUMBER = 22; }
            else if (progress >= 39 && progress <= 42) { EX_REPEAT_NUMBER = 24; }
            else EX_REPEAT_NUMBER = 26;


            if (trainingStage == PREPARATION_STAGE) // short time for preparation
            {
                if (currTimeValue > 0) currTimeValue--;
                else
                {
                    currTimeValue = exTime;
                    trainingStage = EXERCISE_STAGE;
                    setExerciseWindow();
                    setSeriesLabel();
                }
            }


            else if (trainingStage == EXERCISE_STAGE) // training exercise
            {
                if (currTimeValue > 0) currTimeValue--;
                else if (repNumber < EX_REPEAT_NUMBER)
                {
                    currTimeValue = brTime;
                    trainingStage = BREAK_STAGE;
                    setExerciseWindowToBreak(true);
                    repNumber++;
                }
                else if (exNumber < EXERCISES_NUMBER)
                {
                    currTimeValue = brTime;
                    trainingStage = BREAK_STAGE;
                    setExerciseWindowToBreak(true);
                    exNumber++;
                    repNumber = 1;
                }
                else if (exSeries < progress)
                {
                    currTimeValue = lngBrTime;
                    trainingStage = LONG_BREAK_STAGE;
                    setExerciseWindowToBreak(false);
                    exNumber = 1;
                    repNumber = 1;
                    exSeries++;
                }
                else if (exNumber == EXERCISES_NUMBER && exSeries == progress && repNumber == EX_REPEAT_NUMBER)
                {
                    resetTrainingParameters();
                    setExerciseWindowToFinish();
                    trainingStage = TRAINING_FINISHED;
                    dt.Stop();
                }
            }


            else if (trainingStage == BREAK_STAGE) // training short break
            {
                if (currTimeValue > 0) currTimeValue--;
                else
                {
                    currTimeValue = exTime;
                    trainingStage = EXERCISE_STAGE;
                    setExerciseWindow();
                }
            }
            else if (trainingStage == LONG_BREAK_STAGE) //training long break
            {
                if (currTimeValue > 0) currTimeValue--;
                else
                {
                    currTimeValue = exTime;
                    trainingStage = EXERCISE_STAGE;
                    setExerciseWindow();
                    setSeriesLabel();
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
            repNumber = 1;
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
            labelExDescription.Content = EXERCISE_DESCRIPTION[exNumber - 1];

            //Exercise image
            setExImage("/img/wEx" + exNumber + ".png");

            //Exercise number label
            labelEx.Content = "Ćwiczenie: " + exNumber + "/" + EXERCISES_NUMBER;
            labelExRep.Content = "Powtórzenie: " + repNumber + "/" + EX_REPEAT_NUMBER;
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
            mainWindow.setWindow(MainWindow.WEIDER_SETTINGS_PAGE);
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
