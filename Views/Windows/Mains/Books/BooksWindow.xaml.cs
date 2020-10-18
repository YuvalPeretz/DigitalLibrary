using DigitalLibrary.Models.Classes.Common;
using DigitalLibrary.Models.Classes.Useable;
using DigitalLibrary.Views.UserControls;
using DigitalLibrary.Views.Windows.Mains.Books;
using BooksInfoUpdate = DigitalLibrary.Views.Windows.Mains.Books.BooksInfoUpdate.BooksInfoUpdate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DigitalLibrary.Views.Windows.Mains
{
    public partial class BooksWindow : Window
    {
        Window MW,BL,BA,BD,BIU,BFS;
        MainFilesHandler MFH = MainFilesHandler.Instance;

        public BooksWindow()
        {
            InitializeComponent();
            InitializeTempFiles();
        }

        private void InitializeTempFiles() { MFH.InitializeFiles(WriteToConsole); } // Making sure all the main files exist
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void BTN_Exit_Click(object sender, RoutedEventArgs e)
        {
            MW = (MainWindow)Window.GetWindow(App.Current.MainWindow);
            MW.Visibility = Visibility.Visible;
            this.Close();
        }

        private void BTN_ShowBooks_Click(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<BooksList>().Any())
            {
                Mouse.OverrideCursor = Cursors.Wait;
                BL = new BooksList
                {
                    Tag = "mdi_child"
                };
                BL.ShowDialog();
            }
        }

        private void BTN_AddBook_Click(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<BooksAdd>().Any())
            {
                BA = new BooksAdd
                {
                    Tag = "mdi_child"
                };
                BA.ShowDialog();
            }
        }

        private void BTN_DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<BooksDelete>().Any())
            {
                Mouse.OverrideCursor = Cursors.Wait;
                BD = new BooksDelete
                {
                    Tag = "mdi_child"
                };
                BD.ShowDialog();
            }
        }

        private void BTN_ChangeInfo_Click(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<BooksInfoUpdate>().Any())
            {
                Mouse.OverrideCursor = Cursors.Wait;
                BIU = new BooksInfoUpdate
                {
                    Tag = "mdi_child"
                };
                BIU.ShowDialog();
            }
        }

        public void WriteToConsole(string text)
        {
            // Writes down to this window's console the string that is being passed

            ConsoleText CS = new ConsoleText(text + "\n");
            SP_Console.Children.Add(CS);
        }
        private void BTN_FindStudent_Click(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<BooksFindStudent>().Any())
            {
                BFS = new BooksFindStudent
                {
                    Tag = "mdi_child"
                };
                BFS.ShowDialog();
            }
        }
    }
}
