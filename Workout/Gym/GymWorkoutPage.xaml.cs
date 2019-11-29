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
using System.Collections.ObjectModel;
using System.Data;

namespace Workout.Gym
{
    class Muscles
    {
        Image image;
        string description;
    }
    /// <summary>
    /// Interaction logic for GymWorkoutPage.xaml
    /// </summary>

    public partial class GymWorkoutPage : Page
    {
        private MainWindow mainWindow;

        DispatcherTimer dt;

        public GymWorkoutPage(MainWindow mainWindow, List<string> muscles)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            Image img = new Image();
            theGrid.ShowGridLines = true;

            // RowDefinition row = ;
            //DataTable dt = new DataTable();

            //  int number = muscles.Count;
            //  string[] parties = new string[number];

            for (int item = 0; item < muscles.Count; item++)
            {
                Console.WriteLine(muscles[item]);
                img.BeginInit();
                img.Source = new BitmapImage(new Uri("/img/" + muscles[item] + ".png", UriKind.Relative));
                img.EndInit();

               // dt.Rows.Add(img);
                Console.WriteLine(img);
                theGrid.RowDefinitions.Add(new RowDefinition());
                theGrid.
                
                img.SetValue(Grid.RowProperty, item);
                img.SetValue(Grid.ColumnProperty, 0);
                

               
                
                //theGrid.Children.Add(img);
                //img.SetValue(Grid.RowProperty, item);

              //  sp.Children.Add(img);
                //theGrid.Children.Insert(item,img);
                
            }
            //this.mainWindow = new MainWindow();
           // theGrid.ItemsSource = dt.DefaultView;

            foreach (var item in muscles)
            {
                if (item == "Ramiona") { }
            }
            //  if(exSchoulder == true) setExImage("/img/schoulder.png");
            //  if(exAbs == true) setExImage("/img/abs.png");
            //  if(exArm == true) setExImage("/img/arm.png");
            //  if(exLeg == true) setExImage("/img/leg.png");
            //  if(exChest == true) setExImage("/img/chest.png");
            //  if(exBack == true) setExImage("/img/back.png");


            resetTrainingParameters();
            train();
        }
        public void changeImage()
        {
   
           
        }

        /// <summary>
        /// Starts training
        /// </summary>
        private void train()
        {
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
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
        /// Resets training parameters to default values
        /// </summary>
        private void resetTrainingParameters()
        {

            //labelCounter.Content = 0;
            setSeriesLabel();
        }

        /// <summary>
        /// Sets exercise window with break label and photo
        /// </summary>
        /// <param name="isShort"></param>
        private void setExerciseWindowToBreak(bool isShort)
        {
            //Exercise name label
            //labelExName.Content = "";

            //Exercise Image
           // if (isShort) setExImage("/img/break.png");
            //else setExImage("/img/longBreak.png");
        }

        /// <summary>
        /// Sets exercise window with finished photo
        /// </summary>
        private void setExerciseWindowToFinish()
        {
            //labelExName.Content = "";
            //setExImage("/img/trainingFinished.png");
            //labelEx.Content = "";
            //labelSeries.Content = "";
            //labelCounter.Content = "BRAWO!";
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

            //myImage1.Source = image;

        }

        /// <summary>
        /// Sets series label beneath exercise label
        /// </summary>
        private void setSeriesLabel()
        {
            //labelSeries.Content = "Seria: " + exSeries + "/" + SERIES_NUMBER;
        }

        /// <summary>
        /// Goes to previous page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backButton_Click(object sender, RoutedEventArgs e)
        {

            mainWindow.setWindow(MainWindow.GYM_SETTINGS_PAGE);
            // theGrid.RowDefinitions.Clear();
            theGrid.ShowGridLines = false;
            theGrid.RowDefinitions.RemoveRange(0, theGrid.RowDefinitions.Count-1);
            
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
          //  pauseButton.IsEnabled = false;
          //  pauseImage.Source = imageSourceOfString("/icons/pause-dark.png");
         //   playButton.IsEnabled = true;
          //  playImage.Source = imageSourceOfString("/icons/play.png");
        }

        /// <summary>
        /// Pauses training
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            dt.Stop();
          //  pauseButton.IsEnabled = false;
          //  pauseImage.Source = imageSourceOfString("/icons/pause-dark.png");
          //  playButton.IsEnabled = true;
          //  playImage.Source = imageSourceOfString("/icons/play.png");
        }

        /// <summary>
        /// Plays training after pause
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            dt.Start();
          //  pauseButton.IsEnabled = true;
          //  pauseImage.Source = imageSourceOfString("/icons/pause.png");
          //  playButton.IsEnabled = false;
          //  playImage.Source = imageSourceOfString("/icons/play-dark.png");
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
