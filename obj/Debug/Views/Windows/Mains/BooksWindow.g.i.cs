﻿#pragma checksum "..\..\..\..\..\Views\Windows\Mains\BooksWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B3B27AAFE38DBB89648FCDD477E7E57E88ED5D72E24CDB4EE4239109E623B997"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DigitalLibrary.Views.Windows.Mains;
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


namespace DigitalLibrary.Views.Windows.Mains {
    
    
    /// <summary>
    /// BooksWindow
    /// </summary>
    public partial class BooksWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 32 "..\..\..\..\..\Views\Windows\Mains\BooksWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BTN_ShowBooks;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\..\Views\Windows\Mains\BooksWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BTN_AddBook;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\..\Views\Windows\Mains\BooksWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BTN_DeleteBook;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\..\..\Views\Windows\Mains\BooksWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BTN_ChangeInfo;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\..\..\Views\Windows\Mains\BooksWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel SP_Console;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\..\..\Views\Windows\Mains\BooksWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BTN_Exit;
        
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
            System.Uri resourceLocater = new System.Uri("/DigitalLibrary;component/views/windows/mains/bookswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\Windows\Mains\BooksWindow.xaml"
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
            
            #line 17 "..\..\..\..\..\Views\Windows\Mains\BooksWindow.xaml"
            ((DigitalLibrary.Views.Windows.Mains.BooksWindow)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.BTN_ShowBooks = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\..\..\..\Views\Windows\Mains\BooksWindow.xaml"
            this.BTN_ShowBooks.Click += new System.Windows.RoutedEventHandler(this.BTN_ShowBooks_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.BTN_AddBook = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\..\..\..\Views\Windows\Mains\BooksWindow.xaml"
            this.BTN_AddBook.Click += new System.Windows.RoutedEventHandler(this.BTN_AddBook_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.BTN_DeleteBook = ((System.Windows.Controls.Button)(target));
            
            #line 67 "..\..\..\..\..\Views\Windows\Mains\BooksWindow.xaml"
            this.BTN_DeleteBook.Click += new System.Windows.RoutedEventHandler(this.BTN_DeleteBook_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.BTN_ChangeInfo = ((System.Windows.Controls.Button)(target));
            
            #line 80 "..\..\..\..\..\Views\Windows\Mains\BooksWindow.xaml"
            this.BTN_ChangeInfo.Click += new System.Windows.RoutedEventHandler(this.BTN_ChangeInfo_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.SP_Console = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 7:
            this.BTN_Exit = ((System.Windows.Controls.Button)(target));
            
            #line 108 "..\..\..\..\..\Views\Windows\Mains\BooksWindow.xaml"
            this.BTN_Exit.Click += new System.Windows.RoutedEventHandler(this.BTN_Exit_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
