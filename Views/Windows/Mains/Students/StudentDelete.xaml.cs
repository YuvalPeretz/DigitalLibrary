using DigitalLibrary.Models.Classes.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace DigitalLibrary.Views.Windows.Mains.Students
{
    /// <summary>
    /// Interaction logic for StudentDelete.xaml
    /// </summary>
    public partial class StudentDelete : Window
    {
        ObservableCollection<Student> OCStudents = new ObservableCollection<Student>();
        CollectionViewSource StudentsCollection;
        Predicate<object> yourCostumFilter;
        ICollectionView Itemlist;
        StudentsWindow SW;
        Student s = Student.Instance;
        BookBorrowed borrowedBookDate;
        Task<ObservableCollection<Student>> tOCStudents;
        public StudentDelete()
        {
            SW = Application.Current.Windows.OfType<StudentsWindow>().First();
            InitializeObservableCollection();
            Counter();
            InitializeComponent();
        }
        private async void Counter()
        {
            // If the loading of the OCStudents (which are the students list) haven't finished in 1.7 seconds
            // A message will be printed to the console saying it is still loading

            await Task.Delay(1700);
            if (!tOCStudents.IsCompleted)
                SW.WriteToConsole("טוען תלמידים, נא להמתין.");
        }
        private void InitializeObservableCollection()
        {
            // Firstly, the function clears the OCStudents from any information
            // After that, a task will start returning the updated OCBooks
            // After the complition of the task, a setup for the UI will start
            // The use of the task is because we don't want it to freeze the UI

            if (OCStudents.Count != 0)
                OCStudents.Clear();
            tOCStudents = Task.Run(() =>
            {
                foreach (var student in s.GetStudentList())
                    OCStudents.Add(student);
                return OCStudents;
            });
            tOCStudents.GetAwaiter().OnCompleted(() =>
            {
                Mouse.OverrideCursor = null;
                StudentsCollection = new CollectionViewSource { Source = OCStudents };
                Itemlist = StudentsCollection.View;
                DG_StudentsList.ItemsSource = Itemlist;
                CB_Filter.IsEnabled = true;
            });
        }

        private void RefreshItemList()
        {
            // Refreshes the items in the ObservableCollection

            Itemlist.Filter = yourCostumFilter;
            Itemlist.Refresh();
        }

        private void BTN_Delete_Click(object sender, RoutedEventArgs e)
        {
            // Delets student from file, then updateds UI

            if (DG_StudentsList.SelectedItem != null)
            {
                Student studentToDelete = (Student)DG_StudentsList.SelectedItem;
                if (MessageBox.Show($"התלמיד {studentToDelete.Name} עומד להימחק, להמשיך במחיקה?", "אזהרה", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Task tDeleting = Task.Run(()=>s.DeleteStudent(studentToDelete));
                    tDeleting.GetAwaiter().OnCompleted(()=> 
                    {
                        OCStudents.RemoveAt(DG_StudentsList.SelectedIndex);
                        RefreshItemList();
                        SW.WriteToConsole($"התלמיד {studentToDelete.Name} נמחק בהצלחה");
                    });
                }
            }
            else
                MessageBox.Show("יש לבחור ספר למחיקה");
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void TB_SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Refrshes the ObservableCollection to filter only chosen items

            if (CB_Filter.SelectedIndex == 0)
                yourCostumFilter = new Predicate<object>(item => ((Student)item).Name.Contains(TB_SearchBox.Text));
            else if (CB_Filter.SelectedIndex == 1)
                yourCostumFilter = new Predicate<object>(item => ((Student)item).PhoneNum.Contains(TB_SearchBox.Text));
            Itemlist.Filter = yourCostumFilter;
            Itemlist.Refresh();
        }
        private void CB_Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(sender.ToString()))
                TB_SearchBox.IsEnabled = true;
            else
                TB_SearchBox.IsEnabled = false;
        }
        private void BTN_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void CB_BookName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Everytime the selected item is changed, the focused borrowedbook will be changed
            // This happens in order for the display of the borrowed date to be able to desplay

            ComboBox CB = (ComboBox)sender;
            BookBorrowed bbHolder = (BookBorrowed)CB.SelectedItem;
            borrowedBookDate = s.GetBorrowedBookInfo((Student)DG_StudentsList.SelectedItem, bbHolder.BookName);
            BTN_ShowDate.Visibility = Visibility.Visible;
        }

        private void ShowDate_Click(object sender, RoutedEventArgs e)
        {
            // Shows the actual borrowed date

            MessageBox.Show(borrowedBookDate.BookLendDate.ToShortDateString());
        }
    }
}
