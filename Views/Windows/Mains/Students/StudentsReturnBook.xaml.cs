﻿using DigitalLibrary.Models.Classes.Common;
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
    /// Interaction logic for StudentsReturnBook.xaml
    /// </summary>
    public partial class StudentsReturnBook : Window
    {
        ObservableCollection<Student> OBStudents = new ObservableCollection<Student>();
        CollectionViewSource StudentsCollection;
        Predicate<object> yourCostumFilter;
        ICollectionView Itemlist;
        StudentsWindow SW;
        Student s = Student.Instance;
        ComboBox CB;
        Book b = Book.Instance;
        BookBorrowed borrowedBookDate, borrowedBookHolder;
        public StudentsReturnBook()
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
                OBStudents.Add(item);
            StudentsCollection = new CollectionViewSource { Source = OBStudents };
            Itemlist = StudentsCollection.View;
            DG_StudentsList.ItemsSource = Itemlist;
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

        private void BTN_ReturnBook_Click(object sender, RoutedEventArgs e)
        {
            if (borrowedBookHolder != null)
            {
                Student selectedStudent = (Student)DG_StudentsList.SelectedItem;
                List<BookBorrowed> borrowedBooks = selectedStudent.BorrowedBooks;
                borrowedBooks.RemoveAll(b => b.BookName == borrowedBookHolder.BookName);
                Student selectedStudentUpdated = new Student(selectedStudent.Name, selectedStudent.PhoneNum, borrowedBooks);
                s.UpdateInfo(selectedStudent, selectedStudentUpdated);
                Book updatedBook = b.GetBookByInfo($"שם:{borrowedBookHolder.BookName}");
                updatedBook.Quantity++;
                b.UpdateInfo(b.GetBookByInfo($"שם:{borrowedBookHolder.BookName}"), updatedBook);
                SW.WriteToConsole($"הספר {borrowedBookHolder.BookName} נמחק בהצלחה מהתלמיד {selectedStudent.Name}");
                if (CB.Items.Count == 0)
                    BTN_ShowDate.Visibility = Visibility.Hidden;
            }
            else
                SW.WriteToConsole("שום ספר לא נבחר");
            InitializeObservableCollection();
        }
        private void CB_BookName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CB = (ComboBox)sender;
            borrowedBookHolder = (BookBorrowed)CB.SelectedItem;
            borrowedBookDate = s.GetBorrowedBookInfo((Student)DG_StudentsList.SelectedItem, borrowedBookHolder.BookName);
            BTN_ShowDate.Visibility = Visibility.Visible;
        }

        private void ShowDate_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(borrowedBookDate.BookLendDate.ToShortDateString());
        }
    }
}
