﻿#pragma checksum "..\..\..\..\Gym\GymExplanationPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "9F816A3E795A568EDE9758B970B86A000E0E7422AF2E149130BE78E64A34C28F"
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
    /// GymExplanationPage
    /// </summary>
    public partial class GymExplanationPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 37 "..\..\..\..\Gym\GymExplanationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelCounterInfo;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\Gym\GymExplanationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelCounterExample;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\Gym\GymExplanationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelExNameInstruction;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\Gym\GymExplanationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imageExInstruction;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\..\Gym\GymExplanationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelExInfo;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\Gym\GymExplanationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelSeriesInfo;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\..\Gym\GymExplanationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button backButton;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\..\Gym\GymExplanationPage.xaml"
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
            System.Uri resourceLocater = new System.Uri("/Workout;component/gym/gymexplanationpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Gym\GymExplanationPage.xaml"
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
            this.labelCounterInfo = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.labelCounterExample = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.labelExNameInstruction = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.imageExInstruction = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.labelExInfo = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.labelSeriesInfo = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.backButton = ((System.Windows.Controls.Button)(target));
            
            #line 84 "..\..\..\..\Gym\GymExplanationPage.xaml"
            this.backButton.Click += new System.Windows.RoutedEventHandler(this.backButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.nextButton = ((System.Windows.Controls.Button)(target));
            
            #line 97 "..\..\..\..\Gym\GymExplanationPage.xaml"
            this.nextButton.Click += new System.Windows.RoutedEventHandler(this.nextButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
