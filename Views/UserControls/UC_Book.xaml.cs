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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DigitalLibrary.Views.UserControls
{
    /// <summary>
    /// Interaction logic for UC_Book.xaml
    /// </summary>
    public partial class UC_Book : UserControl
    {
        public UC_Book(string BookName, string Genre, DateTime? Published = null, string Author = null, string ImgURL = null, int Quantity = 1)
        {
            InitializeComponent();
            BitmapImage logo = new BitmapImage();
            logo.UriSource = new Uri(ImgURL);
            BookImg.Source = logo;
            TB_BookName.Text = BookName;
            TB_BookGenre.Text = Genre;
            TB_BookPublished.Text = Published.ToString();
            TB_BookAuthor.Text = Author;
            TB_BookQuantity.Text = Quantity.ToString();
        }

        public UC_Book(Book book)
        {
            InitializeComponent();
            BitmapImage logo = new BitmapImage();
            logo.UriSource = new Uri(book.ImgURL = @"http://192.100.106.657:8811/some/part/here/version1/api");
            BookImg.Source = logo;
            TB_BookName.Text = book.BookName;
            TB_BookGenre.Text = book.Genre;
            TB_BookPublished.Text = book.Published.ToString();
            TB_BookAuthor.Text = book.Author;
            TB_BookQuantity.Text = book.Quantity.ToString();
        }
    }
}
