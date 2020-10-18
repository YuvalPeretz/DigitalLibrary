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
        private readonly MainFilesHandler MFH = MainFilesHandler.Instance;
        public List<string> Lines { get; private set; } = new List<string>(); // Holdes the GetAllRawLines for faster use
        private static readonly Lazy<Student> student = new Lazy<Student>(() => new Student()); // Used for a Singleton Design
        public static Student Instance { get { return student.Value; } } // Used for a Singleton Design
        private Book b = Book.Instance;

        private StudentsWindow SW; // Used for SW.WriteToConsole(...)

        public int Id { get; set; } // Every student has an Id
        public string Name { get; set; } // Represnts the name of the student 
        public string PhoneNum { get; set; } // Every student must have a phone number to contact
        public List<BookBorrowed> BorrowedBooks { get; set; } // Represents the Borrowed books that the student has

        private Student() { } // Used for a Singleton Design

        public Student(string Name, string PhoneNum)
        {
            // ctor

            Id = GenerateID();
            this.Name = Name;
            this.PhoneNum = PhoneNum;
            BorrowedBooks = new List<BookBorrowed>();
        }
        public Student(string Name, string PhoneNum, List<BookBorrowed> BorrowedBooks)
        {
            // ctor

            Id = GenerateID();
            this.Name = Name;
            this.PhoneNum = PhoneNum;
            this.BorrowedBooks = BorrowedBooks;
        }
        public Student(int id, string name, string phoneNum, List<BookBorrowed> borrowedBooks)
        {
            // ctor

            Id = id;
            Name = name;
            PhoneNum = phoneNum;
            BorrowedBooks = borrowedBooks;
        }
        public void AddStudent(Student newStudent)
        {
            // When adding a student, the function will firstly check if there is a student with the same
            // name and phone number
            // Note that it is posibble in a school to have 2 students with the same name, but it is impossible for these two students to have the same phone number

            SW = Application.Current.Windows.OfType<StudentsWindow>().First();
            UpdateStudentsLinesList();
            if (!MFH.CheckLineExistens($"שם:{newStudent.Name}", Paths.studentsFile) ||
                !MFH.CheckLineExistens($"טלפון:{newStudent.PhoneNum}", Paths.studentsFile))
            {
                Lines.Add($"מ''ס:{newStudent.Id}\n[");
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
                SW.WriteToConsole($"התלמיד {newStudent.Name} נוסף בהצלחה "); // added successfully 
            }
            else
                MessageBox.Show($"התלמיד {newStudent.Name} כבר קיים במערכת "); // student is already in the system
            MFH.OverrideFile(Lines, Paths.studentsFile);
        }
        public void AddStudentWithoutOuput(Student newStudent)
        {
            // This function will add a student the same way as the one above it, only that this one will not output anything to the console

            SW = Application.Current.Windows.OfType<StudentsWindow>().First();
            UpdateStudentsLinesList();
            if (!MFH.CheckLineExistens($"שם:{newStudent.Name}", Paths.studentsFile) ||
                !MFH.CheckLineExistens($"טלפון:{newStudent.PhoneNum}", Paths.studentsFile))
            {
                Lines.Add($"מ''ס:{newStudent.Id}\n[");
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
            MFH.OverrideFile(Lines, Paths.studentsFile);
        }
        public void AddBookToStudent(Book newBook, Student student)
        {
            // This functions adds a book to a students
            // First it makes surre that the student do not have that book already and only then adds it
            // After that the book quantity will be decressed by the UpdateInfo method

            SW = Application.Current.Windows.OfType<StudentsWindow>().First();
            bool bookExist = false;
            Student studentHolder = student; // this holder is for a later use in the UpdateInfo function
            student.BorrowedBooks.ForEach(bookName =>
            {
                if (bookName.BookName.Equals(newBook.BookName))
                    bookExist = true;
            });
            if (
                !bookExist &
                newBook.Quantity != 0)
            {
                student.BorrowedBooks.Add(new BookBorrowed(newBook.BookName));
                UpdateInfo(studentHolder, student);
                Book bookWithLowerQuantity = new Book(newBook);
                bookWithLowerQuantity.Quantity--;
                b.UpdateInfo(newBook, bookWithLowerQuantity);
                SW.WriteToConsole($"הספר {newBook.BookName} נוסף בהצלחה לתלמיד {student.Name}"); // added successfully
                SW.WriteToConsole($"כמות הספרים הנותרים מהספר {newBook.BookName}: {bookWithLowerQuantity.Quantity}"); // prints the amount of books left from that type
            }
            else
            {
                if (bookExist)
                    MessageBox.Show($"הספר {newBook.BookName} כבר קיים אצל התלמיד {student.Name}"); // student already borrowed the book
                else if (newBook.Quantity == 0)
                    MessageBox.Show($"אין עוד מספר זה בסיפרייה. כמות הספרים מהספר {newBook.BookName}: {newBook.Quantity--}"); // there aren't any more books from that btype to borrow
            }
        }

        public void DeleteStudent(Student student)
        {
            // This function deletes students, but before deleting a student
            // In case he has any books borrowed, the function wil "return" the books to the library

            UpdateStudentsLinesList();
            if (MFH.CheckLineExistens($"שם:{student.Name}", Paths.studentsFile) &
                MFH.CheckLineExistens($"טלפון:{student.PhoneNum}", Paths.studentsFile))
            {
                // The part where the books are being returned to the library

                if (student.BorrowedBooks.Count != 0)
                {
                    List<Book> booksStudentHave = new List<Book>(), booksToReturn = new List<Book>();
                    student.BorrowedBooks.ForEach(bookName =>
                    {
                        booksStudentHave.Add(b.GetBookByInfo($"שם:{bookName.BookName}"));
                    });
                    booksStudentHave.ForEach(book =>
                    {
                        Book bHolder = new Book(book);
                        bHolder.Quantity++;
                        b.UpdateInfo(book, bHolder);
                    });
                }
                List<string> newList = new List<string>();
                bool inMidStudentToDelete = false;
                foreach (var line in MFH.GetAllRawLines(Paths.studentsFile))
                {
                    // The part where the student is actually removed

                    if (line.Equals($"מ''ס:{student.Id}"))
                    {
                        inMidStudentToDelete = true;
                        continue;
                    }
                    else if (line.Equals("]") & inMidStudentToDelete)
                    {
                        inMidStudentToDelete = false;
                        continue;
                    }
                    else if (inMidStudentToDelete)
                        continue;
                    else
                        newList.Add(line);
                }
                MFH.OverrideFile(newList, Paths.studentsFile);
            }
        }

        public void UpdateInfo(Student student, Student updatedStudent)
        {
            // This function updates an info of a student by receiving the original student and the updated version of him

            bool inMid = false;
            List<string> updatedList = new List<string>();
            foreach (var line in MFH.GetAllRawLines(Paths.studentsFile))
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
            updatedList.AddRange(GetNewStudent(updatedStudent));
            MFH.OverrideFile(updatedList, Paths.studentsFile);
        }

        private int GenerateID()
        {
            // When creating a student, this function will generate a new Id for him

            int newId = 0;
            foreach (string line in MFH.GetAllRawLines(Paths.studentsFile))
            {
                if (line.Contains("מ''ס:"))
                {
                    if (newId < int.Parse(line.Substring(line.IndexOf("מ''ס:") + 5)))
                        newId = int.Parse(line.Substring(line.IndexOf("מ''ס:") + 5)); // once reached the highest ID in the File, the newID will be equal to that.
                }
            }
            return newId + 1; // generates a new +1 ID;
        }

        public List<string> GetNewStudent(Student student)
        {
            // Returns a student by string lines as present in the text file
            // That is used for updating students in the UpdateInfo function

            List<string> newLines = new List<string>();
            newLines.Add($"מ''ס:{student.Id}\n[");
            newLines.Add($"שם:{student.Name}");
            newLines.Add($"טלפון:{student.PhoneNum}");
            newLines.Add($"ספרים מושאלים");
            newLines.Add(@"{");
            foreach (var book in student.BorrowedBooks)
            {
                newLines.Add($"שם הספר:{book.BookName}");
                newLines.Add($"תאריך השאלה:{DateTime.Now.ToShortDateString()}");
            }
            newLines.Add(@"}");
            newLines.Add("]");
            return newLines;
        }

        public List<Student> GetStudentList()
        {
            // Returns a list of all the students

            List<Student> newList = new List<Student>();
            foreach (var line in MFH.GetAllRawLines(Paths.studentsFile))
            {
                if (line.Contains("מ''ס"))
                    newList.Add(GetStudentByInfo(line));
            }
            return newList;
        }
        public Student GetStudentByInfo(string info)
        {
            // Returns a student by the information that is given
            // At the moment there wasn't any need for extracting students in any other way other than with the Id
            // Therefore this function is not full

            string name = null, phoneNum = null, BB_name = null;
            int id = new int();
            DateTime BB_lent;
            List<BookBorrowed> borrowedBooks = new List<BookBorrowed>();
            bool inMid = false, inMidBooks = false, insideBook = false;
            if (info.Contains("מ''ס"))
            {
                foreach (var line in MFH.GetAllRawLines(Paths.studentsFile))
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
                                BB_lent = DateTime.Parse(line.Substring(line.IndexOf(":") + 1));
                                borrowedBooks.Add(new BookBorrowed(BB_name, BB_lent));
                                insideBook = false;
                            }

                        }
                    }
                }
                return new Student(id, name, phoneNum, borrowedBooks);
            }
            return null;
        }

        public void UpdateStudentsLinesList() { Lines = MFH.GetAllRawLines(Paths.studentsFile); } // Updates the Lines list with the students file lines

        public BookBorrowed GetBorrowedBookInfo(Student student, string borrowedBookName)
        {
            // This function will return the borrowed book of a student as a BorrowedBook

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