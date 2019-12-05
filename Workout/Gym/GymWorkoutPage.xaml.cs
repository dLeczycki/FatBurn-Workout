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
            string[] description = new string[6] { "ramiona ", "brzuch", "Podciąganie oraz martwy ciąg", "ręce", "Podnoszenie sztangi na ławce płaskiej", "nogi" };            

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

                TextBlock.SetFontSize(desc, 24);
                TextBlock.SetForeground(desc, Brushes.White);
                TextBlock.SetTextAlignment(desc, TextAlignment.Center);

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
