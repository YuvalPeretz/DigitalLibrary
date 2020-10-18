using DigitalLibrary.Models.Classes.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DigitalLibrary.Models.Classes.Useable
{
    public sealed class StaticInformationHandler : TextHandlerBase
    {
        // This class is used for the static files, such as the backup files, and the staticInformation file

        private static readonly Lazy<StaticInformationHandler> SIH = new Lazy<StaticInformationHandler>(() => new StaticInformationHandler());
        public static StaticInformationHandler Instance { get { return SIH.Value; } }
        MainFilesHandler MFH = MainFilesHandler.Instance;
        private StaticInformationHandler() { }

        public override void InitializeFiles()
        {
            // Initialize the files

            Directory.CreateDirectory(Paths.tempDL);
            if (!File.Exists(Paths.tempDLFile))
            {
                File.Create(Paths.tempDLFile).Dispose();
            }
            if (!CheckWordExistens("firstTime"))
            {
                WriteToFile("firstTime=true\n");
            }
            if (!CheckWordExistens("framworkinstalled"))
            {
                WriteToFile("framworkinstalled=false\n");
            }
        }
        public void TriggerBackup()
        {
            // This function trigers the backup, by making sure all the files are created and backup all the information
            
            Directory.CreateDirectory(Paths.BA_tempDir);
            Directory.CreateDirectory(Paths.BA_imgDir);
            MFH.BackupImages();
            if (!File.Exists(Paths.BA_bFile))
            {
                File.Create(Paths.BA_bFile).Dispose();
                File.WriteAllText(Paths.BA_bFile, File.ReadAllText(Paths.booksFile));
            }
            else
                File.WriteAllText(Paths.BA_bFile, File.ReadAllText(Paths.booksFile));
            if (!File.Exists(Paths.BA_sFile))
            {
                File.Create(Paths.BA_sFile).Dispose();
                File.WriteAllText(Paths.BA_sFile, File.ReadAllText(Paths.studentsFile));
            }
            else
                File.WriteAllText(Paths.BA_sFile, File.ReadAllText(Paths.studentsFile));
        }

        public void WriteToFile(string TXTToWrite)  { WriteToFile(TXTToWrite, Paths.tempDLFile); } // Overrload of the base function, just calls it with the static temp
        public void DeleteLine(string TXTToDelete) { DeleteLines(TXTToDelete, Paths.tempDLFile); } // Overrload of the base function, just calls it with the static temp

        public void OverrideFile(List<string> LinesToWrite) { OverrideFile(LinesToWrite, Paths.tempDLFile); } // Overrload of the base function, just calls it with the static temp
        public void OverrideLine(string newLine, string oldLine) { OverrideLine(newLine, oldLine, Paths.tempDLFile); } // Overrload of the base function, just calls it with the static temp

        public bool CheckLineExistens(string LineToCheck) { return CheckLineExistens(LineToCheck,Paths.tempDLFile); } // Overrload of the base function, just calls it with the static temp
        public bool CheckWordExistens(string WordToCheck) { return CheckWordExistens(WordToCheck, Paths.tempDLFile); } // Overrload of the base function, just calls it with the static temp

        public List<string> GetAllRawLines() { return GetAllRawLines(Paths.tempDLFile); } // Overrload of the base function, just calls it with the static temp
        public string GetValueAfterEqual(string Header)
        {
            // Returns the information of a line by its header after its header

            foreach (var line in File.ReadAllLines(Paths.tempDLFile))
            {
                if (line.Contains(Header))
                    return line.Substring(line.IndexOf("=") + 1);
            }
            return null;
        }
    }
}
