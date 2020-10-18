using DigitalLibrary.Models.Classes.Common;
using System;
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
using System.Windows.Shapes;

namespace DigitalLibrary.Views.Windows.Mains.Students.StudentsInfoUpdate
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        Student studentHolder, studentToEdit, s = Student.Instance;
        StudentsWindow SW;
        public EditWindow(Student studentToEdit)
        {
            InitializeComponent();
            studentHolder = studentToEdit; // holds the information of the student for later use in the UpdateInfo function
            this.studentToEdit = studentToEdit;
            InitializeData();
        }
        private void InitializeData()
        {
            // Initialize the fields in the window to match those of the student

            TB_Name.Text = studentHolder.Name;
            TB_PhoneNum.Text = studentHolder.PhoneNum;
        }
        private void BTN_EditBook_Click(object sender, RoutedEventArgs e)
        {
            // After editing a student, a message will be printed on the console
            // Which describes the changes that were made

            string changedS = null;
            if (!TB_Name.Text.Equals(studentHolder.Name) ||
                !TB_PhoneNum.Text.Equals(studentHolder.PhoneNum))
            {
                if (MessageBox.Show($"התלמיד {studentHolder.Name} עומד להיערך, להמשיך?", "אזהרה", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    changedS = $"המידה שהשתנה בתלמיד {studentHolder.Name} הוא:\n";
                    if (!TB_Name.Text.Equals(studentHolder.Name))
                    {
                        changedS += $"שם התלמיד השתנה מ '{studentHolder.Name}' ל '{TB_Name.Text}'\n";
                        studentToEdit.Name = TB_Name.Text;
                    }
                    if (!TB_PhoneNum.Text.Equals(studentHolder.PhoneNum))
                    {
                        changedS += $"מספר הטלפון של התלמיד השתנה מ '{studentHolder.PhoneNum}' ל '{TB_PhoneNum.Text}'\n";
                        studentToEdit.PhoneNum = TB_PhoneNum.Text;
                    }
                    Task tUpdateInfo = Task.Run(()=> 
                    { 
                        s.UpdateInfo(studentHolder,studentToEdit);
                    });
                    tUpdateInfo.GetAwaiter().OnCompleted(()=> 
                    {
                        SW = Application.Current.Windows.OfType<StudentsWindow>().First();
                        SW.WriteToConsole(changedS);
                        this.Close();
                    });
                }
            }
        }

        private void BTN_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
