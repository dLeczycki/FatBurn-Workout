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
using Workout.Pushups;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace Workout
{
    /// <summary>
    /// Interaction for MainWindow.xaml.cs class
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow mainWindow;

        //Public const parameters - pages
        public const int START_PAGE = 0;

        public const int SPARTAKUS_MAIN_PAGE = 11;
        public const int SPARTAKUS_EXPLANATION_PAGE = 12;
        public const int SPARTAKUS_SETTINGS_PAGE = 13;
        public const int SPARTAKUS_WORKOUT_PAGE = 14;

        public const int WEIDER_MAIN_PAGE = 21;
        public const int WEIDER_SETTINGS_PAGE = 23;
        public const int WEIDER_WORKOUT_PAGE = 24;

        public const int GYM_MAIN_PAGE = 31;
        public const int GYM_SETTINGS_PAGE = 33;
        public const int GYM_WORKOUT_PAGE = 34;

        public const int PUSHUPS_MAIN_PAGE = 41;
        public const int PUSHUPS_EXPLANATION_PAGE = 42;
        public const int PUSHUPS_SETTINGS_PAGE = 43;
        public const int PUSHUPS_WORKOUT_PAGE = 44;

        ///Microsoft Speech parameters
        public SpeechRecognitionEngine pSRE;
        public bool speechOn;
        public SpeechSynthesizer pTTS;
        public Thread speechThread;

        //Spartakus time parameters
        public int exTime;
        public int brTime;
        public int lngBrTime;

        //Weider parameters
        public int progress;

        //Gym parameters
        public List<string> muscles = new List<string>();
        
        //Pushups parameters
        public int trainingDay;
        public int testResult;

        //Public local parameters
        public int currentPageNumber;
        public Page currentPage;

        
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
            setWindow(SPARTAKUS_MAIN_PAGE);
        }

        private void buttonWeider_Click(object sender, RoutedEventArgs e)
        {
            setWindow(WEIDER_MAIN_PAGE);
        }

        private void buttonGym_Click(object sender, RoutedEventArgs e)
        {
            setWindow(GYM_MAIN_PAGE);
        }

        private void buttonPushups_Click(object sender, RoutedEventArgs e)
        {
            setWindow(PUSHUPS_MAIN_PAGE);
        }

        /// <summary>
        /// Displays selected page
        /// </summary>
        /// <param name="windowNumber">Page which will be displayed in window</param>
        public void setWindow(int windowNumber)
        {
            pTTS.SpeakAsyncCancelAll();
            currentPage = new StartPage();
            switch (windowNumber)
            {
                case START_PAGE:
                    currentPage = new StartPage();
                    break;
                case SPARTAKUS_MAIN_PAGE:
                    currentPage = new SpartakusMainPage(this);
                    pTTS.SpeakAsync("Wybrałeś Spartakusa");
                    pTTS.SpeakAsync("Twój trening składa się z dziesięciu ćwiczeń w trzech seriach");
                    pTTS.SpeakAsync("Każde z ćwiczeń będziesz wykonywać w określonym czasie");
                    pTTS.SpeakAsync("Pomiędzy kazdym ćwiczeniem następuje krótka przerwa - przerwa pomiędzy ćwiczeniami");
                    pTTS.SpeakAsync("Po wykonaniu dziesięciu ćwiczeń następuje długa przerwa - przerwa pomiędzy seriami");
                    pTTS.SpeakAsync("Przejdź dalej");
                    break;
                case SPARTAKUS_EXPLANATION_PAGE:
                    currentPage = new SpartakusExplanationPage(this);
                    break;
                case SPARTAKUS_SETTINGS_PAGE:
                    currentPage = new SpartakusSettingsPage(this);
                    break;
                case SPARTAKUS_WORKOUT_PAGE:
                    currentPage = new SpartakusWorkoutPage(this, exTime, brTime, lngBrTime);
                    break;

                case WEIDER_MAIN_PAGE:
                    currentPage = new WeiderMainPage(this);
                    break;
                case WEIDER_SETTINGS_PAGE:
                    currentPage = new WeiderSettingsPage(this);
                    break;
                case WEIDER_WORKOUT_PAGE:
                    currentPage = new WeiderWorkoutPage(this, exTime, brTime, lngBrTime, progress);
                    break;

                case GYM_MAIN_PAGE:
                    currentPage = new GymMainPage(this);
                    break;
                case GYM_SETTINGS_PAGE:
                    currentPage = new GymSettingsPage(this);
                    break;
                case GYM_WORKOUT_PAGE:
                    currentPage = new GymWorkoutPage(this, muscles);
                    break;

                case PUSHUPS_MAIN_PAGE:
                    currentPage = new PushupsMainPage(this);
                    break;
                case PUSHUPS_EXPLANATION_PAGE:
                    currentPage = new PushupsExplanationPage(this);
                    break;
                case PUSHUPS_SETTINGS_PAGE:
                    currentPage = new PushupsSettingsPage(this);
                    break;
                case PUSHUPS_WORKOUT_PAGE:
                    currentPage = new PushupsWorkoutPage(this, trainingDay, testResult);
                    break;
                default:
                    currentPage = new StartPage();
                    break;
            }
            currentPageNumber = windowNumber;
            mainFrame.Content = currentPage;
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

                //Build Grammars
                string[] mainCommands = new string[] { "Wyjdź", "Pomoc", "Wyłącz syntezator", "Ustawienia", "Sto pompek", "Szóstka Łejdera", "Spartakus", "Siłownia" };
                string[] navigationCommands = new string[] { "Dalej", "Wstecz", "Następna", "Wróć", "Strona główna" };
                string[] workoutCommands = new string[] { "Rozpocznij trening", "Przerwij trening", "Stop", "Pauza", "Wznów", "Start" };

                buildSimpleGrammar(mainCommands);
                buildSimpleGrammar(navigationCommands);
                buildSimpleGrammar(workoutCommands);

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
        /// Builds speech grammar which intercepts commands and later does not use its words in program.
        /// </summary>
        /// <param name="simpleCommandsArray">Array of simple indivisable string commands</param>
        private void buildSimpleGrammar(string[] simpleCommandsArray)
        {
            //Sets up choices
            Choices spartakusChoices = new Choices();

            //adds array to choices
            spartakusChoices.Add(simpleCommandsArray);

            //builds grammar
            GrammarBuilder buildGrammarSystem = new GrammarBuilder();
            buildGrammarSystem.Append(spartakusChoices);
            Grammar grammarSystem = new Grammar(buildGrammarSystem);

            //adds grammar to engine
            pSRE.LoadGrammarAsync(grammarSystem);
        }

        /// <summary>
        /// Method which works after speech is recognized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PSRE_SpeechRecognizedPL(object sender, SpeechRecognizedEventArgs e)
        {
            string txt = e.Result.Text;
            float confidence = e.Result.Confidence;
            if (confidence > 0.40 && speechOn)
            {
                //
                if (txt.IndexOf("Wyłącz syntezator") >= 0)
                {
                    pTTS.SpeakAsync("Wyłączyłeś syntezator");
                    speechOn = false;
                }
                else if (txt.IndexOf("Pomoc") >= 0)
                {
                    pTTS.SpeakAsync("Treść pomocy");
                }
                else if (txt.IndexOf("Ustawienia") >= 0)
                {

                }
                else if (txt.IndexOf("Sto pompek") >= 0)
                {
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        setWindow(PUSHUPS_MAIN_PAGE);
                    });
                }
                else if (txt.IndexOf("Szóstka Łejdera") >= 0)
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
                else if (txt.IndexOf("Strona główna") >= 0)
                {
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        setWindow(START_PAGE);
                    });
                }

                //Actions activated with voice on selected page
                switch (currentPageNumber)
                {
                    case SPARTAKUS_MAIN_PAGE:
                        if (txt.IndexOf("Dalej") >= 0 || txt.IndexOf("Następna") >= 0)
                        {
                            Application.Current.Dispatcher.Invoke((Action)delegate
                            {
                                setWindow(SPARTAKUS_EXPLANATION_PAGE);
                            });
                        }
                        break;
                    case SPARTAKUS_EXPLANATION_PAGE:
                        SpartakusExplanationPage spartakusExplanationPage = (SpartakusExplanationPage)currentPage;
                        if (txt.IndexOf("Dalej") >= 0 || txt.IndexOf("Następna") >= 0)
                        {
                            Application.Current.Dispatcher.Invoke((Action)delegate
                            {
                                setWindow(SPARTAKUS_SETTINGS_PAGE);
                            });
                        }
                        else if (txt.IndexOf("Wstecz") >= 0 || txt.IndexOf("Wróć") >= 0)
                        {
                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                ButtonAutomationPeer peer = new ButtonAutomationPeer(spartakusExplanationPage.backButton);
                                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                                invokeProv.Invoke();
                            }));
                        }
                        break;
                    case SPARTAKUS_SETTINGS_PAGE:
                        SpartakusSettingsPage spartakusSettingsPage = (SpartakusSettingsPage)currentPage;
                        if (txt.IndexOf("Rozpocznij trening") >= 0)
                        {
                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                ButtonAutomationPeer peer = new ButtonAutomationPeer(spartakusSettingsPage.nextButton);
                                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                                invokeProv.Invoke();
                            }));
                        }
                        else if (txt.IndexOf("Wstecz") >= 0 || txt.IndexOf("Wróć") >= 0)
                        {
                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                ButtonAutomationPeer peer = new ButtonAutomationPeer(spartakusSettingsPage.backButton);
                                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                                invokeProv.Invoke();
                            }));
                        }
                        break;
                    case SPARTAKUS_WORKOUT_PAGE:
                        SpartakusWorkoutPage spartakusWorkoutPage = (SpartakusWorkoutPage)currentPage;
                        if (txt.IndexOf("Przerwij trening") >= 0)
                        {
                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                ButtonAutomationPeer peer = new ButtonAutomationPeer(spartakusWorkoutPage.nextButton);
                                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                                invokeProv.Invoke();
                            }));
                        }
                        else if (txt.IndexOf("Pauza") >= 0 || txt.IndexOf("Stop") >= 0)
                        {
                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                ButtonAutomationPeer peer = new ButtonAutomationPeer(spartakusWorkoutPage.pauseButton);
                                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                                invokeProv.Invoke();
                            }));

                        }
                        else if (txt.IndexOf("Wznów") >= 0 || txt.IndexOf("Start") >= 0)
                        {
                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                ButtonAutomationPeer peer = new ButtonAutomationPeer(spartakusWorkoutPage.playButton);
                                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                                invokeProv.Invoke();
                            }));

                        }
                        else if (txt.IndexOf("Wstecz") >= 0 || txt.IndexOf("Wróć") >= 0)
                        {
                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                try
                                {
                                    ButtonAutomationPeer peer = new ButtonAutomationPeer(spartakusWorkoutPage.backButton);
                                    IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                                    invokeProv.Invoke();
                                }
                                catch (Exception ex)
                                {
                                    Application.Current.Dispatcher.Invoke(new Action(() =>
                                    {
                                        ButtonAutomationPeer peer = new ButtonAutomationPeer(spartakusWorkoutPage.pauseButton);
                                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                                        invokeProv.Invoke();
                                    }));
                                    pTTS.SpeakAsyncCancelAll();
                                    pTTS.SpeakAsync("Polecenie jest nieprawidłowe");
                                    Message_WrongWeiderParameters();
                                }
                            }));
                        }
                        break;
                }

            }
            else
            {
                pTTS.SpeakAsync("Proszę powtórzyć");
                return;
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

        public static void Message_OperationUnavailable()
        {
            string text = "Operacja jest niemożliwa do zrealizowania";
            string caption = "Błąd polecenia";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBox.Show(text, caption, button, icon);
        }

        
    }
}
