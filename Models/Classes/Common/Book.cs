using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DigitalLibrary.Models.Classes.Useable;
using DigitalLibrary.Views.UserControls;
using DigitalLibrary.Views.Windows.Mains;
using DigitalLibrary.Models.Classes.Common;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using System.Threading;
using System.ComponentModel;
using Path = System.IO.Path;
using BooksWindow = DigitalLibrary.Views.Windows.Mains.BooksWindow;

namespace DigitalLibrary.Models.Classes.Common
{

    public class Book
    {
        private readonly TextFileHandler TFH = TextFileHandler.Instance;
        private static readonly Lazy<Book> b = new Lazy<Book>(() => new Book());
        public static Book Instance { get { return b.Value; } }
        BooksWindow BW;

        private List<string> Lines = new List<string>();

        public string BookName { get; set; }
        public string Genre { get; set; }
        public DateTime Published { get; set; }
        public string Publisher { get; set; }
        public string ImgURL { get; set; }
        public BitmapImage Img { get; set; }
        public int Quantity { get; set; }

        private Book() { }
        public Book(Book book)
        {
            this.BookName = book.BookName;
            this.Published = book.Published;
            this.Publisher = book.Publisher;
            this.ImgURL = TFH.GetImagePathFromFolder(Path.GetFileName(book.ImgURL));
            this.Genre = book.Genre;
            this.Quantity = book.Quantity;
        }
        public Book(string BookName, string Genre, DateTime? Published = null, string Publisher = null, string ImgURL = null, int Quantity = 1)
        {
            this.BookName = BookName;
            this.Published = Published ?? DateTime.Now;
            this.Publisher = Publisher;
            this.ImgURL = TFH.GetImagePathFromFolder(Path.GetFileName(ImgURL));
            this.Genre = Genre;
            this.Quantity = Quantity;

        }
        public Book(string BookName, string Genre, DateTime? Published = null, string Publisher = null, BitmapImage Img = null, int Quantity = 1)
        {
            this.BookName = BookName;
            this.Published = Published ?? DateTime.Now;
            this.Publisher = Publisher;
            this.Img = Img;
            this.Genre = Genre;
            this.Quantity = Quantity;
        }

        public void AddBook(Book newBook)
        {
            BW = Application.Current.Windows.OfType<BooksWindow>().First();
            UpdateBooksLinesList();
            if (!TFH.CheckLineExistens($"שם:{newBook.BookName}", Paths.booksFile))
            {
                Lines.Add($"שם:{newBook.BookName}\n[");
                Lines.Add($"ז'אנר:{newBook.Genre}");
                Lines.Add($"הוצל\"א:{newBook.Published.ToShortDateString()}");
                Lines.Add($"מוצל\"א:{newBook.Publisher}");
                Lines.Add($"תמונה:{newBook.ImgURL}");
                Lines.Add($"כמות:{newBook.Quantity}\n]");
                BW.WriteToConsole("נוסף הספר - " + newBook.BookName);
            }
            else if (!TFH.CheckLineExistens($"מוצל\"א:{newBook.Publisher}", Paths.booksFile))
            {
                Lines.Add($"שם:{newBook.BookName}\n[");
                Lines.Add($"ז'אנר:{newBook.Genre}");
                Lines.Add($"הוצל\"א:{newBook.Published.ToShortDateString()}");
                Lines.Add($"מוצל\"א:{newBook.Publisher}");
                Lines.Add($"תמונה:{newBook.ImgURL}");
                Lines.Add($"כמות:{newBook.Quantity}\n]");
                BW.WriteToConsole("נוסף הספר - " + newBook.BookName);
            }
            else
                BW.WriteToConsole($"הספר {newBook.BookName} כבר קיים במערכת");
            TFH.OverrideFile(Lines, Paths.booksFile);
            UpdateBooksLinesList();
        }

        public void DeleteBook(Book book)
        {
            if (TFH.CheckLineExistens($"שם:{book.BookName}", Paths.booksFile))
            {
                bool inMid = false;
                List<string> newList = new List<string>();
                foreach (string line in TFH.GetAllRawLines(Paths.booksFile))
                {
                    if (line.Equals($"שם:{book.BookName}"))
                    {
                        inMid = true;
                    }

                    if (inMid)
                    {
                        if (line.Equals(']'))
                        {
                            inMid = false;
                            continue;
                        }
                    }
                    else
                        newList.Add(line);
                }
                TFH.OverrideFile(newList, Paths.booksFile);
                UpdateBooksLinesList();
            }
        }
        public void DeleteBook(string bookName)
        {
            if (TFH.CheckLineExistens($"שם:{bookName}", Paths.booksFile))
            {
                bool inMid = false, studentHasBook = false, sinMid = false;
                Student s = Student.Instance;
                s.UpdateStudentsLinesList();
                s.Lines.ForEach( line => {
                    if (line.Equals($"שם הספר:{bookName}"))
                        studentHasBook = true;
                });
                List<string> newList = new List<string>(), UpdatingStudentsBooks = new List<string>();
                if (studentHasBook)
                {
                    foreach (var innerLine in s.Lines)
                    {
                        if (innerLine.IndexOf($"שם הספר:{bookName}") >= 0)
                        {
                            sinMid = true;
                            continue;
                        }
                        if (sinMid)
                        {
                            if (innerLine.Contains("תאריך השאלה:"))
                            {
                                sinMid = false;
                                continue;
                            }
                        }
                        else
                            UpdatingStudentsBooks.Add(innerLine);
                    }
                    TFH.OverrideFile(UpdatingStudentsBooks,Paths.studentsFile);
                }
                foreach (string line in TFH.GetAllRawLines(Paths.booksFile))
                {
                    if (line.Equals($"שם:{bookName}"))
                        inMid = true;

                    if (inMid)
                    {
                        if (line.Equals("]"))
                        {
                            inMid = false;
                            continue;
                        }
                    }
                    else
                        newList.Add(line);
                }
                TFH.OverrideFile(newList, Paths.booksFile);
                UpdateBooksLinesList();
            }
        }

        public void UpdateInfo(Book preBook, Book newBook)
        {
            foreach (var line in TFH.GetAllRawLines(Paths.studentsFile))
            {
                if (line.Equals($"שם הספר:{preBook.BookName}"))
                    TFH.OverrideLine($"שם הספר:{preBook.BookName}",
                        $"שם הספר:{newBook.BookName}",
                        Paths.studentsFile);
            }
            if (TFH.CheckLineExistens($"שם:{preBook.BookName}", Paths.booksFile))
            {
                bool inMid = false, bookAdded = false;
                List<string> newList = new List<string>();
                foreach (string line in TFH.GetAllRawLines(Paths.booksFile))
                {
                    if (line.Contains($"שם:{preBook.BookName}"))
                        inMid = true;

                    if (inMid)
                    {
                        if (!bookAdded)
                        {
                            newList.AddRange(GetNewBook(newBook));
                            bookAdded = true;
                        }
                        if (line.Equals(']'))
                        {
                            inMid = false;
                            continue;
                        }
                    }
                    else
                        newList.Add(line);
                }
                TFH.OverrideFile(newList, Paths.booksFile);
                UpdateBooksLinesList();
            }
        }

        public List<string> GetNewBook(Book newBook)
        {
            UpdateBooksLinesList();
            List<string> newLines = new List<string>();
            if (!TFH.CheckLineExistens($"שם:{newBook.BookName}", Paths.booksFile))
            {
                newLines.Add($"שם:{newBook.BookName}\n[");
                newLines.Add($"ז'אנר:{newBook.Genre}");
                newLines.Add($"הוצל\"א:{newBook.Published.ToShortDateString()}");
                newLines.Add($"מוצל\"א:{newBook.Publisher}");
                newLines.Add($"תמונה:{newBook.ImgURL}");
                newLines.Add($"כמות:{newBook.Quantity}\n]");
            }
            else if (!TFH.CheckLineExistens($"ז'אנר:{newBook.Genre}", Paths.booksFile))
            {
                newLines.Add($"שם:{newBook.BookName}\n[");
                newLines.Add($"ז'אנר:{newBook.Genre}");
                newLines.Add($"הוצל\"א:{newBook.Published.ToShortDateString()}");
                newLines.Add($"מוצל\"א:{newBook.Publisher}");
                newLines.Add($"תמונה:{newBook.ImgURL}");
                newLines.Add($"כמות:{newBook.Quantity}\n]");
            }
            else if (!TFH.CheckLineExistens($"הוצל\"א:{newBook.Published}", Paths.booksFile))
            {
                newLines.Add($"שם:{newBook.BookName}\n[");
                newLines.Add($"ז'אנר:{newBook.Genre}");
                newLines.Add($"הוצל\"א:{newBook.Published.ToShortDateString()}");
                newLines.Add($"מוצל\"א:{newBook.Publisher}");
                newLines.Add($"תמונה:{newBook.ImgURL}");
                newLines.Add($"כמות:{newBook.Quantity}\n]");
            }
            else if (!TFH.CheckLineExistens($"הוצל\"מ:{newBook.Publisher}", Paths.booksFile))
            {
                newLines.Add($"שם:{newBook.BookName}\n[");
                newLines.Add($"ז'אנר:{newBook.Genre}");
                newLines.Add($"הוצל\"א:{newBook.Published.ToShortDateString()}");
                newLines.Add($"מוצל\"א:{newBook.Publisher}");
                newLines.Add($"תמונה:{newBook.ImgURL}");
                newLines.Add($"כמות:{newBook.Quantity}\n]");
            }
            else if (!TFH.CheckLineExistens($"תמונה:{newBook.ImgURL}", Paths.booksFile))
            {
                newLines.Add($"שם:{newBook.BookName}\n[");
                newLines.Add($"ז'אנר:{newBook.Genre}");
                newLines.Add($"הוצל\"א:{newBook.Published.ToShortDateString()}");
                newLines.Add($"מוצל\"א:{newBook.Publisher}");
                newLines.Add($"תמונה:{newBook.ImgURL}");
                newLines.Add($"כמות:{newBook.Quantity}\n]");
            }
            else if (!TFH.CheckLineExistens($"כמות:{newBook.Quantity}", Paths.booksFile))
            {
                newLines.Add($"שם:{newBook.BookName}\n[");
                newLines.Add($"ז'אנר:{newBook.Genre}");
                newLines.Add($"הוצל\"א:{newBook.Published.ToShortDateString()}");
                newLines.Add($"מוצל\"א:{newBook.Publisher}");
                newLines.Add($"תמונה:{newBook.ImgURL}");
                newLines.Add($"כמות:{newBook.Quantity}\n]");
            }
            return newLines;
        }
        public List<Book> GetBooksList()
        {
            List<Book> newList = new List<Book>();
            UpdateBooksLinesList();
            foreach (var line in Lines)
            {
                if (line.Contains("שם:"))
                    newList.Add(GetBookByInfo(line));
            }
            return newList;
        }
        public Book GetBookByInfo(string info)
        {
            string BookName = null, Publisher = null, ImgURL = null, genre = null, infoHolder = (string)info;
            DateTime Published = new DateTime(1, 1, 1);
            int Quantity = 1;
            bool inMid = false;
            UpdateBooksLinesList();
            if (info.ToString().Contains("שם:"))
            {
                foreach (var line in Lines)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line.Equals(infoHolder) & !inMid)
                        {
                            inMid = true;
                            BookName = line.Substring(3);
                        }
                        if (inMid)
                        {
                            if (line.Contains("ז'אנר:"))
                                genre = line.Substring(6);
                            else if (line.Contains("הוצל\"א:"))
                                Published = DateTime.Parse(DateTime.Parse(line.Substring(7)).ToShortDateString());
                            else if (line.Contains("מוצל\"א:"))
                                Publisher = line.Substring(7);
                            else if (line.Contains("תמונה:"))
                                ImgURL = line.Substring(6);
                            else if (line.Contains("כמות:"))
                            {
                                Quantity = Int32.Parse(line.Substring(5));
                                break;
                            }
                        }
                    }
                }
                return new Book(BookName, genre, Published, Publisher, ImgURL, Quantity);
            }
            else if (info.ToString().Contains("ז'אנר:"))
            {
                foreach (var line in Lines)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line.Equals(infoHolder) & !inMid)
                        {
                            inMid = true;
                            genre = line.Substring(5);
                        }
                        if (inMid)
                        {
                            foreach (var inerline in Lines.Skip(TFH.GetSpecificLineNum(infoHolder, Paths.booksFile) - 2))
                            {
                                if (inerline.Contains("שם:"))
                                    genre = inerline.Substring(3);
                                else if (inerline.Contains("ז'אנר:"))
                                    genre = inerline.Substring(6);
                                else if (inerline.Contains("הוצל\"א:"))
                                    Published = DateTime.Parse(DateTime.Parse(inerline.Substring(7)).ToShortDateString());
                                else if (inerline.Contains("מוצל\"א:"))
                                    Publisher = inerline.Substring(7);
                                else if (inerline.Contains("תמונה:"))
                                    ImgURL = inerline.Substring(6);
                                else if (inerline.Contains("כמות:"))
                                {
                                    Quantity = Int32.Parse(line.Substring(5));
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else if (info.ToString().Contains("הוצל\"א:"))
            {
                foreach (var line in Lines)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line.Equals(infoHolder) & !inMid)
                        {
                            inMid = true;
                            genre = line.Substring(5);
                        }
                        if (inMid)
                        {
                            foreach (var inerline in Lines.Skip(TFH.GetSpecificLineNum(infoHolder, Paths.booksFile) - 3))
                            {
                                if (inerline.Contains("שם:"))
                                    genre = inerline.Substring(3);
                                else if (inerline.Contains("ז'אנר:"))
                                    genre = inerline.Substring(6);
                                else if (inerline.Contains("הוצל\"א:"))
                                    Published = DateTime.Parse(DateTime.Parse(inerline.Substring(7)).ToShortDateString());
                                else if (inerline.Contains("מוצל\"א:"))
                                    Publisher = inerline.Substring(7);
                                else if (inerline.Contains("תמונה:"))
                                    ImgURL = inerline.Substring(6);
                                else if (inerline.Contains("כמות:"))
                                {
                                    Quantity = Int32.Parse(line.Substring(5));
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else if (info.ToString().Contains("מוצל\"א:"))
            {
                foreach (var line in Lines)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line.Equals(infoHolder) & !inMid)
                        {
                            inMid = true;
                            genre = line.Substring(5);
                        }
                        if (inMid)
                        {
                            foreach (var inerline in Lines.Skip(TFH.GetSpecificLineNum(infoHolder, Paths.booksFile) - 4))
                            {
                                if (inerline.Contains("שם:"))
                                    genre = inerline.Substring(3);
                                else if (inerline.Contains("ז'אנר:"))
                                    genre = inerline.Substring(6);
                                else if (inerline.Contains("הוצל\"א:"))
                                    Published = DateTime.Parse(DateTime.Parse(inerline.Substring(7)).ToShortDateString());
                                else if (inerline.Contains("מוצל\"א:"))
                                    Publisher = inerline.Substring(7);
                                else if (inerline.Contains("תמונה:"))
                                    ImgURL = inerline.Substring(6);
                                else if (inerline.Contains("כמות:"))
                                {
                                    Quantity = Int32.Parse(line.Substring(5));
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else if (info.ToString().Contains("תמונה:"))
            {
                foreach (var line in Lines)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line.Equals(infoHolder) & !inMid)
                        {
                            inMid = true;
                            genre = line.Substring(5);
                        }
                        if (inMid)
                        {
                            foreach (var inerline in Lines.Skip(TFH.GetSpecificLineNum(infoHolder, Paths.booksFile) - 5))
                            {
                                if (inerline.Contains("שם:"))
                                    genre = inerline.Substring(3);
                                else if (inerline.Contains("ז'אנר:"))
                                    genre = inerline.Substring(6);
                                else if (inerline.Contains("הוצל\"א:"))
                                    Published = DateTime.Parse(DateTime.Parse(inerline.Substring(7)).ToShortDateString());
                                else if (inerline.Contains("מוצל\"א:"))
                                    Publisher = inerline.Substring(7);
                                else if (inerline.Contains("תמונה:"))
                                    ImgURL = inerline.Substring(6);
                                else if (inerline.Contains("כמות:"))
                                {
                                    Quantity = Int32.Parse(line.Substring(5));
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else if (info.ToString().Contains("כמות:"))
            {
                foreach (var line in Lines)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line.Equals(infoHolder) & !inMid)
                        {
                            inMid = true;
                            genre = line.Substring(5);
                        }
                        if (inMid)
                        {
                            foreach (var inerline in Lines.Skip(TFH.GetSpecificLineNum(infoHolder, Paths.booksFile) - 6))
                            {
                                if (inerline.Contains("שם:"))
                                    genre = inerline.Substring(3);
                                else if (inerline.Contains("ז'אנר:"))
                                    genre = inerline.Substring(6);
                                else if (inerline.Contains("הוצל\"א:"))
                                    Published = DateTime.Parse(DateTime.Parse(inerline.Substring(7)).ToShortDateString());
                                else if (inerline.Contains("מוצל\"א:"))
                                    Publisher = inerline.Substring(7);
                                else if (inerline.Contains("תמונה:"))
                                    ImgURL = inerline.Substring(6);
                                else if (inerline.Contains("כמות:"))
                                {
                                    Quantity = Int32.Parse(line.Substring(5));
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        private void UpdateBooksLinesList() { Lines = TFH.GetAllRawLines(Paths.booksFile); }
    }
}