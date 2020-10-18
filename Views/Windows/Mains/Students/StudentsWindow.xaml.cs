using DigitalLibrary.Models.Classes.Useable;
using DigitalLibrary.Views.UserControls;
using DigitalLibrary.Views.Windows.Mains.Students.StudentsInfoUpdate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Documents.Serialization;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DigitalLibrary.Views.Windows.Mains.Students
{
    /// <summary>
    /// Interaction logic for StudentsWindow.xaml
    /// </summary>
    public partial class StudentsWindow : Window
    {
        Window MW,SA,SL,SD,SIU,SLB,SRB;
        MainFilesHandler MFH = MainFilesHandler.Instance;
        public StudentsWindow()
        {
            InitializeComponent();
            InitializeTempFiles();
        }
        private void InitializeTempFiles() { MFH.InitializeFiles(WriteToConsole); } // Making sure all the main files exist
        private void BTN_ShowStudents_Click(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<StudentsList>().Any())
            {
                Mouse.OverrideCursor = Cursors.Wait;
                SL = new StudentsList
                {
                    Tag = "mdi_child"
                };
                SL.ShowDialog();
            }
        }

        private void BTN_AddStudents_Click(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<StudentsAdd>().Any())
            {
                SA = new StudentsAdd
                {
                    Tag = "mdi_child"
                };
                SA.ShowDialog();
            }
        }

        private void BTN_DeleteStudents_Click(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<StudentDelete>().Any())
            {
                Mouse.OverrideCursor = Cursors.Wait;
                SD = new StudentDelete
                {
                    Tag = "mdi_child"
                };
                SD.ShowDialog();
            }
        }

        private void BTN_ChangeInfo_Click(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<StudentsInfoUpdate.StudentsInfoUpdate>().Any())
            {
                Mouse.OverrideCursor = Cursors.Wait;
                SIU = new StudentsInfoUpdate.StudentsInfoUpdate
                {
                    Tag = "mdi_child"
                };
                SIU.ShowDialog();
            }
        }

        private void BTN_LendBook_Click(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<StudentsLendBook.StudentsLendBook>().Any())
            {
                Mouse.OverrideCursor = Cursors.Wait;
                SLB = new StudentsLendBook.StudentsLendBook
                {
                    Tag = "mdi_child"
                };
                SLB.ShowDialog();
            }
        }

        private void BTN_ReturnBook_Click(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<StudentsReturnBook>().Any())
            {
                Mouse.OverrideCursor = Cursors.Wait;
                SRB = new StudentsReturnBook
                {
                    Tag = "mdi_child"
                };
                SRB.ShowDialog();
            }
        }
        public void WriteToConsole(string text)
        {
            // Writes down to this window's console the string that is being passed

            ConsoleText CS = new ConsoleText(text + "\n");
            SP_Console.Children.Add(CS);
        }
        private void BTN_Exit_Click(object sender, RoutedEventArgs e)
        {
            MW = (MainWindow)Window.GetWindow(App.Current.MainWindow);
            MW.Visibility = Visibility.Visible;
            this.Close();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
