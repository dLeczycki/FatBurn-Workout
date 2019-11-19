using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Globalization;
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

namespace Workout
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SpeechRecognitionEngine pSRE;
        public bool speechOn;
        public SpeechSynthesizer pTTS;

        public MainWindow()
        {
            InitializeComponent();
            setStartupWindowParameters();
            setStartupSpeechParameters();
        }

        private void setStartupSpeechParameters()
        {
            try
            {
                pTTS.SetOutputToDefaultAudioDevice();
                CultureInfo ci = new CultureInfo("pl-PL");
                pSRE = new SpeechRecognitionEngine(ci);
                pSRE.SetInputToDefaultAudioDevice();
                pSRE.SpeechRecognized += PSRE_SpeechRecognizedPL;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }

        private void setStartupWindowParameters()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            StartPage startPage = new StartPage();
            mainFrame.Content = startPage;
        }

        public static void PSRE_SpeechRecognizedPL(object sender, SpeechRecognizedEventArgs e)
        {
        }
    }
}
