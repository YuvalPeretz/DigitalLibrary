﻿#pragma checksum "..\..\..\..\..\..\..\Views\Windows\Mains\Students\StudentsLendBook\StudentsLendBook.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E6B5679A60E37E3CE06F44EA632B1803CC14C4D25E01BAC33DABEB9DE34FDB28"
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


namespace DigitalLibrary.Views.Windows.Mains.Students.StudentsLendBook {
    
    
    /// <summary>
    /// StudentsLendBook
    /// </summary>
    public partial class StudentsLendBook : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 45 "..\..\..\..\..\..\..\Views\Windows\Mains\Students\StudentsLendBook\StudentsLendBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CB_Filter;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\..\..\..\Views\Windows\Mains\Students\StudentsLendBook\StudentsLendBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TB_SearchBox;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\..\..\..\..\Views\Windows\Mains\Students\StudentsLendBook\StudentsLendBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DG_StudentsList;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\..\..\..\..\Views\Windows\Mains\Students\StudentsLendBook\StudentsLendBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn BTN_ShowDate;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\..\..\..\..\Views\Windows\Mains\Students\StudentsLendBook\StudentsLendBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BTN_LendBook;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\..\..\..\..\..\Views\Windows\Mains\Students\StudentsLendBook\StudentsLendBook.xaml"
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
            System.Uri resourceLocater = new System.Uri("/DigitalLibrary;component/views/windows/mains/students/studentslendbook/studentsl" +
                    "endbook.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\..\Views\Windows\Mains\Students\StudentsLendBook\StudentsLendBook.xaml"
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
            
            #line 20 "..\..\..\..\..\..\..\Views\Windows\Mains\Students\StudentsLendBook\StudentsLendBook.xaml"
            ((DigitalLibrary.Views.Windows.Mains.Students.StudentsLendBook.StudentsLendBook)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.CB_Filter = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.TB_SearchBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 59 "..\..\..\..\..\..\..\Views\Windows\Mains\Students\StudentsLendBook\StudentsLendBook.xaml"
            this.TB_SearchBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TB_SearchBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.DG_StudentsList = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 6:
            this.BTN_ShowDate = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 8:
            this.BTN_LendBook = ((System.Windows.Controls.Button)(target));
            
            #line 104 "..\..\..\..\..\..\..\Views\Windows\Mains\Students\StudentsLendBook\StudentsLendBook.xaml"
            this.BTN_LendBook.Click += new System.Windows.RoutedEventHandler(this.BTN_LendBook_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.BTN_Exit = ((System.Windows.Controls.Button)(target));
            
            #line 115 "..\..\..\..\..\..\..\Views\Windows\Mains\Students\StudentsLendBook\StudentsLendBook.xaml"
            this.BTN_Exit.Click += new System.Windows.RoutedEventHandler(this.BTN_Exit_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 5:
            
            #line 86 "..\..\..\..\..\..\..\Views\Windows\Mains\Students\StudentsLendBook\StudentsLendBook.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CB_BookName_SelectionChanged);
            
            #line default
            #line hidden
            break;
            case 7:
            
            #line 95 "..\..\..\..\..\..\..\Views\Windows\Mains\Students\StudentsLendBook\StudentsLendBook.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowDate_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

