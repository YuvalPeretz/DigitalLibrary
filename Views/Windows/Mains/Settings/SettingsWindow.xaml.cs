using DigitalLibrary.Models.Classes.Useable;
using DigitalLibrary.Views.Windows.Mains.Settings.Info;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;
using Path = System.IO.Path;

namespace DigitalLibrary.Views.Windows.Mains.Settings
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        StaticInformationHandler SIH = StaticInformationHandler.Instance;
        TextFileHandler TFH = TextFileHandler.Instance;
        Window IF;
        public SettingsWindow()
        {
            InitializeComponent();
            if (SIH.CheckWordExistens("tutorial"))
                CB_TutorialEnable.IsChecked = bool.Parse(SIH.GetPartOfLine("tutorial"));
            TutorialCheck();
            SIH.TriggerBackup();
        }

        private void TutorialCheck()
        {
            if (SIH.CheckLineExistens("tutorial=true"))
                SIH.DeleteLine("tutorial=true");
            else if (SIH.CheckLineExistens("tutorial=false"))
                SIH.DeleteLine("tutorial=false");
            SIH.WriteToFile($"tutorial={CB_TutorialEnable.IsChecked.ToString().ToLower()}");
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void BTN_Exit_Click(object sender, RoutedEventArgs e) { this.Close(); }
        private void BTN_Info_Click(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<InfoWindow>().Any())
            {
                IF = new InfoWindow
                {
                    Tag = "mdi_child"
                };
                IF.ShowDialog();
            }
        }
        private void BTN_FilePicker_Click(object sender, RoutedEventArgs e)
        {

            if (!Directory.Exists(Paths.tempDirectory) ||
            !Directory.Exists(Paths.imgDirectory) ||
            !File.Exists(Paths.studentsFile) ||
            !File.Exists(Paths.booksFile))
            {
                TFH.InitializeFilesWithoutWriting();
            }
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            string
                preTempDirectory = String.Copy(Paths.tempDirectory),
                preImageDirectory = String.Copy(Paths.imgDirectory),
                preBooksFile = String.Copy(Paths.booksFile),
                preStudentFile = String.Copy(Paths.studentsFile);
            fbd.SelectedPath = Paths.tempDirectory;
            DialogResult result = fbd.ShowDialog();
            if (result.ToString().Equals("OK")) //after clicks "OK", the chosen path will override existing path, firstly manually by this function (only the tempDirectory path), then all the sub paths by the function "Paths.InitializeSettings()"
            {
                if (fbd.SelectedPath.Contains(@"\DigitalLibraryData"))
                {
                    SIH.DeleteLine($"tempDirectory={preTempDirectory}");
                    SIH.WriteToFile($"tempDirectory={$@"{fbd.SelectedPath}"}\n");
                    Paths.InitializeSettings();
                    try //attempting to move all the files in the directory and itself to the new location
                    {
                        if (!Directory.Exists(Paths.tempDirectory))
                            Directory.CreateDirectory(Paths.tempDirectory);
                        if (!Directory.Exists(Paths.imgDirectory))
                            Directory.CreateDirectory(Paths.imgDirectory);
                        File.WriteAllText(Paths.booksFile, File.ReadAllText(preBooksFile));
                        File.WriteAllText(Paths.studentsFile, File.ReadAllText(preStudentFile));
                        File.Delete(preBooksFile);
                        File.Delete(preStudentFile);
                        Directory.Delete(preImageDirectory);
                        Directory.Delete(preTempDirectory);
                        MessageBox.Show("מיקום הקבצים שונה בהצלחה");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("שינוי מיקום הקבצים כשל");
                    }
                }
                else if (Directory.Exists($@"{fbd.SelectedPath}\BA_tempDir"))
                {
                    Paths.InitializeSettings();
                    try //attempting to move all the files in the directory and itself to the new location
                    {
                        if (!Directory.Exists(Paths.tempDirectory))
                            Directory.CreateDirectory(Paths.tempDirectory);
                        if (!Directory.Exists(Paths.imgDirectory))
                            Directory.CreateDirectory(Paths.imgDirectory);
                        DirectoryInfo di = new DirectoryInfo(Paths.imgDirectory);
                        di.GetFiles().ToList().ForEach(file => { file.Delete(); });
                        Directory.GetFiles(Paths.BA_imgDir).ToList().ForEach(imgUrl => {
                            File.Copy(imgUrl, $@"{Paths.imgDirectory}\{Path.GetFileName(imgUrl)}", true);
                        });
                        File.Create(Paths.booksFile).Dispose();
                        File.WriteAllText(Paths.booksFile, File.ReadAllText($@"{fbd.SelectedPath}\BA_tempDir\BA_bFile.txt"));
                        File.Create(Paths.studentsFile).Dispose();
                        File.WriteAllText(Paths.studentsFile, File.ReadAllText($@"{fbd.SelectedPath}\BA_tempDir\BA_sFile.txt"));
                        File.Delete($@"{fbd.SelectedPath}\BA_tempDir\BA_bFile.txt");
                        File.Delete($@"{fbd.SelectedPath}\BA_tempDir\BA_sFile.txt");
                        Directory.Delete($@"{fbd.SelectedPath}\BA_tempDir\BA_imgDir");
                        Directory.Delete($@"{fbd.SelectedPath}\BA_tempDir");
                        MessageBox.Show("הגיבוי נטען בהצלחה");
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("טעינת הגיבוי נכשלה"+ error);
                    }
                }
                else if (fbd.SelectedPath.Contains("BA_tempDir"))
                {
                    Paths.InitializeSettings();
                    try //attempting to move all the files in the directory and itself to the new location
                    {
                        if (!Directory.Exists(Paths.tempDirectory))
                            Directory.CreateDirectory(Paths.tempDirectory);
                        if (!Directory.Exists(Paths.imgDirectory))
                            Directory.CreateDirectory(Paths.imgDirectory);
                        DirectoryInfo di = new DirectoryInfo(Paths.imgDirectory);
                        di.GetFiles().ToList().ForEach(file => { file.Delete(); });
                        Directory.GetFiles($@"{fbd.SelectedPath}\BA_imgDir").ToList().ForEach(imgUrl => {
                            File.Copy(imgUrl, $@"{$@"{fbd.SelectedPath}\BA_imgDir"}\{Path.GetFileName(imgUrl)}", true);
                        });
                        File.WriteAllText(Paths.booksFile, File.ReadAllText($@"{fbd.SelectedPath}\BA_bFile.txt"));
                        File.WriteAllText(Paths.studentsFile, File.ReadAllText($@"{fbd.SelectedPath}\BA_sFile.txt"));
                        File.Delete($@"{fbd.SelectedPath}\BA_bFile.txt");
                        File.Delete($@"{fbd.SelectedPath}\BA_sFile.txt");
                        Directory.Delete($@"{fbd.SelectedPath}\BA_imgDir");
                        Directory.Delete($@"{fbd.SelectedPath}");
                        MessageBox.Show("הגיבוי נטען בהצלחה");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("אעינת הגיבוי נכשלה");
                    }
                }
                else
                {
                    SIH.DeleteLine($"tempDirectory={preTempDirectory}");
                    SIH.WriteToFile($"tempDirectory={$@"{fbd.SelectedPath}\DigitalLibraryData"}\n");
                    Paths.InitializeSettings();
                    try //attempting to move all the files in the directory and itself to the new location
                    {
                        if (!Directory.Exists(Paths.tempDirectory))
                            Directory.CreateDirectory(Paths.tempDirectory);
                        if (!Directory.Exists(Paths.imgDirectory))
                            Directory.CreateDirectory(Paths.imgDirectory);
                        File.WriteAllText(Paths.booksFile, File.ReadAllText(preBooksFile));
                        File.WriteAllText(Paths.studentsFile, File.ReadAllText(preStudentFile));
                        File.Delete(preBooksFile);
                        File.Delete(preStudentFile);
                        Directory.Delete(preImageDirectory);
                        Directory.Delete(preTempDirectory);
                        MessageBox.Show("מיקום הקבצים שונה בהצלחה");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("שינוי מיקום הקבצים כשל");
                    }
                }
            }
        }
        private void CB_TutorialEnable_Click(object sender, RoutedEventArgs e) { TutorialCheck(); }
        private void BTN_OpenBackup_Click(object sender, RoutedEventArgs e) { System.Diagnostics.Process.Start(Paths.BA_tempDir); }

        private void BTN_OpenInfoDirectory_Click(object sender, RoutedEventArgs e) { System.Diagnostics.Process.Start(Paths.tempDirectory); }
    }
}
