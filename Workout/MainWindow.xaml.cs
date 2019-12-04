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

        //Public const command arrays
        //Arrays for global commands
        public readonly string[] MAIN_COMMANDS = new string[] { "Wyjdź", "Pomoc", "Wyłącz syntezator", "Włącz syntezator", "Wyłącz mikrofon", "Sto pompek", "Pompki", "Szóstka Łejdera", "Spartakus", "Siłownia" };
        public readonly string[] NAVIGATION_COMMANDS = new string[] { "Dalej", "Wstecz", "Następna", "Wróć", "Strona główna" };
        public readonly string[] WORKOUT_COMMANDS = new string[] { "Rozpocznij trening", "Przerwij trening", "Stop", "Pauza", "Wznów", "Start", "Zrobione", "Zrobiłem", "Zrobiłam" };

        //Arrays for Spartakus commands
        public readonly string[] SPARTAKUS_FIRST_SETTING = new string[] { "Ustaw pierwszy czas na ", "Ustaw czas na jedno ćwiczenie na ", "Ustaw czas na ćwiczenie na", "ćwiczenie", };
        public readonly string[] SPARTAKUS_SECOND_SETTING = new string[] { "Ustaw drugi czas na ", "Ustaw czas na krótką przerwę na", "krótka przerwa" };
        public readonly string[] SPARTAKUS_THIRD_SETTING = new string[] { "Ustaw trzeci czas na ", "Ustaw czas na długą przerwę na", "długa przerwa" };

        //Arrays for Pushups commands
        public readonly string[] PUSHUPS_TEST_SETTING = new string[] { "Ustaw wynik testu na", "Wynik testu" };
        public readonly string[] PUSHUPS_TRAINING_DAY_SETTING = new string[] { "Ustaw dzień treningu na", "dzień treningu" };
        public readonly string[] PUSHUPS = new string[] { "pompki", "pompek" };
        public readonly string[] DAYS = new string[] { "dzień" };

        //Arrays for number commands
        public readonly string[] NUMBERS = new string[] { "jeden", "dwa", "trzy", "cztery", "pięć", "sześć", "siedem", "osiem", "dziewięć", "dziesięć", "dwie" };
        public readonly string[] NUMBERS_ORDERED = new string[] { "pierwszy", "drugi", "trzeci", "czwarty", "piąty", "szósty", "siódmy", "ósmy", "dziewiąty", "dziesiąty" };
        public readonly string[] TEENS = new string[] { "jedenaście", "dwanaście", "trzynaście", "czternaście", "piętnaście", "szesnaście", "siedemnaście", "osiemnaście", "dziewiętnaście" };
        public readonly string[] TEENS_ORDERED = new string[] { "jedenasty", "dwunasty", "trzynasty", "czternasty", "piętnasty", "szesnasty", "siedemnasty", "osiemnasty", "dziewiętnasty" };
        public readonly string[] DOZENS = new string[] { "dwadzieścia", "trzydzieści", "czterdzieści", "pięćdziesiąt", "sześćdziesiąt", "siedemdziesiąt", "osiemdziesiąt", "dziewięćdziesiąt" };
        public readonly string[] DOZENS_ORDERED = new string[] { "dwudziesty", "trzydziesty", "czterdziesty", "pięćdziesiąty", "sześćdziesiąty", "siedemdziesiąty", "osiemdziesiąty", "dziewięćdziesiąty" };
        public readonly string[] HUNDREDS = new string[] { "sto", "dwieście", "trzysta", "czterysta", "pięćset", "sześćset", "siedemset", "osiemset", "dziewięćset" };
        public readonly string[] SECONDS = new string[] { "sekund", "sekundy" };

        //Arrays for Gym commands
        public readonly string[] GYM_FIRST_SETTING = new string[] { "Wybierz ", "Ćwicz ", "Ustaw ", "Odznacz ", "Usuń "};
        public readonly string[] GYM_MUSCLE_PART = new string[] { "plecy", "ramiona", "ręce", "klatę", "brzuch", "nogi" };

        //Arrays for Weider commands
        public readonly string[] WEIDER_FIRST_SETTING = new string[] { "Ustaw pierwszy czas na ", "Ustaw czas na jedno ćwiczenie na ", "Ustaw czas na ćwiczenie na", "ćwiczenie", };
        public readonly string[] WEIDER_SECOND_SETTING = new string[] { "Ustaw drugi czas na ", "Ustaw czas na krótką przerwę na", "krótka przerwa" };
        public readonly string[] WEIDER_THIRD_SETTING = new string[] { "Ustaw trzeci czas na ", "Ustaw czas na długą przerwę na", "długa przerwa" };
        public readonly string[] WEIDER_DAY_SETTING = new string[] { "Ustaw dzień na ", "dzień treningu ", "Ustaw dzień treningu na ", "Ustaw etap na "};

        ///Microsoft Speech parameters
        public SpeechRecognitionEngine pSRE;
        public bool micOn;
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

        //Gym parameters
        public List<string> muscles = new List<string>();

        //Public local parameters
        public int currentPageNumber;
        public Page currentPage;



        //-------------------------------------------------------------------------------//
        //---------------------------------WINDOW METHODS--------------------------------//
        //-------------------------------------------------------------------------------//
        public MainWindow()
        {
            InitializeComponent();
            setStartupWindowParameters();
            mainWindow = this;
            speechThread = new Thread(new ThreadStart(setUpSpeech));
            speechThread.SetApartmentState(ApartmentState.STA);
            speechThread.Start();
        }

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

        private void buttonHelp_Click(object sender, RoutedEventArgs e)
        {
            string[] nextFlowArray = new string[] { "Dalej/Następna - przejdź do następnej strony" };
            string[] nextBackFlowArray = new string[] { "Dalej/Następna - przejdź do następnej strony", "Wstecz/Wróć - powrót do poprzedniej strony" };
            switch (currentPageNumber)
            {
                case START_PAGE:
                    string[] startPageOperations = new string[] {
                        "100 pompek/pompki - wyświetla trening pompek",
                        "6 Weidera - wyświetla trening 6 Weidera",
                        "Spartakus - wyświetla trening spartakusa",
                        "Siłownia - wyświetla trening na siłowni",
                        "Wyłącz/włącz mikrofon",
                        "Wyłącz/Włącz syntezator"
                    };
                    Message_OperationsAllowed(startPageOperations);
                    break;

                case SPARTAKUS_MAIN_PAGE:
                    Message_OperationsAllowed(nextFlowArray);
                    break;
                case SPARTAKUS_EXPLANATION_PAGE:
                    Message_OperationsAllowed(nextBackFlowArray);
                    break;
                case SPARTAKUS_SETTINGS_PAGE:
                    string[] spartakusSettingOperations = new string[] {
                        "Rozpocznij trening - rozpoczyna trening o zadanych parametrach", "Wstecz/Wróć - powrót do poprzedniej strony",
                        "Ustaw pierwszy czas na / Ustaw czas na jedno ćwiczenie na / Ustaw czas na ćwiczenie na / ćwiczenie XX sekund - ustawia pierwszy czas na wybraną liczbę sekund",
                        "Ustaw drugi czas na / Ustaw czas na krótką przerwę na / krótka przerwa XX sekund - ustawia drugi czas na wybraną liczbę sekund",
                        "Ustaw trzeci czas na / Ustaw czas na długą przerwę na / długa przerwa XX sekund - ustawia trzeci czas na wybraną liczbę sekund"
                    };
                    Message_OperationsAllowed(spartakusSettingOperations);
                    break;
                case SPARTAKUS_WORKOUT_PAGE:
                    string[] spartakusWorkoutOperations = new string[] {
                        "Przerwij trening - przerywa trening, trening jest łądowany od początku",
                        "Stop / Pauza - Zatrzymuje licznik",
                        "Wznów / Start - Wznawia odliczanie licznika"};
                    Message_OperationsAllowed(spartakusWorkoutOperations);
                    break;

                case PUSHUPS_MAIN_PAGE:
                    Message_OperationsAllowed(nextFlowArray);
                    break;
                case PUSHUPS_EXPLANATION_PAGE:
                    Message_OperationsAllowed(nextBackFlowArray);
                    break;
                case PUSHUPS_SETTINGS_PAGE:
                    string[] pushupsSettingOperations = new string[] {
                        "Wstecz/Wróć - powrót do poprzedniej strony",
                        "Rozpocznij trening - rozpoczyna trening o zadanych parametrach",
                        "Wstecz/Wróć - powrót do poprzedniej strony",
                        "Ustaw wynik testu na / Wynik testu XXX pompki/pompek",
                        "Ustaw dzień treningu na / dzień treningu XX dzień"
                    };
                    Message_OperationsAllowed(pushupsSettingOperations);
                    break;
                case PUSHUPS_WORKOUT_PAGE:
                    string[] pushupsWorkoutOperations = new string[] {
                        "Wstecz/Wróć - powrót do poprzedniej strony",
                        "Zrobione / Zrobiłem / Zrobiłam - ustawia kolejną serię pompek"
                    };
                    Message_OperationsAllowed(pushupsWorkoutOperations);
                    break;

                case GYM_MAIN_PAGE:
                    Message_OperationsAllowed(nextFlowArray);
                    break;
                case GYM_SETTINGS_PAGE:
                    string[] gymSettingOperations = new string[]
                    {
                        "Wstecz/Wróć - powrót do poprzedniej strony",
                        "Wybierz partie mięśniowe, które chcesz zaangażować podczas treningu",
                        "Rozpocznij trening - rozpoczyna trening wybranych partii mięśniowych"
                    };
                    Message_OperationsAllowed(gymSettingOperations);
                    break;
                case GYM_WORKOUT_PAGE:
                    string[] gymWorkoutOperations = new string[]
                    {
                        "Wstecz/Wróć - powrót do poprzedniej strony",
                        "Zrobione / Zrobiłem / Zrobiłam - przechodzi do kolejnej serii ćwiczeń lub po jej wykonaniu, do kolejnej partii mięśniowej"
                    };
                    Message_OperationsAllowed(gymWorkoutOperations);
                    break;

                case WEIDER_MAIN_PAGE:
                    Message_OperationsAllowed(nextFlowArray);
                    break;
                case WEIDER_SETTINGS_PAGE:
                    string[] weiderSettingOperations = new string[]
                    {
                        "Wstecz/Wróć - powrót do poprzedniej strony",
                        "Ustaw pierwszy czas na / Ustaw czas na jedno ćwiczenie na / Ustaw czas na ćwiczenie na / ćwiczenie XX sekund - ustawia pierwszy czas na wybraną liczbę sekund",
                        "Ustaw drugi czas na / Ustaw czas na krótką przerwę na / krótka przerwa XX sekund - ustawia drugi czas na wybraną liczbę sekund",
                        "Ustaw trzeci czas na / Ustaw czas na długą przerwę na / długa przerwa XX sekund - ustawia trzeci czas na wybraną liczbę sekund",
                        "Ustaw dzień treningu na / dzień treningu XX dzień",
                        "Rozpocznij trening - rozpoczyna trening"
                    };
                    Message_OperationsAllowed(weiderSettingOperations);
                    break;
                case WEIDER_WORKOUT_PAGE:
                    string[] weiderWorkoutOperations = new string[]
                    {
                        "Przerwij trening - przerywa trening, trening jest łądowany od początku",
                        "Stop / Pauza - Zatrzymuje licznik czasu",
                        "Wznów / Start - Wznawia odliczanie licznika"
                    };
                    Message_OperationsAllowed(weiderWorkoutOperations);
                    break;
            }
        }

        private void buttonMic_Click(object sender, RoutedEventArgs e)
        {
            if (micOn) turnOffMic();
            else turnOnMic();
        }

        private void buttonSpeaker_Click(object sender, RoutedEventArgs e)
        {
            if (speechOn) turnOffSpeaker();
            else turnOnSpeaker();
        }

        /// <summary>
        /// Displays selected page
        /// </summary>
        /// <param name="windowNumber">Page which will be displayed in window</param>
        public void setWindow(int windowNumber)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
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
                        speechText("Wybrałeś Spartakusa");
                        speechText("Twój trening składa się z dziesięciu ćwiczeń w trzech seriach");
                        speechText("Każde z ćwiczeń będziesz wykonywać w określonym czasie");
                        speechText("Pomiędzy kazdym ćwiczeniem następuje krótka przerwa - przerwa pomiędzy ćwiczeniami");
                        speechText("Po wykonaniu dziesięciu ćwiczeń następuje długa przerwa - przerwa pomiędzy seriami");
                        speechText("Przejdź dalej");
                        break;
                    case SPARTAKUS_EXPLANATION_PAGE:
                        currentPage = new SpartakusExplanationPage(this);
                        speechText("Podczas ćwiczenia wyświetli się podobny ekran");
                        speechText("Po lewej stronie znajduje się licznik, który pokazuje ile czasu ćwiczenia lub przerwy Ci zostało");
                        speechText("Na dole znajdują się przyciski do sterowania licznikiem");
                        speechText("Po prawej stronie znajdują się instrukcje do ćwiczenia. Od góry nazwa ćwiczenia, zdjęcia jak je wykonywać oraz etap treningu");
                        speechText("Przejdź dalej");
                        break;
                    case SPARTAKUS_SETTINGS_PAGE:
                        currentPage = new SpartakusSettingsPage(this);
                        speechText("Ustaw parametry treningu. Po ich ustawieniu rozpocznij trening");
                        break;
                    case SPARTAKUS_WORKOUT_PAGE:
                        currentPage = new SpartakusWorkoutPage(this, exTime, brTime, lngBrTime);
                        break;

                    case WEIDER_MAIN_PAGE:
                        currentPage = new WeiderMainPage(this);
                        speechText("Wybrałeś trening szóstka łejdera");
                        speechText("Twój trening składa się z sześciu ćwiczeń");
                        speechText("Liczba serii zależy od dotychczasowego etapu wykonania treningu");
                        speechText("Pomiędzy każdym ćwiczeniem następuje krótka przerwa - przerwa pomiędzy ćwiczeniami, natomiast po wykonaniu sześciu ćwiczeń następuje długa przerwa - przerwa pomiędzy seriami");
                        speechText("Przejdź dalej");
                        break;
                    case WEIDER_SETTINGS_PAGE:
                        currentPage = new WeiderSettingsPage(this);
                        speechText("Ustaw parametry treningu. Po ich ustawieniu rozpocznij trening");
                        break;
                    case WEIDER_WORKOUT_PAGE:
                        currentPage = new WeiderWorkoutPage(this, exTime, brTime, lngBrTime, progress);
                        break;

                    case GYM_MAIN_PAGE:
                        currentPage = new GymMainPage(this);
                        speechText("Wybrałeś trening na siłowni");
                        speechText("Twój trening jest uzależniony od wybranych pratii mięśniowych");
                        speechText("Pamiętaj, że pomiędzy dniami treningu należy zrobić jeden dzień przerwy");
                        speechText("Przejdź dalej");
                        break;
                    case GYM_SETTINGS_PAGE:
                        currentPage = new GymSettingsPage(this);
                        speechText("Wybierz partie mięśniowe, które ccesz zaangażować podczas treningu. Po ich ustawieniu rozpocznij trening");
                        break;
                    case GYM_WORKOUT_PAGE:
                        currentPage = new GymWorkoutPage(this, muscles);
                        break;

                    case PUSHUPS_MAIN_PAGE:
                        currentPage = new PushupsMainPage(this);
                        speechText("Wybrałeś trening 100 pompek");
                        speechText("Na Twój trening składa się kilka serii wyznaczonej liczby pompek");
                        speechText("Najpierw zrób test, który pozwoli dostosować do Ciebie właściwy cykl treningowy");
                        speechText("Wykonujesz trening zgodnie z zaleceniami danego cyklu");
                        speechText("Pamiętaj! Pomiędzy dniami treningu rób jeden dzień przerwy, po trzech dniach conajmniej dwa dni. Mięśni nie możesz przemęczać");
                        speechText("Przejdź dalej");
                        break;
                    case PUSHUPS_EXPLANATION_PAGE:
                        currentPage = new PushupsExplanationPage(this);
                        speechText("Podczas ćwiczenia wyświetli się podobny ekran");
                        speechText("Od góry wyświetli Ci się seria, liczba pompek do zrobienia w serii oraz przycisk do przejścia do kolejnej serii");
                        speechText("Przejdź dalej");
                        break;
                    case PUSHUPS_SETTINGS_PAGE:
                        currentPage = new PushupsSettingsPage(this);
                        speechText("Ustaw parametry treningu. Po ich ustawieniu rozpocznij trening");
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
            });

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



        //-------------------------------------------------------------------------------//
        //---------------------------------SPEECH METHODS--------------------------------//
        //-------------------------------------------------------------------------------//
        /// <summary>
        /// Method which sets basic Speech Parameters
        /// </summary>
        private void setUpSpeech()
        {
            micOn = true;
            speechOn = true;
            pTTS = new SpeechSynthesizer();
            try
            {
                pTTS.SetOutputToDefaultAudioDevice();
                CultureInfo ci = new CultureInfo("pl-PL");
                pSRE = new SpeechRecognitionEngine(ci);
                pSRE.SetInputToDefaultAudioDevice();
                pSRE.SpeechRecognized += PSRE_SpeechRecognizedPL;

                speechText("Witaj w swoim doradcy treningu");

                //Build Grammars
                //Main grammars
                buildGrammar(MAIN_COMMANDS);
                buildGrammar(NAVIGATION_COMMANDS);
                buildGrammar(WORKOUT_COMMANDS);

                //Spartakus setting grammars
                string[] spartakusSetting = new string[SPARTAKUS_FIRST_SETTING.Length + SPARTAKUS_SECOND_SETTING.Length + SPARTAKUS_THIRD_SETTING.Length];
                SPARTAKUS_FIRST_SETTING.CopyTo(spartakusSetting, 0);
                SPARTAKUS_SECOND_SETTING.CopyTo(spartakusSetting, SPARTAKUS_FIRST_SETTING.Length);
                SPARTAKUS_THIRD_SETTING.CopyTo(spartakusSetting, SPARTAKUS_FIRST_SETTING.Length + SPARTAKUS_SECOND_SETTING.Length);

                buildGrammar(spartakusSetting, NUMBERS, SECONDS);
                buildGrammar(spartakusSetting, TEENS, SECONDS);
                buildGrammar(spartakusSetting, DOZENS, SECONDS);
                buildGrammar(spartakusSetting, HUNDREDS, SECONDS);

                buildGrammar(spartakusSetting, DOZENS, NUMBERS, SECONDS);
                buildGrammar(spartakusSetting, HUNDREDS, NUMBERS, SECONDS);
                buildGrammar(spartakusSetting, HUNDREDS, TEENS, SECONDS);
                buildGrammar(spartakusSetting, HUNDREDS, DOZENS, SECONDS);

                buildGrammar(spartakusSetting, HUNDREDS, DOZENS, NUMBERS, SECONDS);

                //Pushups setting grammars
                buildGrammar(PUSHUPS_TEST_SETTING, NUMBERS, PUSHUPS);
                buildGrammar(PUSHUPS_TEST_SETTING, TEENS, PUSHUPS);
                buildGrammar(PUSHUPS_TEST_SETTING, DOZENS, PUSHUPS);
                buildGrammar(PUSHUPS_TEST_SETTING, DOZENS, NUMBERS, PUSHUPS);

                buildGrammar(PUSHUPS_TRAINING_DAY_SETTING, NUMBERS_ORDERED, DAYS);


                //Weider setting grammars
                string[] weiderSetting = new string[WEIDER_FIRST_SETTING.Length + WEIDER_SECOND_SETTING.Length + WEIDER_THIRD_SETTING.Length + WEIDER_DAY_SETTING.Length];
                WEIDER_FIRST_SETTING.CopyTo(weiderSetting, 0);
                WEIDER_SECOND_SETTING.CopyTo(weiderSetting, WEIDER_FIRST_SETTING.Length);
                WEIDER_THIRD_SETTING.CopyTo(weiderSetting, WEIDER_FIRST_SETTING.Length + WEIDER_SECOND_SETTING.Length);
                WEIDER_DAY_SETTING.CopyTo(weiderSetting, WEIDER_FIRST_SETTING.Length + WEIDER_SECOND_SETTING.Length + WEIDER_THIRD_SETTING.Length);

                buildGrammar(weiderSetting, NUMBERS, SECONDS);
                buildGrammar(weiderSetting, TEENS, SECONDS);

                buildGrammar(weiderSetting, DOZENS, NUMBERS, SECONDS);
                buildGrammar(weiderSetting, HUNDREDS, NUMBERS, SECONDS);
                buildGrammar(weiderSetting, HUNDREDS, TEENS, SECONDS);
                buildGrammar(weiderSetting, HUNDREDS, DOZENS, SECONDS);

                buildGrammar(weiderSetting, HUNDREDS, DOZENS, NUMBERS, SECONDS);

                buildGrammar(weiderSetting, NUMBERS_ORDERED, DAYS);
                buildGrammar(weiderSetting, TEENS_ORDERED, DAYS);
                buildGrammar(weiderSetting, DOZENS_ORDERED, NUMBERS_ORDERED, DAYS);

                //Gym setting grammars
                buildGrammar(GYM_FIRST_SETTING, GYM_MUSCLE_PART);

                // Ustaw rozpoznawanie przy wykorzystaniu wielu gramatyk:
                pSRE.RecognizeAsync(RecognizeMode.Multiple);

                while (micOn == true) {; }
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
            if (confidence > 0.40 && micOn)
            {
                if (txt.IndexOf("Wyłącz syntezator") >= 0) turnOffSpeaker();
                else if (txt.IndexOf("Włącz syntezator") >= 0) turnOnSpeaker();
                else if (txt.IndexOf("Wyłącz mikrofon") >= 0) turnOffMic();
                else if (txt.IndexOf("Pomoc") >= 0) invokePageButtonMethod(buttonHelp);
                else if (txt.IndexOf("Sto pompek") >= 0 || txt.IndexOf("Pompki") >= 0) setWindow(PUSHUPS_MAIN_PAGE);
                else if (txt.IndexOf("Szóstka Łejdera") >= 0) setWindow(WEIDER_MAIN_PAGE);
                else if (txt.IndexOf("Spartakus") >= 0) setWindow(SPARTAKUS_MAIN_PAGE);
                else if (txt.IndexOf("Siłownia") >= 0) setWindow(GYM_MAIN_PAGE);
                else if (txt.IndexOf("Strona główna") >= 0) setWindow(START_PAGE);

                //Actions activated with voice on selected page
                switch (currentPageNumber)
                {

                    case SPARTAKUS_MAIN_PAGE:
                        SpartakusMainPage spartakusMainPage = (SpartakusMainPage)currentPage;
                        if (txt.IndexOf("Dalej") >= 0 || txt.IndexOf("Następna") >= 0) invokePageButtonMethod(spartakusMainPage.nextButton);
                        break;
                    case SPARTAKUS_EXPLANATION_PAGE:
                        SpartakusExplanationPage spartakusExplanationPage = (SpartakusExplanationPage)currentPage;
                        if (txt.IndexOf("Dalej") >= 0 || txt.IndexOf("Następna") >= 0) invokePageButtonMethod(spartakusExplanationPage.nextButton);
                        else if (txt.IndexOf("Wstecz") >= 0 || txt.IndexOf("Wróć") >= 0) invokePageButtonMethod(spartakusExplanationPage.backButton);
                        break;
                    case SPARTAKUS_SETTINGS_PAGE:
                        SpartakusSettingsPage spartakusSettingsPage = (SpartakusSettingsPage)currentPage;
                        setNumberTextBox(spartakusSettingsPage.setSpartakusFirstTime, SPARTAKUS_FIRST_SETTING, txt);
                        setNumberTextBox(spartakusSettingsPage.setSpartakusSecondTime, SPARTAKUS_SECOND_SETTING, txt);
                        setNumberTextBox(spartakusSettingsPage.setSpartakusThirdTime, SPARTAKUS_THIRD_SETTING, txt);
                        if (txt.IndexOf("Rozpocznij trening") >= 0) invokePageButtonMethod(spartakusSettingsPage.nextButton);
                        else if (txt.IndexOf("Wstecz") >= 0 || txt.IndexOf("Wróć") >= 0) invokePageButtonMethod(spartakusSettingsPage.backButton);
                        break;
                    case SPARTAKUS_WORKOUT_PAGE:
                        SpartakusWorkoutPage spartakusWorkoutPage = (SpartakusWorkoutPage)currentPage;
                        if (txt.IndexOf("Przerwij trening") >= 0) invokePageButtonMethod(spartakusWorkoutPage.nextButton);
                        else if (txt.IndexOf("Pauza") >= 0 || txt.IndexOf("Stop") >= 0) invokePageButtonMethod(spartakusWorkoutPage.pauseButton);
                        else if (txt.IndexOf("Wznów") >= 0 || txt.IndexOf("Start") >= 0) invokePageButtonMethod(spartakusWorkoutPage.playButton);
                        else if (txt.IndexOf("Wstecz") >= 0 || txt.IndexOf("Wróć") >= 0)
                        {
                            try
                            {
                                invokePageButtonMethod(spartakusWorkoutPage.backButton);
                            }
                            catch (Exception ex)
                            {
                                pTTS.SpeakAsyncCancelAll();
                                speechText("Polecenie jest nieprawidłowe");
                                Message_WrongWeiderParameters();
                            }
                        }
                        break;


                    case PUSHUPS_MAIN_PAGE:
                        PushupsMainPage pushupsMainPage = (PushupsMainPage)currentPage;
                        if (txt.IndexOf("Dalej") >= 0 || txt.IndexOf("Następna") >= 0) invokePageButtonMethod(pushupsMainPage.nextButton);
                        break;
                    case PUSHUPS_EXPLANATION_PAGE:
                        PushupsExplanationPage pushupsExplanationPage = (PushupsExplanationPage)currentPage;
                        if (txt.IndexOf("Dalej") >= 0 || txt.IndexOf("Następna") >= 0) invokePageButtonMethod(pushupsExplanationPage.nextButton);
                        else if (txt.IndexOf("Wstecz") >= 0 || txt.IndexOf("Wróć") >= 0) invokePageButtonMethod(pushupsExplanationPage.backButton);
                        break;
                    case PUSHUPS_SETTINGS_PAGE:
                        PushupsSettingsPage pushupsSettingsPage = (PushupsSettingsPage)currentPage;
                        setNumberTextBox(pushupsSettingsPage.setTestResult, PUSHUPS_TEST_SETTING, txt);
                        setNumberTextBox(pushupsSettingsPage.setTrainingDay, PUSHUPS_TRAINING_DAY_SETTING, txt);
                        if (txt.IndexOf("Rozpocznij trening") >= 0) invokePageButtonMethod(pushupsSettingsPage.nextButton);
                        else if (txt.IndexOf("Wstecz") >= 0 || txt.IndexOf("Wróć") >= 0) invokePageButtonMethod(pushupsSettingsPage.backButton);
                        break;
                    case PUSHUPS_WORKOUT_PAGE:
                        PushupsWorkoutPage pushupsWorkoutPage = (PushupsWorkoutPage)currentPage;
                        if (txt.IndexOf("Zrobione") >= 0 || txt.IndexOf("Zrobiłem") >= 0 || txt.IndexOf("Zrobiłam") >= 0 || txt.IndexOf("Dalej") >= 0)
                        {
                            try
                            {
                                invokePageButtonMethod(pushupsWorkoutPage.nextSeriesButton);
                            }
                            catch (Exception ex)
                            {
                                pTTS.SpeakAsyncCancelAll();
                                speechText("Wykonano wszystkie serie");
                            }
                        }
                        else if (txt.IndexOf("Wstecz") >= 0 || txt.IndexOf("Wróć") >= 0) invokePageButtonMethod(pushupsWorkoutPage.backButton);
                        break;



                    case WEIDER_MAIN_PAGE:
                        WeiderMainPage weiderMainPage = (WeiderMainPage)currentPage;
                        if (txt.IndexOf("Dalej") >= 0 || txt.IndexOf("Następna") >= 0) invokePageButtonMethod(weiderMainPage.nextButton);
                        break;
                    case WEIDER_SETTINGS_PAGE:
                        WeiderSettingsPage weiderSettingsPage = (WeiderSettingsPage)currentPage;
                        setNumberTextBox(weiderSettingsPage.setWeiderFirstTime, WEIDER_FIRST_SETTING, txt);
                        setNumberTextBox(weiderSettingsPage.setWeiderSecondTime, WEIDER_SECOND_SETTING, txt);
                        setNumberTextBox(weiderSettingsPage.setWeiderThirdTime, WEIDER_THIRD_SETTING, txt);
                        setNumberTextBox(weiderSettingsPage.setWeiderDayProgress, WEIDER_DAY_SETTING, txt);
                        if (txt.IndexOf("Rozpocznij trening") >= 0) invokePageButtonMethod(weiderSettingsPage.nextButton);
                        else if (txt.IndexOf("Wstecz") >= 0 || txt.IndexOf("Wróć") >= 0) invokePageButtonMethod(weiderSettingsPage.backButton);
                        break;
                    case WEIDER_WORKOUT_PAGE:
                        WeiderWorkoutPage weiderWorkoutPage = (WeiderWorkoutPage)currentPage;
                        if (txt.IndexOf("Przerwij trening") >= 0) invokePageButtonMethod(weiderWorkoutPage.nextButton);
                        else if (txt.IndexOf("Pauza") >= 0 || txt.IndexOf("Stop") >= 0) invokePageButtonMethod(weiderWorkoutPage.pauseButton);
                        else if (txt.IndexOf("Wznów") >= 0 || txt.IndexOf("Start") >= 0) invokePageButtonMethod(weiderWorkoutPage.playButton);
                        else if (txt.IndexOf("Wstecz") >= 0 || txt.IndexOf("Wróć") >= 0)
                        {
                            try
                            {
                                invokePageButtonMethod(weiderWorkoutPage.backButton);
                            }
                            catch (Exception ex)
                            {
                                pTTS.SpeakAsyncCancelAll();
                                speechText("Polecenie jest nieprawidłowe");
                                Message_WrongWeiderParameters();
                            }
                        }
                        break;


                    case GYM_MAIN_PAGE:
                        GymMainPage gymMainPage = (GymMainPage)currentPage;
                        if (txt.IndexOf("Dalej") >= 0 || txt.IndexOf("Następna") >= 0) invokePageButtonMethod(gymMainPage.nextButton);
                        break;
                    case GYM_SETTINGS_PAGE:
                        GymSettingsPage gymSettingsPage = (GymSettingsPage)currentPage;

                     //   Message_WrongGymParameters()

                        setCheckBox(gymSettingsPage.setCheckButtonValue, GYM_FIRST_SETTING, txt);
                        if (txt.IndexOf("Rozpocznij trening") >= 0) invokePageButtonMethod(gymSettingsPage.nextButton);
                        else if (txt.IndexOf("Wstecz") >= 0 || txt.IndexOf("Wróć") >= 0) invokePageButtonMethod(gymSettingsPage.backButton);
                        break;


                    case GYM_WORKOUT_PAGE:
                        GymWorkoutPage gymWorkoutPage = (GymWorkoutPage)currentPage;
                        if (txt.IndexOf("Zrobione") >= 0 || txt.IndexOf("Zrobiłem") >= 0 || txt.IndexOf("Zrobiłam") >= 0 || txt.IndexOf("Dalej") >= 0)
                        {
                            try
                            {
                                invokePageButtonMethod(gymWorkoutPage.backButton);
                            }
                            catch (Exception ex)
                            {
                                pTTS.SpeakAsyncCancelAll();
                                speechText("Wykonano wszystkie serie");
                            }
                        }
                        else if (txt.IndexOf("Wstecz") >= 0 || txt.IndexOf("Wróć") >= 0) invokePageButtonMethod(gymWorkoutPage.backButton);
                        break;
                }

            }
            else
            {
                speechText("Proszę powtórzyć");
                return;
            }
        }

        /// <summary>
        /// Recognizes number from word
        /// </summary>
        /// <param name="numberText">Number in text recognized from speech</param>
        /// <returns></returns>
        private int recognizeNumber(string numberText)
        {
            int result = 0;
            string[] words = numberText.Split(' ');
            foreach (string word in words)
            {
                for (int i = 0; i < NUMBERS.Length - 1; i++)
                {
                    if (word.Equals(NUMBERS[i])) result += (i + 1);
                }
                if (word.Equals(NUMBERS[10])) result += 2;
                for (int i = 0; i < NUMBERS_ORDERED.Length - 1; i++)
                {
                    if (word.Equals(NUMBERS_ORDERED[i])) result += (i + 1);
                }


                for (int i = 0; i < TEENS.Length; i++)
                {
                    if (word.Equals(TEENS[i])) result += (i + 11);
                }
                for (int i = 0; i < TEENS_ORDERED.Length; i++)
                {
                    if (word.Equals(TEENS_ORDERED[i])) result += (i + 11);
                }


                for (int i = 0; i < DOZENS.Length; i++)
                {
                    if (word.Equals(DOZENS[i])) result += ((i + 2) * 10);
                }
                for (int i = 0; i < DOZENS_ORDERED.Length; i++)
                {
                    if (word.Equals(DOZENS_ORDERED[i])) result += ((i + 2) * 10);
                }

                for (int i = 0; i < HUNDREDS.Length; i++)
                {
                    if (word.Equals(HUNDREDS[i])) result += ((i + 1) * 100);
                }
            }
            return result;
        }

        /// <summary>
        /// Invokes method of page nested in main window
        /// </summary>
        /// <param name="button"></param>
        private void invokePageButtonMethod(Button button)
        {
            try
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    if (button.IsEnabled)
                    {
                        ButtonAutomationPeer peer = new ButtonAutomationPeer(button);
                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();
                    }
                }));
            }
            catch (Exception ex)
            {
                speechText("Komenda niedozwolona");
            }
        }


        /// <summary>
        /// Sets Page CheckBox which should contain appropriate bool
        /// </summary>
        /// <param name="pageSetCheckBoxMethod">Page method which sets checkbox</param>
        /// <param name="grammarPrefixes">Array of strings which stand before number part of command</param>
        /// <param name="recognizedSpeech">Text of recognized speech</param>
        private void setCheckBox(Action<string, bool> pageSetCheckBoxMethod, string[] grammarPrefixes, string recognizedSpeech)
        {

            foreach (string commandStart in grammarPrefixes)
            {
                bool result = true;
                if (commandStart.StartsWith("Wybierz") || commandStart.StartsWith("Ustaw") || commandStart.StartsWith("Ćwicz")) result = true;
                else if (commandStart.StartsWith("Usuń") || commandStart.StartsWith("Odznacz")) result = false;

                if (recognizedSpeech.StartsWith(commandStart))
                {
                    
                    string musclePart = recognizedSpeech.Substring(commandStart.Length);  

                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        pageSetCheckBoxMethod(musclePart, result);
                    }));
                }
            }
        }


        /// <summary>
        /// Sets Page TextBox which should contain numbers
        /// </summary>
        /// <param name="pageSetTextBoxMethod">Page method which sets textbox</param>
        /// <param name="grammarPrefixes">Array of strings which stand before number part of command</param>
        /// <param name="recognizedSpeech">Text of recognized speech</param>
        private void setNumberTextBox(Action<int> pageSetTextBoxMethod, string[] grammarPrefixes, string recognizedSpeech)
        {
            foreach (string commandStart in grammarPrefixes)
            {
                if (recognizedSpeech.StartsWith(commandStart))
                {
                    string numberWord = recognizedSpeech.Substring(commandStart.Length);
                    numberWord = numberWord.TrimEnd(' ');
                    numberWord = numberWord.Remove(numberWord.LastIndexOf(' ') + 1);
                    int number = recognizeNumber(numberWord);
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        pageSetTextBoxMethod(number);
                    }));
                }
            }
        }

        /// <summary>
        /// Counts to three
        /// </summary>
        /// <param name="i">Nsumber</param>
        public void speechCountThree(int i)
        {
            if (i == 3) speechText("Trzy");
            else if (i == 2) speechText("Dwa");
            else if (i == 1) speechText("Jeden");
        }

        /// <summary>
        /// Speaks text
        /// </summary>
        /// <param name="txt">Text to speak</param>
        public void speechText(string txt)
        {
            if (speechOn) pTTS.SpeakAsync(txt);
        }

        public void turnOnMic()
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                micOn = true;
                speechText("Włączyłeś mikrofon");
                micImage.Source = imageSourceOfString("/icons/microphone-light.png");
            }));
        }

        public void turnOffMic()
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                pTTS.SpeakAsyncCancelAll();
                speechText("Wyłączyłeś Mikrofon");
                micOn = false;
                micImage.Source = imageSourceOfString("/icons/microphone-dark.png");
            }));
        }

        public void turnOnSpeaker()
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                speechOn = true;
                speechText("Włączyłeś syntezator");
                speakerImage.Source = imageSourceOfString("/icons/speaker-light.png");
            }));
        }

        public void turnOffSpeaker()
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                pTTS.SpeakAsyncCancelAll();
                speechText("Wyłączyłeś syntezator");
                speechOn = false;
                speakerImage.Source = imageSourceOfString("/icons/speaker-dark.png");
            }));
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

        /// <summary>
        /// MessageBox displayed when Weider paramaeters are wrong
        /// </summary>
        public static void Message_WrongWeiderParameters()
        {
            string text = "Podano złe wartości. Należy wprowadzić liczby całkowite.";
            string caption = "Złe dane";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBox.Show(text, caption, button, icon);
        }

        /// <summary>
        /// MessageBox displayed when none of Gym paramaeters is unchecked
        /// </summary>
        public static void Message_WrongGymParameters()
        {
            string text = "Nie wybrano Partii mięściowych, wybierz conajmniej jedną z podanych opcji";
            string caption = "Brak wyboru";
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

        /// <summary>
        /// MessageBox displaying operations allowed on page.
        /// </summary>
        /// <param name="operations"></param>
        public static void Message_OperationsAllowed(string[] operations)
        {
            string caption = "Komendy głosowe na stronie";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;

            string text = "";
            for (int i = 0; i < operations.Length; i++)
            {
                text += operations[i] + Environment.NewLine;
            }

            MessageBox.Show(text, caption, button, icon);
        }
    }
}
