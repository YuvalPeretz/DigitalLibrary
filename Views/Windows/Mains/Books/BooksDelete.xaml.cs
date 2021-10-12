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

namespace DigitalLibrary.Views.Windows.Mains.Books
{
    /// <summary>
    /// Interaction logic for BooksDelete.xaml
    /// </summary>
    public partial class BooksDelete : Window
    {
        ObservableCollection<Book> OCBooks = new ObservableCollection<Book>();
        CollectionViewSource BooksCollection;
        Predicate<object> yourCostumFilter;
        ICollectionView Itemlist;
        Book b = Book.Instance;
        BooksWindow BW;
        Task<ObservableCollection<Book>> tOCBooks;
        public BooksDelete()
        {
            BW = Application.Current.Windows.OfType<BooksWindow>().First();
            InitializeObservableCollection();
            Counter();
            InitializeComponent();
        }
        private async void Counter()
        {
            // If the loading of the OCBooks (which are the books list) haven't finished in 1.7 seconds
            // A message will be printed to the console saying it is still loading

            await Task.Delay(1700);
            if (!tOCBooks.IsCompleted)
                BW.WriteToConsole("טוען ספרים, נא להמתין.");
        }
        private void InitializeObservableCollection()
        {
            // Firstly, the function clears the OCBooks from any information
            // After that, a task will start returning the updated OCBooks
            // After the complition of the task, a setup for the UI will start
            // The use of the task is because we don't want it to freeze the UI

            if (OCBooks.Count != 0)
                OCBooks.Clear();
            tOCBooks = Task.Run(()=> 
            {
                foreach (var item in ChangeBooksImagesToBitmapImage(b.GetBooksList()))
                    OCBooks.Add(item);
                return OCBooks;
            });
            tOCBooks.GetAwaiter().OnCompleted(()=> 
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
                    logo.Freeze();
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
        private void BTN_Delete_Click(object sender, RoutedEventArgs e)
        {
            // Deletes the chosen book from the file

            if (DG_BooksList.SelectedItem != null)
            {
                Book bookToDelete = (Book)DG_BooksList.SelectedItem;
                if (MessageBox.Show($"הספר {bookToDelete.BookName} עומד להימחק, להמשיך במחיקה?", "אזהרה", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    b.DeleteBook(bookToDelete.BookName);
                    OCBooks.RemoveAt(DG_BooksList.SelectedIndex);
                    RefreshItemList();
                    BW.WriteToConsole($"הספר {bookToDelete.BookName} נמחק בהצלחה");
                }
            }
            else
                MessageBox.Show("יש לבחור ספר למחיקה");
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
    }
}
