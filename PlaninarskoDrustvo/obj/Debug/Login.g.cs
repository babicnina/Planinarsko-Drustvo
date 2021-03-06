﻿#pragma checksum "..\..\Login.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "DE85D9C4027B542472F59444B531E4C59351E893"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using PlaninarskoDrustvo;
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


namespace PlaninarskoDrustvo {
    
    
    /// <summary>
    /// Login
    /// </summary>
    public partial class Login : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image loginImage;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LoginPage;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Username;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.PackIcon ErrorIcon;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ErrorMessage;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox Password;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.PackIcon ErrorIconPassword;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ErrorMessagePassword;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid PasswordChangePage;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordNew;
        
        #line default
        #line hidden
        
        
        #line 135 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordNewConfirm;
        
        #line default
        #line hidden
        
        
        #line 141 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.PackIcon ErrorIconPasswordConfirm;
        
        #line default
        #line hidden
        
        
        #line 145 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ErrorMessagePasswordConfirm;
        
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
            System.Uri resourceLocater = new System.Uri("/PlaninarskoDrustvo;component/login.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Login.xaml"
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
            
            #line 9 "..\..\Login.xaml"
            ((PlaninarskoDrustvo.Login)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.WindowDrag);
            
            #line default
            #line hidden
            
            #line 9 "..\..\Login.xaml"
            ((PlaninarskoDrustvo.Login)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            
            #line 9 "..\..\Login.xaml"
            ((PlaninarskoDrustvo.Login)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.loginImage = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.LoginPage = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.Username = ((System.Windows.Controls.TextBox)(target));
            
            #line 52 "..\..\Login.xaml"
            this.Username.GotFocus += new System.Windows.RoutedEventHandler(this.Username_GotFocus);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ErrorIcon = ((MaterialDesignThemes.Wpf.PackIcon)(target));
            return;
            case 6:
            this.ErrorMessage = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.Password = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 73 "..\..\Login.xaml"
            this.Password.GotFocus += new System.Windows.RoutedEventHandler(this.Password_GotFocus);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ErrorIconPassword = ((MaterialDesignThemes.Wpf.PackIcon)(target));
            return;
            case 9:
            this.ErrorMessagePassword = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            
            #line 88 "..\..\Login.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonLogIn_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.PasswordChangePage = ((System.Windows.Controls.Grid)(target));
            return;
            case 12:
            this.PasswordNew = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 121 "..\..\Login.xaml"
            this.PasswordNew.GotFocus += new System.Windows.RoutedEventHandler(this.PasswordChange_GotFocus);
            
            #line default
            #line hidden
            return;
            case 13:
            this.PasswordNewConfirm = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 139 "..\..\Login.xaml"
            this.PasswordNewConfirm.GotFocus += new System.Windows.RoutedEventHandler(this.PasswordChange_GotFocus);
            
            #line default
            #line hidden
            return;
            case 14:
            this.ErrorIconPasswordConfirm = ((MaterialDesignThemes.Wpf.PackIcon)(target));
            return;
            case 15:
            this.ErrorMessagePasswordConfirm = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 16:
            
            #line 153 "..\..\Login.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonSavePassword_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

