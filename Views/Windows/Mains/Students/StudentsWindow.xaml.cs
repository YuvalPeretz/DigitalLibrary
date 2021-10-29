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
        TextFileHandler TFH = TextFileHandler.Instance;
        public StudentsWindow()
        {
            InitializeComponent();
            InitializeTempFiles();
        }
        private void InitializeTempFiles() { TFH.InitializeFiles(WriteToConsole); }
        private void BTN_ShowStudents_Click(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<StudentsList>().Any())
            {
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
                SRB = new StudentsReturnBook
                {
                    Tag = "mdi_child"
                };
                SRB.ShowDialog();
            }
        }
        public void WriteToConsole(string text)
        {
            ConsoleText CS = new ConsoleText(text);
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
