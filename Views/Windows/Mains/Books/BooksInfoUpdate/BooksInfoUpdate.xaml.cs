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

namespace DigitalLibrary.Views.Windows.Mains.Books.BooksInfoUpdate
{
    /// <summary>
    /// Interaction logic for BooksInfoUpdate.xaml
    /// </summary>
    public partial class BooksInfoUpdate : Window
    {
        ObservableCollection<Book> OCBooks = new ObservableCollection<Book>();
        CollectionViewSource BooksCollection;
        Predicate<object> yourCostumFilter;
        ICollectionView Itemlist;
        Book b = Book.Instance;
        Window BW,EW;
        public BooksInfoUpdate()
        {
            InitializeComponent();
            InitializeObservableCollection();
            BW = Application.Current.Windows.OfType<BooksWindow>().First();
        }
        private void InitializeObservableCollection()
        {
            if (OCBooks.Count != 0)
                OCBooks.Clear();
            foreach (var item in ChangeBooksImagesToBitmapImage(b.GetBooksList()))
                OCBooks.Add(item);
            BooksCollection = new CollectionViewSource { Source = OCBooks };
            Itemlist = BooksCollection.View;
            DG_BooksList.ItemsSource = Itemlist;
            RefreshItemList();

        }
        private void RefreshItemList()
        {
            Itemlist.Filter = yourCostumFilter;
            Itemlist.Refresh();
        }
        private List<Book> ChangeBooksImagesToBitmapImage(List<Book> list)
        {
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
                            book.Publisher,
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
            if (CB_Filter.SelectedIndex == 0)
                yourCostumFilter = new Predicate<object>(item => ((Book)item).BookName.Contains(TB_SearchBox.Text));
            else if (CB_Filter.SelectedIndex == 1)
                yourCostumFilter = new Predicate<object>(item => ((Book)item).Genre.Contains(TB_SearchBox.Text));
            else if (CB_Filter.SelectedIndex == 2)
                yourCostumFilter = new Predicate<object>(item => ((Book)item).Published.ToString().Contains(TB_SearchBox.Text));
            else if (CB_Filter.SelectedIndex == 3)
                yourCostumFilter = new Predicate<object>(item => ((Book)item).Publisher.Contains(TB_SearchBox.Text));
            else if (CB_Filter.SelectedIndex == 4)
                yourCostumFilter = new Predicate<object>(item => ((Book)item).Quantity.ToString().Contains(TB_SearchBox.Text));
            RefreshItemList();
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
        private void BTN_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (DG_BooksList.SelectedItem != null)
            {
                Book bookToEdit = (Book)DG_BooksList.SelectedItem;
                if (!Application.Current.Windows.OfType<EditWindow>().Any())
                {
                    EW = new EditWindow(bookToEdit)
                    {
                        Tag = "mdi_child"
                    };
                    EW.ShowDialog();
                    InitializeObservableCollection();
                }
            }
            else
                MessageBox.Show("יש לבחור ספר לעריכה");
        }
    }
}
