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

namespace Workout.Pushups
{
    /// <summary>
    /// Interaction logic for PushupsMainPage.xaml
    /// </summary>
    public partial class PushupsMainPage : Page
    {
        private MainWindow mainWindow;
        public PushupsMainPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.setWindow(MainWindow.PUSHUPS_EXPLANATION_PAGE);
        }
    }
}
