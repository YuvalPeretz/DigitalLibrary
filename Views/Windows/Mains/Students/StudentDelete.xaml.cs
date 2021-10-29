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

namespace DigitalLibrary.Views.Windows.Mains.Students
{
    /// <summary>
    /// Interaction logic for StudentDelete.xaml
    /// </summary>
    public partial class StudentDelete : Window
    {
        ObservableCollection<Student> OBStudents = new ObservableCollection<Student>();
        CollectionViewSource StudentsCollection;
        Predicate<object> yourCostumFilter;
        ICollectionView Itemlist;
        StudentsWindow SW;
        Student s = Student.Instance;
        BookBorrowed borrowedBookDate;
        public StudentDelete()
        {
            InitializeComponent();
            InitializeObservableCollection();
            SW = Application.Current.Windows.OfType<StudentsWindow>().First();
        }
        private void InitializeObservableCollection()
        {
            if (OBStudents.Count != 0)
                OBStudents.Clear();
            foreach (var item in s.GetStudentList())
                OBStudents.Add(item); ;
            StudentsCollection = new CollectionViewSource { Source = OBStudents };
            Itemlist = StudentsCollection.View;
            DG_StudentsList.ItemsSource = Itemlist;
        }

        private void BTN_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DG_StudentsList.SelectedItem != null)
            {
                Student studentToDelete = (Student)DG_StudentsList.SelectedItem;
                if (MessageBox.Show($"התלמיד {studentToDelete.Name} עומד להימחק, להמשיך במחיקה?", "אזהרה", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    s.DeleteStudent(studentToDelete);
                    InitializeObservableCollection();
                    SW.WriteToConsole($"התלמיד {studentToDelete.Name} נמחק בהצלחה");
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
            ComboBox CB = (ComboBox)sender;
            BookBorrowed bbHolder = (BookBorrowed)CB.SelectedItem;
            borrowedBookDate = s.GetBorrowedBookInfo((Student)DG_StudentsList.SelectedItem, bbHolder.BookName);
            BTN_ShowDate.Visibility = Visibility.Visible;
        }

        private void ShowDate_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(borrowedBookDate.BookLendDate.ToShortDateString());
        }
    }
}
