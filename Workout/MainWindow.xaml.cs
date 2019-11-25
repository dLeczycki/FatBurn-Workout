using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.Synthesis;
using System.Globalization;
using System.Text;
using System.Threading;
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
using System.ComponentModel;
using Workout.Spartakus;
using Workout.Weider;
using Workout.Gym;

namespace Workout
{
    /// <summary>
    /// Interaction for MainWindow.xaml.cs class
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow mainWindow;

        //Public const parameters
        public const int START_PAGE = 0;

        public const int SPARTAKUS_MAIN_PAGE = 11;
        public const int SPARTAKUS_EXPLANATION_PAGE = 12;
        public const int SPARTAKUS_SETTINGS_PAGE = 13;
        public const int SPARTAKUS_WORKOUT_PAGE = 14;

        public const int WEIDER_MAIN_PAGE = 21;
        public const int WEIDER_SETTINGS_PAGE = 23;
        public const int WEIDER_WORKOUT_PAGE = 24;

        public const int GYM_MAIN_PAGE = 31;
        public const int GYM_EXPLANATION_PAGE = 32;
        public const int GYM_SETTINGS_PAGE = 33;
        public const int GYM_WORKOUT_PAGE = 34;

        ///Microsoft Speech parameters
        public SpeechRecognitionEngine pSRE;
        public bool speechOn;
        public SpeechSynthesizer pTTS;
        public Thread speechThread;

        //Spartakus time parameters
        public int exTime;
        public int brTime;
        public int lngBrTime;
        public int progress;

        public MainWindow()
        {
            InitializeComponent();
            setStartupWindowParameters();
            mainWindow = this;
            speechThread = new Thread(new ThreadStart(setUpSpeech));
            speechThread.SetApartmentState(ApartmentState.STA);
            speechThread.Start();
        }


        //-------------------------------------------------------------------------------//
        //---------------------------------WINDOW METHODS--------------------------------//
        //-------------------------------------------------------------------------------//
        /// <summary>
        /// Initializes Startup Window and its parameters and outlook.
        /// </summary>
        private void setStartupWindowParameters()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            StartPage startPage = new StartPage();
            mainFrame.Content = startPage;
        }

        /// <summary>
        /// Closes application and stops all threads on window close.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Opens Spartakus layout page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSpartakus_Click(object sender, RoutedEventArgs e)
        {
            SpartakusMainPage spartakus = new SpartakusMainPage(this);
            mainFrame.Content = spartakus;
        }

        private void buttonWeider_Click(object sender, RoutedEventArgs e)
        {
            WeiderMainPage weider = new WeiderMainPage(this);
            mainFrame.Content = weider;
        }

        private void buttonGym_Click(object sender, RoutedEventArgs e)
        {
            GymMainPage silownia = new GymMainPage(this);
            mainFrame.Content = silownia;
        }

        /// <summary>
        /// Displays selected page
        /// </summary>
        /// <param name="windowNumber">Page number: 1 - main, 2 - explanation, 3 - settings, 4 - workout</param>
        public void setWindow(int windowNumber)
        {
            Page page = new StartPage();
            switch (windowNumber)
            {
                case START_PAGE:
                    page = new StartPage();
                    break;
                case SPARTAKUS_MAIN_PAGE:
                    page = new SpartakusMainPage(this);
                    break;
                case SPARTAKUS_EXPLANATION_PAGE:
                    page = new SpartakusExplanationPage(this);
                    break;
                case SPARTAKUS_SETTINGS_PAGE:
                    page = new SpartakusSettingsPage(this);
                    break;
                case SPARTAKUS_WORKOUT_PAGE:
                    page = new SpartakusWorkoutPage(this, exTime, brTime, lngBrTime);
                    break;

                case WEIDER_MAIN_PAGE:
                    page = new WeiderMainPage(this);
                    break;
                case WEIDER_SETTINGS_PAGE:
                    page = new WeiderSettingsPage(this);
                    break;
                case WEIDER_WORKOUT_PAGE:
                    page = new WeiderWorkoutPage(this, exTime, brTime, lngBrTime, progress);
                    break;

                case GYM_MAIN_PAGE:
                    page = new GymMainPage(this);
                    break;
                case GYM_EXPLANATION_PAGE:
                    page = new GymExplanationPage(this);
                    break;
                case GYM_SETTINGS_PAGE:
                    page = new GymSettingsPage(this);
                    break;
                case GYM_WORKOUT_PAGE:
                    page = new GymWorkoutPage(this, exTime, brTime, lngBrTime);
                    break;

                default:
                    page = new StartPage();
                    break;
            }
            mainFrame.Content = page;
        }

        //-------------------------------------------------------------------------------//
        //---------------------------------SPEECH METHODS--------------------------------//
        //-------------------------------------------------------------------------------//
        /// <summary>
        /// Method which sets basic Speech Parameters
        /// </summary>
        private void setUpSpeech()
        {
            speechOn = true;
            pTTS = new SpeechSynthesizer();
            try
            {
                pTTS.SetOutputToDefaultAudioDevice();
                CultureInfo ci = new CultureInfo("pl-PL");
                pSRE = new SpeechRecognitionEngine(ci);
                pSRE.SetInputToDefaultAudioDevice();
                pSRE.SpeechRecognized += PSRE_SpeechRecognizedPL;

                pTTS.SpeakAsync("Witaj w swoim doradcy treningu");
                // -------------------------------------------------------------------------
                // Budowa gramatyki numer 1 - POLECENIA SYSTEMOWE
                // Budowa gramatyki numer 1 - określenie komend:
                Choices mainChoices = new Choices();
                string[] mainCommands = new string[] { "Wyjdź", "Pomoc", "Wyłącz syntezator", "Ustawienia", "Sto pompek", "Szóstka Łejdera", "Spartakus", "Siłownia" };
                mainChoices.Add(mainCommands);

                // Budowa gramatyki numer 1 - definiowanie składni gramatyki:
                GrammarBuilder buildGrammarSystem = new GrammarBuilder();
                buildGrammarSystem.Append(mainChoices);

                // Budowa gramatyki numer 1 - utworzenie gramatyki:
                Grammar grammarSystem = new Grammar(buildGrammarSystem); 

                // -------------------------------------------------------------------------      
                // Budowa gramatyki numer 2 - POLECENIA DLA PROGRAMU   
                // Budowa gramatyki numer 2 - określenie komend:
                Choices chNumbers = new Choices(); //możliwy wybór słów
                Choices chOperations = new Choices();

                string[] numbers = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                string[] operations = new string[] { "plus", "minus", "razy", "przez" };

                chNumbers.Add(numbers);
                chOperations.Add(operations);
                // Budowa gramatyki numer 2 - definiowanie składni gramatyki dodawania:
                GrammarBuilder grammarProgramOperations = new GrammarBuilder();
                grammarProgramOperations.Append("Oblicz");
                grammarProgramOperations.Append(chNumbers);
                grammarProgramOperations.Append(chOperations);
                grammarProgramOperations.Append(chNumbers);

                // Budowa gramatyki numer 2 - utworzenie gramatyki:
                Grammar g_WhatIsXoperacjaY = new Grammar(grammarProgramOperations); //gramatyka

                //Załadowanie gramatyk
                pSRE.LoadGrammarAsync(g_WhatIsXoperacjaY);
                pSRE.LoadGrammarAsync(grammarSystem);

                // Ustaw rozpoznawanie przy wykorzystaniu wielu gramatyk:
                pSRE.RecognizeAsync(RecognizeMode.Multiple);

                while (speechOn == true) {; } 
            }
            catch (Exception e)
            {
                Message_SpeechServiceFailed();
            }
        }

        /// <summary>
        /// Method which works after speech is recognized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PSRE_SpeechRecognizedPL(object sender, SpeechRecognizedEventArgs e)
        {
            string[] mainCommands = new string[] { "Wyjdź", "Pomoc", "Wyłącz syntezator", "Ustawienia", "Sto pompek", "Szóstka Łejdera", "Spartakus", "Siłownia" };
            string txt = e.Result.Text;
            string comments;
            float confidence = e.Result.Confidence;
            if (confidence > 0.40)
            {
                if (txt.IndexOf("Wyłącz syntezator") >= 0)
                {
                    speechOn = false;
                }
                else if (txt.IndexOf("Pomoc") >= 0)
                {
                    pTTS.SpeakAsync("Pomoc");
                }
                else if(txt.IndexOf("Wyłącz syntezator") >= 0)
                {

                }
                else if (txt.IndexOf("Ustawienia") >= 0)
                {

                }
                else if(txt.IndexOf("Sto pompek") >= 0)
                {

                }
                else if(txt.IndexOf("Szóstka Łejdera") >= 0)
                {
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        setWindow(WEIDER_MAIN_PAGE);
                    });

                }
                else if (txt.IndexOf("Spartakus") >= 0)
                {
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        setWindow(SPARTAKUS_MAIN_PAGE);
                    });
                        
                }
                else if (txt.IndexOf("Siłownia") >= 0)
                {
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        setWindow(GYM_MAIN_PAGE);
                    });

                }
                else if ((txt.IndexOf("Oblicz") >= 0) && (txt.IndexOf("plus") >= 0) &&
               (speechOn == true))
                {
                    string[] words = txt.Split(' ');
                    int liczba1 = int.Parse(words[1]);
                    int liczba2 = int.Parse(words[3]);
                    int suma = liczba1 + liczba2;
                    comments = String.Format("\tOBLICZONO: {0} + {1} = {2}",
                    liczba1, liczba2, suma);
                    Console.WriteLine(comments);
                    pTTS.SpeakAsync("Wynik działania to: " + suma);
                }
            }
            else
            {
                comments = String.Format("\tNISKI WSPÓŁCZYNNIK WIARYGODNOŚCI - powtórz polecenie");

                Console.WriteLine(comments);
                pTTS.SpeakAsync("Proszę powtórzyć");
            }
        }


        //-------------------------------------------------------------------------------//
        //--------------------------------MESSAGE BOXES----------------------------------//
        //-------------------------------------------------------------------------------//
        /// <summary>
        /// MessageBox displayed when speech service did not start.
        /// </summary>
        private static void Message_SpeechServiceFailed()
        {
            string text = "Nie udało się uruchomić usługi głosowej";
            string caption = "Błąd usługi głosowej";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBox.Show(text, caption, button, icon);
        }

        /// <summary>
        /// MessageBox displayed when Spartakus time paramaeters are wrong
        /// </summary>
        public static void Message_WrongSpartakusParameters()
        {
            string text = "Podano złe wartości czasowe. Musisz wprowadzić liczby.";
            string caption = "Złe dane";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBox.Show(text, caption, button, icon);
        }

        public static void Message_WrongWeiderParameters()
        {
            string text = "Podano złe wartości. Należy wprowadzić liczby całkowite.";
            string caption = "Złe dane";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBox.Show(text, caption, button, icon);
        }
    }
}
