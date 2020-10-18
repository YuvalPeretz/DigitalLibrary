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
using DigitalLibrary.Models.Interfaces;
using DigitalLibrary.Models.Classes.Abstract;

namespace DigitalLibrary.Models.Classes.Useable
{
    public sealed class MainFilesHandler : TextHandlerBase
    {
        // This class is used for the main files

        private static readonly Lazy<MainFilesHandler> MFH = new Lazy<MainFilesHandler>(() => new MainFilesHandler()); // Used for a Singleton Design
        public static MainFilesHandler Instance { get { return MFH.Value; } } // Used for a Singleton Design
        private MainFilesHandler() { } // Used for a Singleton Design
        BooksWindow BW; // Used for BW.WriteToConsole(...)
        StudentsWindow SW; // Used for SW.WriteToConsole(...)

        public override void InitializeFiles()
        {
            // Initializing the main files

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
            // Same as before, but here the action is used for displaying the result in the console of the caller window

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
            // Same as before, without printing anything to the console

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

        public string GetImagePathFromFolder(string OriginalPath)
        {
            // Returns the url of the image in the imgDirectory

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

        public void AddToImageFile(string ImgPath) 
        {
            // Copying an image to the imgDirectory from its original path

            if(!string.IsNullOrEmpty(ImgPath))
                File.Copy(ImgPath,Paths.imgDirectory +@"\"+ Path.GetFileName(ImgPath),true);    
        }

        public void BackupImages()
        {
            // Backingup the image in the backup directory

            DirectoryInfo di = new DirectoryInfo(Paths.BA_imgDir);
            di.GetFiles().ToList().ForEach(file => { file.Delete(); });
            Directory.GetFiles(Paths.imgDirectory).ToList().ForEach(imgUrl => {
                File.Copy(imgUrl,$@"{Paths.BA_imgDir}\{Path.GetFileName(imgUrl)}",true);
            });
        }
    }
}