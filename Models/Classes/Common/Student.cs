using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Windows.Shapes;
using DigitalLibrary.Models.Classes.Useable;
using DigitalLibrary.Views.Windows.Mains.Students;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace DigitalLibrary.Models.Classes.Common
{
    public class Student
    {
        private readonly TextFileHandler TFH = TextFileHandler.Instance;
        public List<string> Lines { get; private set; } = new List<string>();
        private static readonly Lazy<Student> student = new Lazy<Student>(() => new Student());
        public static Student Instance { get { return student.Value; } }
        private Book b = Book.Instance;

        private StudentsWindow SW;

        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNum { get; set; }
        public List<BookBorrowed> BorrowedBooks { get; set; }

        private Student() { }
        private Student(int id, string name, string phoneNum, List<BookBorrowed> borrowedBooks)
        {
            Id = id;
            Name = name;
            PhoneNum = phoneNum;
            BorrowedBooks = borrowedBooks;
        }
        public Student(string Name, string PhoneNum)
        {
            Id = GenerateID();
            this.Name = Name;
            this.PhoneNum = PhoneNum;
            BorrowedBooks = new List<BookBorrowed>();
        }
        public Student(string Name, string PhoneNum, List<BookBorrowed> BorrowedBooks)
        {
            Id = GenerateID();
            this.Name = Name;
            this.PhoneNum = PhoneNum;
            this.BorrowedBooks = BorrowedBooks;
        }

        public void AddStudent(Student newStudent)
        {
            SW = Application.Current.Windows.OfType<StudentsWindow>().First();
            UpdateStudentsLinesList();
            if (!TFH.CheckLineExistens($"שם:{newStudent.Name}", Paths.studentsFile) ||
                !TFH.CheckLineExistens($"טלפון:{newStudent.PhoneNum}", Paths.studentsFile))
            {
                Lines.Add($"מ''ס:{GenerateID()}\n[");
                Lines.Add($"שם:{newStudent.Name}");
                Lines.Add($"טלפון:{newStudent.PhoneNum}");
                Lines.Add($"ספרים מושאלים");
                Lines.Add(@"{");
                foreach (var book in newStudent.BorrowedBooks)
                {
                    Lines.Add($"שם הספר:{book.BookName}");
                    Lines.Add($"תאריך השאלה:{DateTime.Now.ToShortDateString()}");
                }
                Lines.Add(@"}");
                Lines.Add("]");
                SW.WriteToConsole($"התלמיד {newStudent.Name} נוסף בהצלחה ");
            }
            else
            {
                SW.WriteToConsole($"התלמיד {newStudent.Name} כבר קיים במערכת ");
            }
            TFH.OverrideFile(Lines, Paths.studentsFile);
        }
        public void AddStudentWithoutOuput(Student newStudent)
        {
            SW = Application.Current.Windows.OfType<StudentsWindow>().First();
            UpdateStudentsLinesList();
            if (!TFH.CheckLineExistens($"שם:{newStudent.Name}", Paths.studentsFile) ||
                !TFH.CheckLineExistens($"טלפון:{newStudent.PhoneNum}", Paths.studentsFile))
            {
                Lines.Add($"מ''ס:{GenerateID()}\n[");
                Lines.Add($"שם:{newStudent.Name}");
                Lines.Add($"טלפון:{newStudent.PhoneNum}");
                Lines.Add($"ספרים מושאלים");
                Lines.Add(@"{");
                foreach (var book in newStudent.BorrowedBooks)
                {
                    Lines.Add($"שם הספר:{book.BookName}");
                    Lines.Add($"תאריך השאלה:{DateTime.Now.ToShortDateString()}");
                }
                Lines.Add(@"}");
                Lines.Add("]");
            }
            TFH.OverrideFile(Lines, Paths.studentsFile);
        }
        public void AddBookToStudent(Book newBook, Student student)
        {
            SW = Application.Current.Windows.OfType<StudentsWindow>().First();
            bool bookExist = false;
            student.BorrowedBooks.ForEach(bookName => {
                if (bookName.Equals(newBook.BookName))
                    bookExist = true;
            });
            if (
                !bookExist &
                newBook.Quantity != 0)
            {
                student.BorrowedBooks.Add(new BookBorrowed(newBook.BookName));
                StudentWithUpdatedBooks(student);
                Book bookWithLowerQuantity = new Book(newBook);
                bookWithLowerQuantity.Quantity--;
                b.UpdateInfo(newBook, bookWithLowerQuantity);
                SW.WriteToConsole($"הספר {newBook.BookName} נוסף בהצלחה לתלמיד {student.Name}");
                SW.WriteToConsole($"כמות הספרים הנותרים מהספר {newBook.BookName}: {bookWithLowerQuantity.Quantity}");
            }
            else
            {
                if(bookExist)
                    SW.WriteToConsole($"הספר {newBook.BookName} כבר קיים אצל התלמיד {student.Name}");
                else if(newBook.Quantity == 0)
                    SW.WriteToConsole($"אין עוד מספר זה בסיפרייה. כמות הספרים מהספר {newBook.BookName}: {newBook.Quantity--}");
            }
        }

        public void DeleteStudent(Student student)
        {
            UpdateStudentsLinesList();
            if (TFH.CheckLineExistens($"שם:{student.Name}", Paths.studentsFile) &
                TFH.CheckLineExistens($"טלפון:{student.PhoneNum}", Paths.studentsFile))
            {
                if (student.BorrowedBooks.Count != 0)
                {
                    List<Book> booksStudentHave = new List<Book>(), booksToReturn = new List<Book>();
                    student.BorrowedBooks.ForEach(bookName => {
                        booksStudentHave.Add(b.GetBookByInfo($"שם:{bookName}"));
                    });
                    booksStudentHave.ForEach(book => {
                        Book bHolder = new Book(book);
                        bHolder.Quantity += book.Quantity;
                        b.UpdateInfo(book,bHolder);
                    });
                }
                bool inMidName = false, inMidPhone = false, insindMide = false;
                List<string> newList = new List<string>();
                int idLine = new int(), currentline = 1;
                foreach (var line in Lines)
                {
                    if (line.Equals($"שם:{student.Name}"))
                        inMidName = true;
                    if (line.Equals($"טלפון:{student.PhoneNum}"))
                        inMidPhone = true;
                    if (inMidName & inMidPhone)
                    {
                        idLine = currentline - 4;
                        break;
                    }
                    currentline++;
                }
                int id = int.Parse(Lines[idLine].Substring(Lines[idLine].IndexOf(":")+1));
                foreach (var line in Lines)
                {
                    if (line.Equals($"מ''ס:{id}"))
                        insindMide = true;
                    if (insindMide)
                    {
                        if (line.Equals("]"))
                            insindMide = false;
                    }
                    else
                        newList.Add(line);
                }
                TFH.OverrideFile(newList, Paths.studentsFile);
            }
        }
        public void DeleteStudent(string name, string phoneNum)
        {
            UpdateStudentsLinesList();
            if (TFH.CheckLineExistens($"שם:{name}", Paths.studentsFile) &
                TFH.CheckLineExistens($"טלפון:{phoneNum}", Paths.studentsFile))
            {
                bool inMidName = false, inMidPhone = false, insindMide = false;
                List<string> newList = new List<string>();
                int idLine = new int(), currentline = 1;
                foreach (var line in Lines)
                {
                    if (line.Equals($"שם:{name}"))
                        inMidName = true;
                    if (line.Equals($"טלפון:{phoneNum}"))
                        inMidPhone = true;
                    if (inMidName & inMidPhone)
                    {
                        idLine = currentline - 4;
                        break;
                    }
                    currentline++;
                }
                int id = int.Parse(Lines[idLine].Substring(Lines[idLine].IndexOf(":") + 1));
                foreach (var line in Lines)
                {
                    if (line.Equals($"מ''ס:{id}"))
                        insindMide = true;
                    if (insindMide)
                    {
                        if (line.Equals("]"))
                            insindMide = false;
                    }
                    else
                        newList.Add(line);
                }
                TFH.OverrideFile(newList, Paths.studentsFile);
            }
        }

        public void StudentWithUpdatedBooks(Student student)
        {
            bool inMid = false;
            List<string> updatedList = new List<string>();
            foreach (var line in TFH.GetAllRawLines(Paths.studentsFile))
            {
                if (line.Equals($"מ''ס:{student.Id}"))
                    inMid = true;
                if (inMid)
                {
                    if (line.Equals("]"))
                        inMid = false;
                }
                else
                    updatedList.Add(line);
            }
            TFH.OverrideFile(updatedList, Paths.studentsFile);
            AddStudentWithoutOuput(student);
        }

        public void UpdateInfo(Student student,Student updatedStudent)
        {
            bool inMid = false;
            List<string> updatedList = new List<string>();
            foreach (var line in TFH.GetAllRawLines(Paths.studentsFile))
            {
                if (line.Equals($"מ''ס:{student.Id}"))
                    inMid = true;
                if (inMid)
                {
                    if (line.Equals("]"))
                        inMid = false;
                }
                else
                    updatedList.Add(line);
            }
            TFH.OverrideFile(updatedList,Paths.studentsFile);
            AddStudentWithoutOuput(updatedStudent);
        }

        private int GenerateID()
        {
            int newID = 0;
            foreach (string line in TFH.GetAllRawLines(Paths.studentsFile))
            {
                if (line.Contains("מ''ס:"))
                    newID++;
            }
            return newID;
        }

        public List<string> GetNewStudent(Student student)
        {
            UpdateStudentsLinesList();
            List<string> newLines = new List<string>();
            if (!TFH.CheckLineExistens($"שם:{student.Name}", Paths.studentsFile) &
                !TFH.CheckLineExistens($"טלפון:{student.PhoneNum}", Paths.studentsFile))
            {
                Lines.Add($"מ''ס:{student.Id}\n[");
                Lines.Add($"שם:{student.Name}");
                Lines.Add($"טלפון:{student.PhoneNum}");
                Lines.Add($"ספרים מושאלים");
                Lines.Add(@"{");
                foreach (var book in student.BorrowedBooks)
                {
                    Lines.Add($"שם הספר:{book.BookName}");
                    Lines.Add($"תאריך השאלה:{DateTime.Now.ToShortDateString()}");
                }
                Lines.Add(@"}");
                Lines.Add("]");
            }
            return newLines;
        }
        public List<Student> GetStudentList()
        {
            List<Student> newList = new List<Student>();
            foreach (var line in TFH.GetAllRawLines(Paths.studentsFile))
            {
                if (line.Contains("מ''ס"))
                    newList.Add(GetStudentByInfo(line));
            }
            return newList;
        }
        public Student GetStudentByInfo(string info)
        {
            string name = null, phoneNum = null,BB_name = null;
            int id = new int();
            DateTime BB_lent;
            List<BookBorrowed> borrowedBooks = new List<BookBorrowed>();
            bool inMid = false, inMidBooks = false, insideBook = false;
            if (info.Contains("מ''ס"))
            {
                foreach (var line in TFH.GetAllRawLines(Paths.studentsFile))
                {
                    if (line.Equals(info))
                    {
                        inMid = true;
                        id = int.Parse(line.Substring(line.IndexOf(":") + 1));
                    }
                    if (inMid)
                    {
                        if (line.Equals("]"))
                            break;
                        else if (line.Contains("שם:"))
                            name = line.Substring(line.IndexOf(":") + 1);
                        else if (line.Contains("טלפון:"))
                            phoneNum = line.Substring(line.IndexOf(":") + 1);
                        else if (line.Equals("{"))
                        {
                            inMidBooks = true;
                            continue;
                        }
                        else if (line.Equals("}"))
                            inMidBooks = false;
                        if (inMidBooks)
                        {
                            if (line.Contains("שם הספר:"))
                            {
                                BB_name = line.Substring(line.IndexOf(":") + 1);
                                insideBook = true;
                                continue;
                            }
                            if (insideBook)
                            {
                                BB_lent = DateTime.Parse(line.Substring(line.IndexOf(":")+1));
                                borrowedBooks.Add(new BookBorrowed(BB_name,BB_lent));
                                insideBook = false;
                            }

                        }
                    }
                }
                return new Student(id, name, phoneNum, borrowedBooks);
            }
            return null;
        }

        public void UpdateStudentsLinesList() { Lines = TFH.GetAllRawLines(Paths.studentsFile); }

        public BookBorrowed GetBorrowedBookInfo(Student student,string borrowedBookName)
        {
            Student currentStudent = GetStudentByInfo($"מ''ס:{student.Id}");
            foreach (var book in currentStudent.BorrowedBooks)
            {
                if (book.BookName.Equals(borrowedBookName))
                    return new BookBorrowed(book.BookName, book.BookLendDate);
            }
            return null;
        }
    }
}