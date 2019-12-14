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
            string[] description = new string[6] {
                  "Stojąc w lekkim rozkroku, unoś ramiona w bok do wysokości barków \nProponowana liczba powtórzeń: [16][14][12][10]" +
                  "\nUnoszenie gryfu na prostych ramionach (powoli unoś sztangę szerokim łukiem i utrzymaj ją przez chwilę na linii barków) \nProponowana liczba powtórzeń: [12][10][8][6][4]"    //ćwiczenia na ramiona
                , "\"Plank\": nogi wyprostowane, łokcie znajdują się pod linią barków a wzrok skierowany w dół, ciało znajduje się w linii prostej. \nProponowany czas wykonywania ćwiczenia w serii: 60 sekund"      //ćwiczenia na brzuch 
                , "Wiosłowanie sztangą w opadzie tułowia. \nProponowana liczba powtórzeń: [20][18][16][14][12]" +
                  "\nPodciąganie nachwytem. \nProponowana liczba powtórzeń: [10][9][8][7][6]"        //ćwiczenia na plecy
                , "Biceps: chwyć hantle i przyciągaj je na zmianę w kierunku barków, nie dotykając ich, przytrzymując je około 15 cm od barków." +
                  "\nTriceps: prostowanie ramion na wyciągu w pozycji stojącej. \nProponowana liczba powtórzeń: [20][18][16][14]"        //ćwiczenia na ręce
                , "Wyciskanie sztangi na ławce płaskiej." +
                  "\nRozpiętki na maszynie w pozycji siedzącej \nProponowana liczba powtórzeń: [12][10][8][6][4]"       //ćwiczenia na klatkę
                , "Przysiady typu high bar (sztanga wysoko na ramionach) i low bar (sztanga na wysokości łopatek). \nWykonuj przysiady do momentu, w którym Twoja miednica zaczyna się podwijać." +
                  "\nMartwy ciąg. \nProponowana liczba powtórzeń: [12][10][8][6][4]"        //ćwiczenia na nogi
            };            

            for (int item = 0; item < muscles.Count; item++)
            {
                theGrid.RowDefinitions.Add(new RowDefinition());
                Image img = new Image();
                TextBlock desc = new TextBlock();
                Border borderBG = new Border();
                img.BeginInit();
                img.Source = new BitmapImage(new Uri("/img/" + muscles[item] + ".png", UriKind.Relative));
                img.EndInit();

                if (muscles[item] == "schoulder") { desc.Text = description[0]; }
                else if (muscles[item] == "abs") { desc.Text = description[1]; }
                else if (muscles[item] == "back") { desc.Text = description[2]; }
                else if (muscles[item] == "arm") { desc.Text = description[3]; }
                else if (muscles[item] == "chest") { desc.Text = description[4]; }
                else { desc.Text = description[5]; };

                img.SetValue(Grid.RowProperty, muscles.Count - 1 - item);
                img.SetValue(Grid.ColumnProperty, (item+1)%2);
                img.MinHeight = 100;
                img.MinWidth = 100;
                img.MaxHeight = 250;
                img.MaxWidth = 250;

                desc.SetValue(Grid.RowProperty, muscles.Count - 1 - item);
                desc.SetValue(Grid.ColumnProperty, (item+2) % 2);
                
                img.HorizontalAlignment = HorizontalAlignment.Center;
                desc.VerticalAlignment = VerticalAlignment.Center;
                desc.TextWrapping = TextWrapping.Wrap;
                desc.Margin = new Thickness(20);

                TextBlock.SetFontSize(desc, 15);
                TextBlock.SetForeground(desc, Brushes.White);
                TextBlock.SetTextAlignment(desc, TextAlignment.Justify);

                theGrid.Children.Add(img);
                theGrid.Children.Add(desc);
            }

        }


        /// <summary>
        /// Goes to previous page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            theGrid.Children.Clear();
            theGrid.RowDefinitions.Clear();
 
            mainWindow.setWindow(MainWindow.GYM_SETTINGS_PAGE);          
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.setWindow(MainWindow.START_PAGE);
        }
    }
}
