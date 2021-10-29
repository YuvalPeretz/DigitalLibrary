using DigitalLibrary.Models.Classes.Common;
using DigitalLibrary.Views.UserControls;
using DigitalLibrary.Views.Windows.Mains;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Path = System.IO.Path;
using BooksWindow = DigitalLibrary.Views.Windows.Mains.BooksWindow;
using System.Security.Cryptography;
using DigitalLibrary.Views.Windows.Mains.Students;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace DigitalLibrary.Models.Classes.Useable
{
    public sealed class TextFileHandler
    {
        private static readonly Lazy<TextFileHandler> TFH = new Lazy<TextFileHandler>(() => new TextFileHandler());
        public static TextFileHandler Instance { get { return TFH.Value; } }
        private TextFileHandler() { }
        BooksWindow BW;
        StudentsWindow SW;

        public void InitializeFiles()
        {
            if (Application.Current.Windows.OfType<BooksWindow>().Count() != 0)
                BW = Application.Current.Windows.OfType<BooksWindow>().First();
            else if (Application.Current.Windows.OfType<StudentsWindow>().Count() != 0)
                SW = Application.Current.Windows.OfType<StudentsWindow>().First();
            if (!Directory.Exists(Paths.tempDirectory))
            {
                try
                {
                    Directory.CreateDirectory(Paths.tempDirectory);
                    BW.WriteToConsole("תיקיית קבצים נוצרה בהצלחה");
                }
                catch (Exception)
                {

                    BW.WriteToConsole("שגיאה ביצירת תקיית קבצים");
                }
            }
            if (!Directory.Exists(Paths.imgDirectory))
            {
                try
                {
                    Directory.CreateDirectory(Paths.imgDirectory);
                    BW.WriteToConsole("תיקיית תמונות נוצרה בהצלחה");
                }
                catch (Exception)
                {

                    BW.WriteToConsole("שגיאה ביצירת תקיית קבצים");
                }
            }
            if (!File.Exists(Paths.studentsFile))
            {
                try
                {
                    File.Create(Paths.studentsFile).Dispose();
                    BW.WriteToConsole("קובץ תלמידים נוצר בהצלחה");
                }
                catch (Exception)
                {

                    BW.WriteToConsole("שגיאה ביצירת קובץ תלמידים");
                }
            }
            if (!File.Exists(Paths.booksFile))
            {
                try
                {
                    File.Create(Paths.booksFile).Dispose();
                    BW.WriteToConsole("קובץ ספרים נוצר בהצלחה");
                }
                catch (Exception)
                {
                    BW.WriteToConsole("שגיאה ביצירת קובץ ספרים");
                }
            }
        }
        public void InitializeFiles(Action<string> action)
        {
            if (Application.Current.Windows.OfType<BooksWindow>().Count() !=0)
                BW = Application.Current.Windows.OfType<BooksWindow>().First();
            else if (Application.Current.Windows.OfType<StudentsWindow>().Count() != 0)
                SW = Application.Current.Windows.OfType<StudentsWindow>().First();
            if (!Directory.Exists(Paths.tempDirectory))
            {
                try
                {
                    Directory.CreateDirectory(Paths.tempDirectory);
                    action("תיקיית קבצים נוצרה בהצלחה");
                }
                catch (Exception)
                {

                    action("שגיאה ביצירת תקיית קבצים");
                }
            }
            if (!Directory.Exists(Paths.imgDirectory))
            {
                try
                {
                    Directory.CreateDirectory(Paths.imgDirectory);
                    action("תיקיית תמונות נוצרה בהצלחה");
                }
                catch (Exception)
                {

                    action("שגיאה ביצירת תקיית קבצים");
                }
            }
            if (!File.Exists(Paths.studentsFile))
            {
                try
                {
                    File.Create(Paths.studentsFile).Dispose();
                    action("קובץ תלמידים נוצר בהצלחה");
                }
                catch (Exception)
                {

                    action("שגיאה ביצירת קובץ תלמידים");
                }
            }
            if (!File.Exists(Paths.booksFile))
            {
                try
                {
                    File.Create(Paths.booksFile).Dispose();
                    action("קובץ ספרים נוצר בהצלחה");
                }
                catch (Exception)
                {
                    action("שגיאה ביצירת קובץ ספרים");
                }
            }
        }
        public void InitializeFilesWithoutWriting()
        {
            if (!Directory.Exists(Paths.tempDirectory))
            {
                try
                {
                    Directory.CreateDirectory(Paths.tempDirectory);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            if (!Directory.Exists(Paths.imgDirectory))
            {
                try
                {
                    Directory.CreateDirectory(Paths.imgDirectory);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            if (!File.Exists(Paths.studentsFile))
            {
                try
                {
                    File.Create(Paths.studentsFile).Dispose();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            if (!File.Exists(Paths.booksFile))
            {
                try
                {
                    File.Create(Paths.booksFile).Dispose();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public void MoveFiles(string sourcePath, string destPath) { File.Move(sourcePath,destPath); }

        public async void WriteToFile(string TXTToWrite, string FilePath) { await Task.Run(() => File.AppendAllText(FilePath, TXTToWrite)); }
        public async void DeleteLine(string TXTToDelete, string FilePath) // deletes the text that was entered, MUST ENTER EACH LINE AGAIN!
        {
            if (await Task.Run(() => File.ReadLines(FilePath).Contains(TXTToDelete)))
            {
                string item = TXTToDelete.Trim();
                var lines = await Task.Run(() => File.ReadAllLines(FilePath).Where(line => line.Trim() != item).ToArray()); // creates an array with all the lines except for the line with the text.
                await Task.Run(() => File.WriteAllLines(FilePath, lines)); // re-writing all the lines.
            }
        }

        public void OverrideFile(List<string> LinesToWrite, string FilePath)
        {
            File.WriteAllText(FilePath, String.Empty);
            File.WriteAllLines(FilePath, LinesToWrite);
        }
        public void OverrideLine(string preLine, string newLine, string FilePath)
        {
            List<string> newFileLines = new List<string>();
            foreach (var line in File.ReadAllLines(FilePath))
            {
                if (!line.Equals(preLine))
                {
                    newFileLines.Add(line);
                    continue;
                }
                else
                    newFileLines.Add(newLine);
            }
            OverrideFile(newFileLines,FilePath);
        }

        public bool CheckLineExistens(string LineToCheck, string FilePath)
        {
            bool returnval = false;
            File.ReadAllLines(FilePath).ToList().ForEach(line => { 
                if(line.Equals(LineToCheck))
                    returnval = true;
            });
                
            return returnval;
        }

        public List<string> GetAllRawLines(string FilePath)
        {
            List<string> AllLines = new List<string>();
            foreach (var line in File.ReadAllLines(FilePath))
            {
                AllLines.Add(line);
            }
            return AllLines;
        }
        public string GetImagePathFromFolder(string OriginalPath)
        {
            if (!string.IsNullOrEmpty(OriginalPath))
            {
                foreach (var img in Directory.GetFiles(Paths.imgDirectory))
                {
                    if (Path.GetFileName(OriginalPath).Equals(Path.GetFileName(img)))
                        return img;
                }
                return OriginalPath;
            }
            return null;
        }
        public int GetSpecificLineNum(string LineToCheck, string FilePath)
        {
            int lineNum = 0;
            foreach (var line in File.ReadAllLines(FilePath))
            {
                lineNum++;
                if (line.Equals(LineToCheck))
                    return lineNum;
            }
            return lineNum;
        }

        public async void AddToImageFile(string ImgPath) 
        {
            if(!string.IsNullOrEmpty(ImgPath))
                await Task.Run(() => File.Copy(ImgPath,Paths.imgDirectory +@"\"+ Path.GetFileName(ImgPath),true));    
        }
        public void BackupImages()
        {
            DirectoryInfo di = new DirectoryInfo(Paths.BA_imgDir);
            di.GetFiles().ToList().ForEach(file => { file.Delete(); });
            Directory.GetFiles(Paths.imgDirectory).ToList().ForEach(imgUrl => {
                File.Copy(imgUrl,$@"{Paths.BA_imgDir}\{Path.GetFileName(imgUrl)}",true);
            });
        }
    }
}