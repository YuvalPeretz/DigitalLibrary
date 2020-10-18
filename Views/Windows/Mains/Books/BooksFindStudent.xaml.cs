using DigitalLibrary.Models.Classes.Useable;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace DigitalLibrary.Views.Windows.Mains.Books
{
    /// <summary>
    /// Interaction logic for BooksFindStudent.xaml
    /// </summary>
    public partial class BooksFindStudent : Window
    {
        MainFilesHandler MFH = MainFilesHandler.Instance;
        BooksWindow BW;
        public BooksFindStudent()
        {
            InitializeComponent();
            BW = Application.Current.Windows.OfType<BooksWindow>().First();
        }

        private void BTN_SearchStudent_Click(object sender, RoutedEventArgs e)
        {
            // Goes thorugh all the students and searching for those which have the chosen book
            // Prints it out afterwords

            if (MFH.CheckLineExistens($"שם:{TB_SearchStudent.Text}", Paths.booksFile))
            {
                if (MFH.CheckLineExistens($"שם הספר:{TB_SearchStudent.Text}", Paths.studentsFile))
                {
                    string studentsWhoHasBook = null, currentStudentname = null;
                    foreach (var line in MFH.GetAllRawLines(Paths.studentsFile))
                    {
                        if (line.Contains($"שם:"))
                            currentStudentname = line.Substring(line.IndexOf(":") + 1);
                        if (line.Equals($"שם הספר:{TB_SearchStudent.Text}"))
                            studentsWhoHasBook += currentStudentname + "\n";
                    }
                    BW.WriteToConsole($"הספר '{TB_SearchStudent.Text}' הושאל על ידי התלמידים:\n{studentsWhoHasBook}");
                }
                else
                    MessageBox.Show("ספר זה לא קיים אצל שום תלמיד במערכת");
            }
            else
                MessageBox.Show("ספר זה לא קיים במערכת");
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void BTN_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
