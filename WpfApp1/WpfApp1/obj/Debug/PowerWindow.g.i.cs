﻿#pragma checksum "..\..\PowerWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "BF8D73936F41DB0FA7BBDD809E89716FBB439507D0D66D7F2F3D54EE17F00D58"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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
using WpfApp1;


namespace WpfApp1 {
    
    
    /// <summary>
    /// PowerWindow
    /// </summary>
    public partial class PowerWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\PowerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel container;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\PowerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton add;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\PowerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton edit;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\PowerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox nameTextBox;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\PowerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox priceTextBox;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\PowerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox powerTextBox;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\PowerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox countTextBox;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\PowerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button saveButton;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\PowerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button saveEditButton;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\PowerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listView;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApp1;component/powerwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\PowerWindow.xaml"
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
            this.container = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 2:
            this.add = ((System.Windows.Controls.RadioButton)(target));
            
            #line 21 "..\..\PowerWindow.xaml"
            this.add.Checked += new System.Windows.RoutedEventHandler(this.Radio_Checked);
            
            #line default
            #line hidden
            return;
            case 3:
            this.edit = ((System.Windows.Controls.RadioButton)(target));
            
            #line 26 "..\..\PowerWindow.xaml"
            this.edit.Checked += new System.Windows.RoutedEventHandler(this.Radio_Checked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.nameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.priceTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 40 "..\..\PowerWindow.xaml"
            this.priceTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.priceTextBox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 6:
            this.powerTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.countTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.saveButton = ((System.Windows.Controls.Button)(target));
            
            #line 63 "..\..\PowerWindow.xaml"
            this.saveButton.Click += new System.Windows.RoutedEventHandler(this.saveButton_Click_add);
            
            #line default
            #line hidden
            return;
            case 9:
            this.saveEditButton = ((System.Windows.Controls.Button)(target));
            
            #line 70 "..\..\PowerWindow.xaml"
            this.saveEditButton.Click += new System.Windows.RoutedEventHandler(this.saveButton_Click_edit);
            
            #line default
            #line hidden
            return;
            case 10:
            this.listView = ((System.Windows.Controls.ListView)(target));
            
            #line 78 "..\..\PowerWindow.xaml"
            this.listView.PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.listView_PreviewMouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 82 "..\..\PowerWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.deleteButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

