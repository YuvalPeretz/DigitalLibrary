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

namespace DigitalLibrary.Views.Windows.Mains.Students.StudentsLendBook
{
    /// <summary>
    /// Interaction logic for LendBookWindow.xaml
    /// </summary>
    public partial class LendBookWindow : Window
    {
        ObservableCollection<Book> OCBooks = new ObservableCollection<Book>();
        CollectionViewSource BooksCollection;
        Predicate<object> yourCostumFilter;
        ICollectionView Itemlist;
        Book b = Book.Instance, bookToAdd;
        StudentsWindow SW;
        Student s = Student.Instance, studentToAddTo;
        Task<ObservableCollection<Book>> tOCBooks;
        public LendBookWindow(Student student)
        {
            SW = Application.Current.Windows.OfType<StudentsWindow>().First();
            InitializeObservableCollection();
            Counter();
            InitializeComponent();
            studentToAddTo = student;
        }
        private async void Counter()
        {
            // If the loading of the OCBooks (which are the books list) haven't finished in 1.7 seconds
            // A message will be printed to the console saying it is still loading

            await Task.Delay(1700);
            if (!tOCBooks.IsCompleted)
                SW.WriteToConsole("טוען ספרים, נא להמתין.");
        }
        private void InitializeObservableCollection()
        {
            // Firstly, the function clears the OCBooks from any information
            // After that, a task will start returning the updated OCBooks
            // After the complition of the task, a setup for the UI will start
            // The use of the task is because we don't want it to freeze the UI

            if (OCBooks.Count != 0)
                OCBooks.Clear();
            tOCBooks = Task.Run(() =>
            {
                foreach (var item in ChangeBooksImagesToBitmapImage(b.GetBooksList()))
                {
                    if (item.Quantity > 0)
                        OCBooks.Add(item);
                }
                return OCBooks;
            });
            tOCBooks.GetAwaiter().OnCompleted(() =>
            {
                Mouse.OverrideCursor = null;
                BooksCollection = new CollectionViewSource { Source = OCBooks };
                Itemlist = BooksCollection.View;
                DG_BooksList.ItemsSource = Itemlist;
                RefreshItemList();
                CB_Filter.IsEnabled = true;
                TB_SearchBox.IsEnabled = true;
            });
        }
        private void RefreshItemList()
        {
            // Refreshes the items in the ObservableCollection

            Itemlist.Filter = yourCostumFilter;
            Itemlist.Refresh();
        }
        private List<Book> ChangeBooksImagesToBitmapImage(List<Book> list)
        {
            // Parse all books urls to actual images

            List<Book> newbl = new List<Book>();
            foreach (var book in list)
            {
                if (!string.IsNullOrEmpty(book.ImgURL))
                {
                    BitmapImage logo = new BitmapImage();
                    logo.BeginInit();
                    logo.UriSource = new Uri(book.ImgURL);
                    logo.DecodePixelHeight = 25;
                    logo.DecodePixelWidth = 25;
                    logo.EndInit();
                    newbl.Add(
                        new Book(
                            book.BookName,
                            book.Genre,
                            DateTime.Parse(book.Published.ToShortDateString()),
                            book.Author,
                            logo,
                            book.Quantity));
                }
                else
                    newbl.Add(book);
            }
            return newbl;
        }
        private void TB_SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Refrshes the ObservableCollection to filter only chosen items

            if (CB_Filter.SelectedIndex == 0)
                yourCostumFilter = new Predicate<object>(item => ((Book)item).BookName.Contains(TB_SearchBox.Text));
            else if (CB_Filter.SelectedIndex == 1)
                yourCostumFilter = new Predicate<object>(item => ((Book)item).Genre.Contains(TB_SearchBox.Text));
            else if (CB_Filter.SelectedIndex == 2)
                yourCostumFilter = new Predicate<object>(item => ((Book)item).Published.ToString().Contains(TB_SearchBox.Text));
            else if (CB_Filter.SelectedIndex == 3)
                yourCostumFilter = new Predicate<object>(item => ((Book)item).Author.Contains(TB_SearchBox.Text));
            else if (CB_Filter.SelectedIndex == 4)
                yourCostumFilter = new Predicate<object>(item => ((Book)item).Quantity.ToString().Contains(TB_SearchBox.Text));
            RefreshItemList();
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
        private void BTN_Lend_Click(object sender, RoutedEventArgs e)
        {
            // Once clicked, the book will be added to the students BorrowedBooks List
            // And then the UI will be updated

            bookToAdd = (Book)DG_BooksList.SelectedItem;
            List<string> studentBorrowedBooks = new List<string>();
            studentToAddTo.BorrowedBooks.ForEach(borrowedBook => { studentBorrowedBooks.Add(borrowedBook.BookName); });
            if (bookToAdd.Quantity > 0)
            {
                if (!studentBorrowedBooks.Contains(bookToAdd.BookName))
                {
                    s.AddBookToStudent(bookToAdd, studentToAddTo);
                    bookToAdd.Quantity--;
                    RefreshItemList();
                    if (bookToAdd.Quantity == 0)
                        OCBooks.RemoveAt(DG_BooksList.SelectedIndex);
                }
                else
                    MessageBox.Show("ספר זה כבר קיים אצל התלמיד במערכת");
            }
            else
                MessageBox.Show("אין ספרים להשאיל מספר זה.");
        }
    }
}
