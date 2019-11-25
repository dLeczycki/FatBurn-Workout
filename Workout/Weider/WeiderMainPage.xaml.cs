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

namespace Workout.Weider
{
    /// <summary>
    /// Interaction logic for WeiderMainPage.xaml
    /// </summary>
    public partial class WeiderMainPage : Page
    {
        public MainWindow mainWindow;
        public WeiderMainPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.setWindow(MainWindow.WEIDER_SETTINGS_PAGE);
        }
    }
}
