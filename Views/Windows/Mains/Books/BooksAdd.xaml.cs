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

namespace DigitalLibrary.Views.Windows.Mains.Books
{
    public partial class BooksAdd : Window
    {
        MainFilesHandler MFH = MainFilesHandler.Instance;
        string filePath = "";
        public BooksAdd()
        {
            InitializeComponent();
            DP_Published.SelectedDate = DateTime.Parse("1/1/1000");
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
        private void BTN_FilePicker_Click(object sender, RoutedEventArgs e)
        {
            // In case of choosing a different image, this function handels the file picking process

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                filePath = dlg.FileName;
                TB_FileImage.Source = new BitmapImage(new Uri(filePath));
                L_Beforehand.Padding = new Thickness(0);
            }
        }
        private void TB_Quantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Limits the TB_Quantity to numbers only

            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void BTN_AddBook_Click(object sender, RoutedEventArgs e)
        {
            // Adding a book to the file
            // The adding process is Task related

            var preItem = new object();
            Label DataLabel;
            foreach (var spi in MainSP.Children.OfType<StackPanel>())
            {
                StackPanel sp = (StackPanel)spi;
                foreach (var item in sp.Children)
                {
                    if (item.GetType() != typeof(Label))
                    {
                        if (item.GetType() == typeof(TextBox))
                        {
                            TextBox t = (TextBox)item;
                            if (string.IsNullOrEmpty(t.Text))
                            {
                                DataLabel = (Label)preItem;
                                if (DataLabel.Content.Equals("שם הספר:"))
                                    DataLabel.Foreground = new SolidColorBrush(Colors.Red);
                                if (DataLabel.Content.Equals("מחבר:"))
                                    DataLabel.Foreground = new SolidColorBrush(Colors.Green);
                            }
                            else
                            {
                                DataLabel = (Label)preItem;
                                DataLabel.Foreground = new SolidColorBrush(Colors.Black);
                            }
                        }
                        else if (item.GetType() == typeof(ComboBox))
                        {
                            ComboBox c = (ComboBox)item;
                            if (c.SelectedIndex == -1)
                            {
                                DataLabel = (Label)preItem;
                                DataLabel.Foreground = new SolidColorBrush(Colors.Red);
                            }
                            else
                            {
                                DataLabel = (Label)preItem;
                                DataLabel.Foreground = new SolidColorBrush(Colors.Black);
                            }
                        }
                        else if (item.GetType() == typeof(DatePicker))
                        {
                            DatePicker d = (DatePicker)item;
                            if (d.SelectedDate == null)
                            {
                                DataLabel = (Label)preItem;
                                DataLabel.Foreground = new SolidColorBrush(Colors.Green);
                            }
                            else
                            {
                                DataLabel = (Label)preItem;
                                DataLabel.Foreground = new SolidColorBrush(Colors.Black);
                            }
                        }
                        else if (item.GetType() == typeof(Button))
                            continue;
                        else if (item.GetType() == typeof(TextBlock))
                        {
                            TextBlock t = (TextBlock)item;
                            if (string.IsNullOrEmpty(t.Text))
                            {
                                DataLabel = (Label)preItem;
                                DataLabel.Foreground = new SolidColorBrush(Colors.Green);
                            }
                            else
                            {
                                DataLabel = (Label)preItem;
                                DataLabel.Foreground = new SolidColorBrush(Colors.Black);
                            }
                        }
                    }
                    preItem = item;
                }

            }
            if (
                L_BookName.Foreground.ToString() == Brushes.Black.ToString() &
                L_BookGenre.Foreground.ToString() == Brushes.Black.ToString())
            {
                if (
                    L_Published.Foreground.ToString() != Brushes.Black.ToString() ||
                    L_Author.Foreground.ToString() != Brushes.Black.ToString() ||
                    L_Beforehand.Foreground.ToString() != Brushes.Black.ToString() ||
                    L_BookQuantity.Foreground.ToString() != Brushes.Black.ToString())
                {
                    if (MessageBox.Show("לא כל התאים מלאים, האם תרצו להמשיך בוודאות?",
                        "אזהרה",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        string tempBookName = TB_BookName.Text, tempGenre = CB_BookGenre.Text, tempAuthor = null, tempFileName = null;
                        DateTime tempPublished = DateTime.Now;
                        int tempQuantity = 1;
                        if (L_Published.Foreground.ToString() == Brushes.Black.ToString())
                            tempPublished = DateTime.Parse(DP_Published.SelectedDate.Value.Date.ToShortDateString());
                        if (L_Author.Foreground.ToString() == Brushes.Black.ToString())
                            tempAuthor = TB_Author.Text;
                        if (L_Beforehand.Foreground.ToString() == Brushes.Black.ToString())
                            tempFileName = filePath;
                        if (L_BookQuantity.Foreground.ToString() == Brushes.Black.ToString())
                            tempQuantity = Int32.Parse(TB_Quantity.Text);
                        MFH.AddToImageFile(filePath);
                        Book newBook = new Book(
                            tempBookName,
                            tempGenre,
                            tempPublished,
                            tempAuthor,
                            tempFileName,
                            tempQuantity
                            );
                        newBook.AddBook(newBook);
                        TB_BookName.Text = String.Empty;
                        DP_Published.SelectedDate = DateTime.Parse("1/1/1000");
                        TB_Author.Text = String.Empty;
                        TB_FileImage.Source = null;
                        TB_Quantity.Text = "1";
                        return;
                    }
                }
                else
                {
                    MFH.AddToImageFile(filePath);
                    Book newBook = new Book(
                        TB_BookName.Text,
                        CB_BookGenre.Text,
                        DateTime.Parse(DP_Published.SelectedDate.Value.Date.ToShortDateString()),
                        TB_Author.Text,
                        filePath,
                        Int32.Parse(TB_Quantity.Text));
                    newBook.AddBook(newBook);
                    TB_BookName.Text = String.Empty;
                    DP_Published.SelectedDate = DateTime.Parse("1/1/1");
                    TB_Author.Text = String.Empty;
                    TB_FileImage.Source = null;
                    TB_Quantity.Text = "1";
                    return;
                }
            }
            else
            {
                MessageBox.Show("יש בעיה ביצירת הספר.\n חובה למלא את התאים המסומנים באדום.");
            }
        }
    }
}