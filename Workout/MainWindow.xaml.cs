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

namespace Workout
{
    /// <summary>
    /// Interaction for MainWindow.xaml.cs class
    /// </summary>
    public partial class MainWindow : Window
    {
        ///Microsoft Speech parameters
        public static SpeechRecognitionEngine pSRE;
        public static bool speechOn;
        public static SpeechSynthesizer pTTS;
        public Thread speechThread;

        public MainWindow()
        {
            InitializeComponent();
            setStartupWindowParameters();
            speechThread = new Thread(new ThreadStart(setUpSpeech));
            speechThread.Start();
        }


        //-------------------------------------------------------------------------------//
        //---------------------------------WINDOW METHODS--------------------------------//
        //-------------------------------------------------------------------------------//
        /// <summary>
        /// Method which initializes Startup Window and its parameters and outlook.
        /// </summary>
        private void setStartupWindowParameters()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            StartPage startPage = new StartPage();
            mainFrame.Content = startPage;
        }

        /// <summary>
        /// Closes application and stops all threads on window close
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            Environment.Exit(0);
        }



        //-------------------------------------------------------------------------------//
        //---------------------------------SPEECH METHODS--------------------------------//
        //-------------------------------------------------------------------------------//
        /// <summary>
        /// Method which sets basic Speech Parameters
        /// </summary>
        private static void setUpSpeech()
        {
            speechOn = true;
            pTTS = new SpeechSynthesizer();
            try
            {
                pTTS.SetOutputToDefaultAudioDevice();
                CultureInfo ci = new CultureInfo("en-US");
                pSRE = new SpeechRecognitionEngine(ci);
                pSRE.SetInputToDefaultAudioDevice();
                pSRE.SpeechRecognized += PSRE_SpeechRecognizedPL;
                // -------------------------------------------------------------------------
                // Budowa gramatyki numer 1 - POLECENIA SYSTEMOWE
                // Budowa gramatyki numer 1 - określenie komend:
                Choices stopChoice = new Choices();
                stopChoice.Add("Stop");
                stopChoice.Add("Pomoc");

                // Budowa gramatyki numer 1 - definiowanie składni gramatyki:
                GrammarBuilder buildGrammarSystem = new GrammarBuilder();
                buildGrammarSystem.Append(stopChoice);

                // Budowa gramatyki numer 1 - utworzenie gramatyki:
                Grammar grammarSystem = new Grammar(buildGrammarSystem); //
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
        public static void PSRE_SpeechRecognizedPL(object sender, SpeechRecognizedEventArgs e)
        {
            string txt = e.Result.Text;
            string comments;
            float confidence = e.Result.Confidence;
            comments = String.Format("ROZPOZNANO (wiarygodność: {0:0.000}): '{1}'",
           e.Result.Confidence, txt);
            Console.WriteLine(comments);
            if (confidence > 0.40)
            {
                if (txt.IndexOf("Stop") >= 0)
                {
                    speechOn = false;
                }
                else if (txt.IndexOf("Pomoc") >= 0)
                {
                    pTTS.SpeakAsync("Składnia polecenia: Oblicz liczba operacja liczba. Na przykład: oblicz dwa plus trzy.");
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


        
    }
}
