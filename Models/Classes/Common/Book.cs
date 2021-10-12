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
        private readonly MainFilesHandler MFH = MainFilesHandler.Instance; // Used for a Singleton Design
        private static readonly Lazy<Book> b = new Lazy<Book>(() => new Book()); // Used for a Singleton Design
        public static Book Instance { get { return b.Value; } } // Used for a Singleton Design
        BooksWindow BW; // Used for BW.WriteToConsole(...)

        private List<string> Lines = new List<string>(); // Holdes the GetAllRawLines for faster use

        public string BookName { get; set; } // The Book's Name - Mendatory
        public string Genre { get; set; } // The Genre - Mendatory
        public DateTime Published { get; set; } // The date which the book was published at
        public string Author { get; set; } // The Author's name
        public string ImgURL { get; set; } // Holdes a Url for the image to be displayed
        public BitmapImage Img { get; set; } // Holdes a BitmapImage, usually used for external use in an actual need of a Image
        public int Quantity { get; set; } // The quantity of this book in the library
        private Book() { } // Used for a Singleton Design
        public Book(Book book) 
        {
            //ctor

            this.BookName = book.BookName;
            this.Published = book.Published;
            this.Author = book.Author;
            this.ImgURL = MFH.GetImagePathFromFolder(Path.GetFileName(book.ImgURL));
            this.Genre = book.Genre;
            this.Quantity = book.Quantity;
        }
        public Book(string BookName, string Genre, DateTime? Published = null, string Author = null, string ImgURL = null, int Quantity = 1) 
        {
            //ctor

            this.BookName = BookName;
            this.Published = Published ?? DateTime.Now;
            this.Author = Author;
            this.ImgURL = MFH.GetImagePathFromFolder(Path.GetFileName(ImgURL));
            this.Genre = Genre;
            this.Quantity = Quantity;

        }
        public Book(string BookName, string Genre, DateTime? Published = null, string Author = null, BitmapImage Img = null, int Quantity = 1) 
        {
            //ctor

            this.BookName = BookName;
            this.Published = Published ?? DateTime.Now;
            this.Author = Author;
            this.Img = Img;
            this.Genre = Genre;
            this.Quantity = Quantity;
        }
        public void AddBook(Book newBook)
        {
            // After receiving a new book to add, checks if any other book has the same name,genre and author
            // Only if non has the same values, will add the book to the text file

            BW = Application.Current.Windows.OfType<BooksWindow>().First(); // Inorder to WriteToConsole
            UpdateBooksLinesList();
            if (!MFH.CheckLineExistens($"שם:{newBook.BookName}", Paths.booksFile) |
                !MFH.CheckLineExistens($"ז'אנר:{newBook.Genre}", Paths.booksFile) |
                !MFH.CheckLineExistens($"מחבר:{newBook.Author}", Paths.booksFile))
            {
                Lines.Add($"שם:{newBook.BookName}\n["); // name
                Lines.Add($"ז'אנר:{newBook.Genre}"); // genre
                Lines.Add($"הוצל\"א:{newBook.Published.ToShortDateString()}"); // published
                Lines.Add($"מחבר:{newBook.Author}"); // author
                Lines.Add($"תמונה:{newBook.ImgURL}"); // imgurl
                Lines.Add($"כמות:{newBook.Quantity}\n]"); // quantity
                BW.WriteToConsole("נוסף הספר - " + newBook.BookName); // printing to the console that the book has been added
            }
            else
                MessageBox.Show($"הספר {newBook.BookName} כבר קיים במערכת"); // the book is already in the system
            MFH.OverrideFile(Lines, Paths.booksFile);
            UpdateBooksLinesList();
        }
        public void DeleteBook(Book book)
        {
            // When deleting a Book, the function will firstly go through all students
            // And if one of them borrowed the book, it will delete it from him

            if (MFH.CheckLineExistens($"שם:{book.BookName}", Paths.booksFile))
            {
                // This part is the one that removes the book from all the students

                bool inMid = false, studentHasBook = false, sinMid = false;
                Student s = Student.Instance;
                s.UpdateStudentsLinesList();
                s.Lines.ForEach(line => {
                    if (line.Equals($"שם הספר:{book.BookName}"))
                        studentHasBook = true;
                });
                List<string> newList = new List<string>(), UpdatingStudentsBooks = new List<string>();
                if (studentHasBook)
                {
                    foreach (var innerLine in s.Lines)
                    {
                        if (innerLine.IndexOf($"שם הספר:{book.BookName}") >= 0)
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
                    MFH.OverrideFile(UpdatingStudentsBooks, Paths.studentsFile);
                }

                foreach (string line in MFH.GetAllRawLines(Paths.booksFile))
                {
                    // This is part where the book is actually being removed

                    if (line.Equals($"שם:{book.BookName}"))
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
                MFH.OverrideFile(newList, Paths.booksFile);
                UpdateBooksLinesList();
            }
        }
        public void DeleteBook(string bookName)
        {
            // When deleting a Book, the function will firstly go through all students
            // And if one of them borrowed the book, it will delete it from him

            if (MFH.CheckLineExistens($"שם:{bookName}", Paths.booksFile))
            {
                // This part is the one that removes the book from all the students

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
                    MFH.OverrideFile(UpdatingStudentsBooks,Paths.studentsFile);
                }

                foreach (string line in MFH.GetAllRawLines(Paths.booksFile))
                {
                    // This is part where the book is actually being removed

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
                MFH.OverrideFile(newList, Paths.booksFile);
                UpdateBooksLinesList();
            }
        }
        public void UpdateInfo(Book preBook, Book newBook)
        {
            // The function recives two books, the one that you want to change, and the book that you change it for
            // (Usually it is just the same book with updated information)

            foreach (var line in MFH.GetAllRawLines(Paths.studentsFile))
            {
                if (line.Equals($"שם הספר:{preBook.BookName}"))
                    MFH.OverrideAllLine($"שם הספר:{newBook.BookName}",
                        $"שם הספר:{preBook.BookName}",
                        Paths.studentsFile);
            }
            if (MFH.CheckLineExistens($"שם:{preBook.BookName}", Paths.booksFile))
            {
                bool inMid = false, bookAdded = false;
                List<string> newList = new List<string>();
                foreach (string line in MFH.GetAllRawLines(Paths.booksFile))
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
                        if (line.Equals("]"))
                        {
                            inMid = false;
                            continue;
                        }
                    }
                    else
                        newList.Add(line);
                }
                MFH.OverrideFile(newList, Paths.booksFile);
                UpdateBooksLinesList();
            }
        }
        private void UpdateBooksLinesList() { Lines = MFH.GetAllRawLines(Paths.booksFile); } // Updates the Lines list with the books file lines
        public List<string> GetNewBook(Book newBook)
        {
            // Returns a List<string> which contains the book that is passed to it
            // (Pasing a book inside will result in a return of the information of that book)

            UpdateBooksLinesList();
            List<string> newLines = new List<string>();
            if (!MFH.CheckLineExistens($"שם:{newBook.BookName}", Paths.booksFile))
            {
                newLines.Add($"שם:{newBook.BookName}\n[");
                newLines.Add($"ז'אנר:{newBook.Genre}");
                newLines.Add($"הוצל\"א:{newBook.Published.ToShortDateString()}");
                newLines.Add($"מחבר:{newBook.Author}");
                newLines.Add($"תמונה:{newBook.ImgURL}");
                newLines.Add($"כמות:{newBook.Quantity}\n]");
            }
            else if (!MFH.CheckLineExistens($"ז'אנר:{newBook.Genre}", Paths.booksFile))
            {
                newLines.Add($"שם:{newBook.BookName}\n[");
                newLines.Add($"ז'אנר:{newBook.Genre}");
                newLines.Add($"הוצל\"א:{newBook.Published.ToShortDateString()}");
                newLines.Add($"מחבר:{newBook.Author}");
                newLines.Add($"תמונה:{newBook.ImgURL}");
                newLines.Add($"כמות:{newBook.Quantity}\n]");
            }
            else if (!MFH.CheckLineExistens($"הוצל\"א:{newBook.Published}", Paths.booksFile))
            {
                newLines.Add($"שם:{newBook.BookName}\n[");
                newLines.Add($"ז'אנר:{newBook.Genre}");
                newLines.Add($"הוצל\"א:{newBook.Published.ToShortDateString()}");
                newLines.Add($"מחבר:{newBook.Author}");
                newLines.Add($"תמונה:{newBook.ImgURL}");
                newLines.Add($"כמות:{newBook.Quantity}\n]");
            }
            else if (!MFH.CheckLineExistens($"מחבר:{newBook.Author}", Paths.booksFile))
            {
                newLines.Add($"שם:{newBook.BookName}\n[");
                newLines.Add($"ז'אנר:{newBook.Genre}");
                newLines.Add($"הוצל\"א:{newBook.Published.ToShortDateString()}");
                newLines.Add($"מחבר:{newBook.Author}");
                newLines.Add($"תמונה:{newBook.ImgURL}");
                newLines.Add($"כמות:{newBook.Quantity}\n]");
            }
            else if (!MFH.CheckLineExistens($"תמונה:{newBook.ImgURL}", Paths.booksFile))
            {
                newLines.Add($"שם:{newBook.BookName}\n[");
                newLines.Add($"ז'אנר:{newBook.Genre}");
                newLines.Add($"הוצל\"א:{newBook.Published.ToShortDateString()}");
                newLines.Add($"מחבר:{newBook.Author}");
                newLines.Add($"תמונה:{newBook.ImgURL}");
                newLines.Add($"כמות:{newBook.Quantity}\n]");
            }
            else if (!MFH.CheckLineExistens($"כמות:{newBook.Quantity}", Paths.booksFile))
            {
                newLines.Add($"שם:{newBook.BookName}\n[");
                newLines.Add($"ז'אנר:{newBook.Genre}");
                newLines.Add($"הוצל\"א:{newBook.Published.ToShortDateString()}");
                newLines.Add($"מחבר:{newBook.Author}");
                newLines.Add($"תמונה:{newBook.ImgURL}");
                newLines.Add($"כמות:{newBook.Quantity}\n]");
            }
            return newLines;
        }
        public List<Book> GetBooksList()
        {
            // Return a list of all the books

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
            // Return the first book which contains the given information

            string BookName = null, Author = null, ImgURL = null, genre = null, infoHolder = (string)info;
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
                            else if (line.Contains("מחבר:"))
                                Author = line.Substring(5);
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
                return new Book(BookName, genre, Published, Author, ImgURL, Quantity);
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
                            foreach (var inerline in Lines.Skip(MFH.GetSpecificLineNum(infoHolder, Paths.booksFile) - 2))
                            {
                                if (inerline.Contains("שם:"))
                                    genre = inerline.Substring(3);
                                else if (inerline.Contains("ז'אנר:"))
                                    genre = inerline.Substring(6);
                                else if (inerline.Contains("הוצל\"א:"))
                                    Published = DateTime.Parse(DateTime.Parse(inerline.Substring(7)).ToShortDateString());
                                else if (inerline.Contains("מחבר:"))
                                    Author = inerline.Substring(5);
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
                            foreach (var inerline in Lines.Skip(MFH.GetSpecificLineNum(infoHolder, Paths.booksFile) - 3))
                            {
                                if (inerline.Contains("שם:"))
                                    genre = inerline.Substring(3);
                                else if (inerline.Contains("ז'אנר:"))
                                    genre = inerline.Substring(6);
                                else if (inerline.Contains("הוצל\"א:"))
                                    Published = DateTime.Parse(DateTime.Parse(inerline.Substring(7)).ToShortDateString());
                                else if (inerline.Contains("מחבר:"))
                                    Author = inerline.Substring(5);
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
            else if (info.ToString().Contains("מחבר:"))
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
                            foreach (var inerline in Lines.Skip(MFH.GetSpecificLineNum(infoHolder, Paths.booksFile) - 4))
                            {
                                if (inerline.Contains("שם:"))
                                    genre = inerline.Substring(3);
                                else if (inerline.Contains("ז'אנר:"))
                                    genre = inerline.Substring(6);
                                else if (inerline.Contains("הוצל\"א:"))
                                    Published = DateTime.Parse(DateTime.Parse(inerline.Substring(7)).ToShortDateString());
                                else if (inerline.Contains("מחבר:"))
                                    Author = inerline.Substring(5);
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
                            foreach (var inerline in Lines.Skip(MFH.GetSpecificLineNum(infoHolder, Paths.booksFile) - 5))
                            {
                                if (inerline.Contains("שם:"))
                                    genre = inerline.Substring(3);
                                else if (inerline.Contains("ז'אנר:"))
                                    genre = inerline.Substring(6);
                                else if (inerline.Contains("הוצל\"א:"))
                                    Published = DateTime.Parse(DateTime.Parse(inerline.Substring(7)).ToShortDateString());
                                else if (inerline.Contains("מחבר:"))
                                    Author = inerline.Substring(5);
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
                            foreach (var inerline in Lines.Skip(MFH.GetSpecificLineNum(infoHolder, Paths.booksFile) - 6))
                            {
                                if (inerline.Contains("שם:"))
                                    genre = inerline.Substring(3);
                                else if (inerline.Contains("ז'אנר:"))
                                    genre = inerline.Substring(6);
                                else if (inerline.Contains("הוצל\"א:"))
                                    Published = DateTime.Parse(DateTime.Parse(inerline.Substring(7)).ToShortDateString());
                                else if (inerline.Contains("מחבר:"))
                                    Author = inerline.Substring(5);
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
    }
}