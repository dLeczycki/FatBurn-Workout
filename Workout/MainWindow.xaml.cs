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
        public const int GYM_EXPLANATION_PAGE = 32;
        public const int GYM_SETTINGS_PAGE = 33;
        public const int GYM_WORKOUT_PAGE = 34;

        public const int PUSHUPS_MAIN_PAGE = 41;
        public const int PUSHUPS_EXPLANATION_PAGE = 42;
        public const int PUSHUPS_SETTINGS_PAGE = 43;
        public const int PUSHUPS_WORKOUT_PAGE = 44;

        
        ///Microsoft Speech parameters
        public SpeechRecognitionEngine pSRE;
        public bool speechWait;
        public bool speechOn;
        public SpeechSynthesizer pTTS;
        public Thread speechThread;

        //Spartakus time parameters
        public int exTime;
        public int brTime;
        public int lngBrTime;

        //Weider parameters
        public int progress;

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
                case GYM_EXPLANATION_PAGE:
                    currentPage = new GymExplanationPage(this);
                    break;
                case GYM_SETTINGS_PAGE:
                    currentPage = new GymSettingsPage(this);
                    break;
                case GYM_WORKOUT_PAGE:
                    currentPage = new GymWorkoutPage(this, exTime, brTime, lngBrTime);
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
            speechWait = true;
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
                string[] mainCommands = new string[] { "Wyjdź", "Pomoc", "Wyłącz syntezator", "Ustawienia", "Sto pompek", "Pompki", "Szóstka Łejdera", "Spartakus", "Siłownia" };
                string[] navigationCommands = new string[] { "Dalej", "Wstecz", "Następna", "Wróć", "Strona główna" };
                string[] workoutCommands = new string[] { "Rozpocznij trening", "Przerwij trening", "Stop", "Pauza", "Wznów", "Start", "Zrobione", "Zrobiłem", "Zrobiłam" };

                buildGrammar(mainCommands);
                buildGrammar(navigationCommands);
                buildGrammar(workoutCommands);

                string[] firstSpartakusSetting = new string[] { "Ustaw pierwszy czas na ", "Ustaw czas na jedno ćwiczenie na ", "Ustaw czas na ćwiczenie na", "ćwiczenie", };
                string[] secondSpartakusSetting = new string[] { "Ustaw drugi czas na ", "Ustaw czas na krótką przerwę na", "krótka przerwa" };
                string[] thirdSpartakusSetting = new string[] { "Ustaw trzeci czas na ", "Ustaw czas na długą przerwę na", "długa przerwa" };
                string[] spartakusSetting = new string[firstSpartakusSetting.Length + secondSpartakusSetting.Length + thirdSpartakusSetting.Length];
                firstSpartakusSetting.CopyTo(spartakusSetting, 0);
                secondSpartakusSetting.CopyTo(spartakusSetting, firstSpartakusSetting.Length);
                thirdSpartakusSetting.CopyTo(spartakusSetting, firstSpartakusSetting.Length + secondSpartakusSetting.Length);

                string[] numbers = new string[] { "jeden", "dwa", "dwie", "trzy", "cztery", "pięć", "sześć", "siedem", "osiem", "dziewięć", "dziesięć" };
                string[] teens = new string[] { "jedenaście", "dwanaście", "trzynaście", "czternaście", "piętnaście", "szesnaście", "siedemnaście", "osiemnaście", "dziewiętnaście" };
                string[] dozens = new string[] { "dwadzieścia", "trzydzieści", "czterdzieści", "pięćdziesiąt", "sześćdziesiąt", "siedemdziesiąt", "osiemdziesiąt", "dziewięćdziesiąt" };
                string[] hundreds = new string[] { "sto", "dwieście", "trzysta", "czterysta", "pięćset" };

                buildGrammar(spartakusSetting, numbers, new string[] { "sekund" });
                buildGrammar(spartakusSetting, teens, new string[] { "sekund" });
                buildGrammar(spartakusSetting, dozens, new string[] { "sekund" });
                buildGrammar(spartakusSetting, hundreds, new string[] { "sekund" });

                buildGrammar(spartakusSetting, hundreds, numbers, new string[] { "sekund" });
                buildGrammar(spartakusSetting, hundreds, teens, new string[] { "sekund" });
                buildGrammar(spartakusSetting, hundreds, dozens, new string[] { "sekund" });

                buildGrammar(spartakusSetting, hundreds, dozens, numbers, new string[] { "sekund" });

                // Ustaw rozpoznawanie przy wykorzystaniu wielu gramatyk:
                pSRE.RecognizeAsync(RecognizeMode.Multiple);

                while (speechWait == true) {; }
            }
            catch (Exception e)
            {
                Message_SpeechServiceFailed();
            }
        }

        /// <summary>
        /// Builds grammar which command consists of words given in string arrays
        /// </summary>
        /// <param name="commandParts"></param>
        private void buildGrammar(params string[][] commandParts)
        {
            List<Choices> chList = new List<Choices>();
            Choices newChoice;
            for (int i = 0; i < commandParts.Length; i++)
            {
                newChoice = new Choices();
                newChoice.Add(commandParts[i]);
                chList.Add(newChoice);
            }
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            foreach (Choices choice in chList)
            {
                grammarBuilder.Append(choice);
            }
            Grammar grammarSystem = new Grammar(grammarBuilder);

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
            if (confidence > 0.40 && speechWait)
            {
                if (txt.IndexOf("Wyłącz syntezator") >= 0)
                {
                    pTTS.SpeakAsyncCancelAll();
                    pTTS.SpeakAsync("Wyłączyłeś syntezator");
                    speechWait = false;
                }
                else if (txt.IndexOf("Pomoc") >= 0)
                {
                    pTTS.SpeakAsync("Treść pomocy");
                }
                else if (txt.IndexOf("Ustawienia") >= 0)
                {

                }
                else if (txt.IndexOf("Sto pompek") >= 0 || txt.IndexOf("Pompki") >= 0)
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
                            try
                            {
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    ButtonAutomationPeer peer = new ButtonAutomationPeer(spartakusWorkoutPage.backButton);
                                    IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                                    invokeProv.Invoke();
                                }));
                            }
                            catch (Exception ex)
                            {
                                pTTS.SpeakAsyncCancelAll();
                                pTTS.SpeakAsync("Polecenie jest nieprawidłowe");
                                Message_WrongWeiderParameters();
                            }
                        }
                        break;
                    case PUSHUPS_MAIN_PAGE:
                        if (txt.IndexOf("Dalej") >= 0 || txt.IndexOf("Następna") >= 0)
                        {
                            Application.Current.Dispatcher.Invoke((Action)delegate
                            {
                                setWindow(PUSHUPS_EXPLANATION_PAGE);
                            });
                        }
                        break;
                    case PUSHUPS_EXPLANATION_PAGE:
                        PushupsExplanationPage pushupsExplanationPage = (PushupsExplanationPage)currentPage;
                        if (txt.IndexOf("Dalej") >= 0 || txt.IndexOf("Następna") >= 0)
                        {
                            Application.Current.Dispatcher.Invoke((Action)delegate
                            {
                                setWindow(PUSHUPS_SETTINGS_PAGE);
                            });
                        }
                        else if (txt.IndexOf("Wstecz") >= 0 || txt.IndexOf("Wróć") >= 0)
                        {
                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                ButtonAutomationPeer peer = new ButtonAutomationPeer(pushupsExplanationPage.backButton);
                                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                                invokeProv.Invoke();
                            }));
                        }
                        break;
                    case PUSHUPS_SETTINGS_PAGE:
                        PushupsSettingsPage pushupsSettingsPage = (PushupsSettingsPage)currentPage;
                        if (txt.IndexOf("Rozpocznij trening") >= 0)
                        {
                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                ButtonAutomationPeer peer = new ButtonAutomationPeer(pushupsSettingsPage.nextButton);
                                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                                invokeProv.Invoke();
                            }));
                        }
                        else if (txt.IndexOf("Wstecz") >= 0 || txt.IndexOf("Wróć") >= 0)
                        {
                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                ButtonAutomationPeer peer = new ButtonAutomationPeer(pushupsSettingsPage.backButton);
                                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                                invokeProv.Invoke();
                            }));
                        }
                        break;
                    case PUSHUPS_WORKOUT_PAGE:
                        PushupsWorkoutPage pushupsWorkoutPage = (PushupsWorkoutPage)currentPage;
                        if (txt.IndexOf("Zrobione") >= 0 || txt.IndexOf("Zrobiłem") >= 0 || txt.IndexOf("Zrobiłam") >= 0 || txt.IndexOf("Dalej") >= 0)
                        {
                            try
                            {
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    ButtonAutomationPeer peer = new ButtonAutomationPeer(pushupsWorkoutPage.nextSeriesButton);
                                    IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                                    invokeProv.Invoke();
                                }));
                            }
                            catch (Exception ex)
                            {
                                pTTS.SpeakAsyncCancelAll();
                                pTTS.SpeakAsync("Wykonano wszystkie serie");
                            }
                        }
                        else if (txt.IndexOf("Wstecz") >= 0 || txt.IndexOf("Wróć") >= 0)
                        {
                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                ButtonAutomationPeer peer = new ButtonAutomationPeer(pushupsWorkoutPage.backButton);
                                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                                invokeProv.Invoke();
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

        private bool checkFirstSpartakusSetting(string txt)
        {
            return false;
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
