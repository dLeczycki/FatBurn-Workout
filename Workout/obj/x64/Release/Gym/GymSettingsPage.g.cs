﻿#pragma checksum "..\..\..\..\Gym\GymSettingsPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3647D58A52148541DE02D61F6A6C22B1E987B78AFA506C2BA3A3A8E02D16DEDB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Workout.Gym;


namespace Workout.Gym {
    
    
    /// <summary>
    /// GymSettingsPage
    /// </summary>
    public partial class GymSettingsPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 57 "..\..\..\..\Gym\GymSettingsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imageSchoulder;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\Gym\GymSettingsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imageAbs;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\..\Gym\GymSettingsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imageArm;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\Gym\GymSettingsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imageChest;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\..\Gym\GymSettingsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imageLeg;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\Gym\GymSettingsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imageBack;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\..\Gym\GymSettingsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button backButton;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\..\Gym\GymSettingsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button nextButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Workout;component/gym/gymsettingspage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Gym\GymSettingsPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.imageSchoulder = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.imageAbs = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.imageArm = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.imageChest = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.imageLeg = ((System.Windows.Controls.Image)(target));
            return;
            case 6:
            this.imageBack = ((System.Windows.Controls.Image)(target));
            return;
            case 7:
            this.backButton = ((System.Windows.Controls.Button)(target));
            
            #line 87 "..\..\..\..\Gym\GymSettingsPage.xaml"
            this.backButton.Click += new System.Windows.RoutedEventHandler(this.backButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.nextButton = ((System.Windows.Controls.Button)(target));
            
            #line 96 "..\..\..\..\Gym\GymSettingsPage.xaml"
            this.nextButton.Click += new System.Windows.RoutedEventHandler(this.nextButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

