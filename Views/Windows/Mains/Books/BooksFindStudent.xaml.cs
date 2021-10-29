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
        TextFileHandler TFH = TextFileHandler.Instance;
        public BooksFindStudent()
        {
            InitializeComponent();
        }

        private void BTN_SearchStudent_Click(object sender, RoutedEventArgs e)
        {
            if (TFH.CheckLineExistens($"שם:{TB_SearchStudent.Text}", Paths.booksFile))
            {
                if (TFH.CheckLineExistens($"שם הספר:{TB_SearchStudent.Text}", Paths.studentsFile))
                {
                    string studentName = null;
                    bool outOfBooks = false;
                    List<string> lines = File.ReadAllLines(Paths.studentsFile).ToList();
                    for (int i = TFH.GetSpecificLineNum($"שם הספר:{TB_SearchStudent.Text}", Paths.studentsFile); i != 0; i--)
                    {
                        if (outOfBooks)
                        {
                            if (lines[i].Contains("שם:"))
                            {
                                studentName = lines[i].Substring(lines[i].IndexOf(":")+1);
                            }
                        }
                        if (lines[i].Equals("{"))
                            outOfBooks = true;
                    }
                    MessageBox.Show($"שם התלמיד שהשאיל את הספר הוא: {studentName}");
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
