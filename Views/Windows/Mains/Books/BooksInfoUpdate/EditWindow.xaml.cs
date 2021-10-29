using DigitalLibrary.Models.Classes.Common;
using DigitalLibrary.Models.Classes.Useable;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DigitalLibrary.Views.Windows.Mains.Books.BooksInfoUpdate
{
    public partial class EditWindow : Window
    {
        TextFileHandler TFH = TextFileHandler.Instance;
        string filePath = "",tempBookName, tempGenre, tempPublisher = null, tempFileName = null;
        DateTime tempPublished = DateTime.Now;
        int tempQuantity = 1;
        bool logochanged = false;
        BooksWindow BW;
        Book bookToEdit,preBook, b = Book.Instance;
        public EditWindow(Book book)
        {
            InitializeComponent();
            BW = Application.Current.Windows.OfType<BooksWindow>().First();
            preBook = new Book(book);
            bookToEdit = book;
            InitializeData(bookToEdit);
            if(bookToEdit.Img != null)
                bookToEdit.ImgURL = bookToEdit.Img.ToString();
            InitializeBook();
        }
        private void InitializeData(Book bookToEdit)
        {
            tempBookName = bookToEdit.BookName;
            tempGenre = bookToEdit.Genre;
            tempPublished = bookToEdit.Published;
            tempPublisher = bookToEdit.Publisher;
            if (bookToEdit.ImgURL != null)
                tempFileName = bookToEdit.ImgURL;
            if(bookToEdit.Img != null)
                tempFileName = bookToEdit.Img.ToString();
            tempQuantity = bookToEdit.Quantity;
        }
        private void InitializeBook()
        {
            TB_BookName.Text = tempBookName;
            if (tempGenre.Equals("רומן"))
                CB_BookGenre.SelectedIndex = 0;
            else if (tempGenre.Equals("קומיקס"))
                CB_BookGenre.SelectedIndex = 1;
            else if (tempGenre.Equals("פנטנזיה"))
                CB_BookGenre.SelectedIndex = 2;
            else if (tempGenre.Equals("מותחן"))
                CB_BookGenre.SelectedIndex = 3;
            else if (tempGenre.Equals("היסטוריה"))
                CB_BookGenre.SelectedIndex = 4;
            else if (tempGenre.Equals("מדע בדיוני"))
                CB_BookGenre.SelectedIndex = 5;
            else if (tempGenre.Equals("ילדים"))
                CB_BookGenre.SelectedIndex = 6;
            DP_Published.SelectedDate = tempPublished;
            TB_Publisher.Text = tempPublisher;
            if(tempFileName != null)
                TB_FileImage.Source = new BitmapImage(new Uri(tempFileName));
            TB_Quantity.Text = tempQuantity.ToString();
        }
        
        private void BTN_EditBook_Click(object sender, RoutedEventArgs e)
        {
            string changedS = null;
            if (
                tempBookName != TB_BookName.Text ||
                tempGenre != CB_BookGenre.SelectionBoxItem.ToString() ||
                tempPublished != DP_Published.SelectedDate ||
                tempPublisher != TB_Publisher.Text ||
                logochanged ||
                tempQuantity != Int32.Parse(TB_Quantity.Text))
            {
                if (MessageBox.Show($"הספר {preBook.BookName} עומד להיערך, להמשיך?", "אזהרה", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    changedS = $"המידע שהשתנה בספר {tempBookName} הוא:\n";
                    if (tempBookName != TB_BookName.Text)
                    {
                        changedS += $"שם הספר, מ\"{tempBookName}\" ל\"{TB_BookName.Text}\"\n";
                        bookToEdit.BookName = TB_BookName.Text;
                    }
                    if (tempGenre != CB_BookGenre.SelectionBoxItem.ToString())
                    {
                        changedS += $"ז'אנר הספר, מ\"{tempGenre}\" ל\"{CB_BookGenre.SelectionBoxItem.ToString()}\"\n";
                        bookToEdit.Genre = CB_BookGenre.SelectionBoxItem.ToString();
                    }
                    if (tempPublished != DP_Published.SelectedDate)
                    {
                        changedS += $"הוצל''א הספר, מ\"{tempPublished}\" ל\"{DP_Published.SelectedDate}\"\n";
                        bookToEdit.Published = (DateTime)DP_Published.SelectedDate;
                    }
                    if (tempPublisher != TB_Publisher.Text)
                    {
                        changedS += $"מוצל''א הספר, מ\"{tempPublisher}\" ל\"{TB_Publisher.Text}\"\n";
                        bookToEdit.Publisher = TB_Publisher.Text;
                    }
                    if (logochanged)
                    {
                        changedS += $"תמונת הספר\n";
                        bookToEdit.ImgURL = filePath;
                        bookToEdit.Img = new BitmapImage(new Uri(filePath));
                    }
                    if (tempQuantity != Int32.Parse(TB_Quantity.Text))
                    {
                        changedS += $"כמות הספרים, מ\"{tempQuantity}\" ל\"{TB_Quantity.Text}\"\n";
                        bookToEdit.Quantity = Int32.Parse(TB_Quantity.Text);
                    }
                    b.UpdateInfo(preBook, bookToEdit);
                    BW.WriteToConsole(changedS);
                    this.Close();
                }
            }
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
        private void TB_Quantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void BTN_FilePicker_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
            };
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                filePath = dlg.FileName;
                TB_FileImage.Source = new BitmapImage(new Uri(filePath));
                L_Beforehand.Padding = new Thickness(0);
                logochanged = true;
            }
        }
    }
}
