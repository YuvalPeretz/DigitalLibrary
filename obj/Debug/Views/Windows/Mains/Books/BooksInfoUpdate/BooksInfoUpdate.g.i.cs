﻿#pragma checksum "..\..\..\..\..\..\..\Views\Windows\Mains\Books\BooksInfoUpdate\BooksInfoUpdate.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1E1CD15B0E1300499CD1ACC558530081760A2B76E2C0F4CB61C15232D90E8036"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DigitalLibrary.Views.Windows.Mains.Books.BooksInfoUpdate;
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


namespace DigitalLibrary.Views.Windows.Mains.Books.BooksInfoUpdate {
    
    
    /// <summary>
    /// BooksInfoUpdate
    /// </summary>
    public partial class BooksInfoUpdate : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 46 "..\..\..\..\..\..\..\Views\Windows\Mains\Books\BooksInfoUpdate\BooksInfoUpdate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CB_Filter;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\..\..\..\Views\Windows\Mains\Books\BooksInfoUpdate\BooksInfoUpdate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TB_SearchBox;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\..\..\..\Views\Windows\Mains\Books\BooksInfoUpdate\BooksInfoUpdate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DG_BooksList;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\..\..\..\..\Views\Windows\Mains\Books\BooksInfoUpdate\BooksInfoUpdate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BTN_Edit;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\..\..\..\..\..\Views\Windows\Mains\Books\BooksInfoUpdate\BooksInfoUpdate.xaml"
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
            System.Uri resourceLocater = new System.Uri("/DigitalLibrary;component/views/windows/mains/books/booksinfoupdate/booksinfoupda" +
                    "te.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\..\Views\Windows\Mains\Books\BooksInfoUpdate\BooksInfoUpdate.xaml"
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
            
            #line 21 "..\..\..\..\..\..\..\Views\Windows\Mains\Books\BooksInfoUpdate\BooksInfoUpdate.xaml"
            ((DigitalLibrary.Views.Windows.Mains.Books.BooksInfoUpdate.BooksInfoUpdate)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.CB_Filter = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.TB_SearchBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 63 "..\..\..\..\..\..\..\Views\Windows\Mains\Books\BooksInfoUpdate\BooksInfoUpdate.xaml"
            this.TB_SearchBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TB_SearchBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.DG_BooksList = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 5:
            this.BTN_Edit = ((System.Windows.Controls.Button)(target));
            
            #line 101 "..\..\..\..\..\..\..\Views\Windows\Mains\Books\BooksInfoUpdate\BooksInfoUpdate.xaml"
            this.BTN_Edit.Click += new System.Windows.RoutedEventHandler(this.BTN_Edit_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.BTN_Exit = ((System.Windows.Controls.Button)(target));
            
            #line 112 "..\..\..\..\..\..\..\Views\Windows\Mains\Books\BooksInfoUpdate\BooksInfoUpdate.xaml"
            this.BTN_Exit.Click += new System.Windows.RoutedEventHandler(this.BTN_Exit_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

