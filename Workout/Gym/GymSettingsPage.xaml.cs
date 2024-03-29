﻿using System;
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

namespace Workout.Gym
{
    /// <summary>
    /// Interaction logic for GymSettingsPage.xaml
    /// </summary>
    public partial class GymSettingsPage : Page
    {
        private MainWindow mainWindow;
        public GymSettingsPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.setWindow(MainWindow.GYM_MAIN_PAGE);
        }


        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                //     if (mainWindow.muscles.Any())
                setGymParameters();
                if (mainWindow.muscles.Any())
                    mainWindow.setWindow(MainWindow.GYM_WORKOUT_PAGE); 
                else
                {
                    MainWindow.Message_WrongGymParameters();
                    setGymParameters();
                }
                    

            }
            catch (Exception ex)
            {
                MainWindow.Message_WrongGymParameters();
            }
        }

        /// <summary>
        /// Sets gym training, and breaks times.
        /// </summary>
        /// <returns></returns>
        private bool setGymParameters()
        {
            mainWindow.muscles.Clear();

            if (cbSchoulder.IsChecked == true) mainWindow.muscles.Add("schoulder");
            if (cbAbs.IsChecked == true) mainWindow.muscles.Add("abs");
            if (cbArm.IsChecked == true) mainWindow.muscles.Add("arm");
            if (cbChest.IsChecked == true) mainWindow.muscles.Add("chest");
            if (cbLeg.IsChecked == true) mainWindow.muscles.Add("leg");
            if (cbBack.IsChecked == true) mainWindow.muscles.Add("back");

          //  if (mainWindow.muscles.Any())  MainWindow.Message_WrongGymParameters();
            return true;
        }


        public void setCheckButtonValue(string musclePart, bool result)
        {
            if (musclePart == "abs" || musclePart == "brzuch") cbAbs.IsChecked = result;
            if (musclePart == "ramiona" || musclePart == "biceps" || musclePart == "triceps") cbArm.IsChecked = result;
            if (musclePart == "barki") cbSchoulder.IsChecked = result;
            if (musclePart == "nogi") cbLeg.IsChecked = result;
            if (musclePart == "plecy") cbBack.IsChecked = result;
            if (musclePart == "klatę" || musclePart == "klatkę") cbChest.IsChecked = result;
        }

    }
}
