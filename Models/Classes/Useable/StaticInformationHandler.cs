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
    public sealed class StaticInformationHandler
    {
        private static readonly Lazy<StaticInformationHandler> SIH = new Lazy<StaticInformationHandler>(() => new StaticInformationHandler());
        public static StaticInformationHandler Instance { get { return SIH.Value; } }
        TextFileHandler TFH = TextFileHandler.Instance;
        private StaticInformationHandler() { }

        public void InitializeMainTemp()
        {
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
            Directory.CreateDirectory(Paths.BA_tempDir);
            Directory.CreateDirectory(Paths.BA_imgDir);
            TFH.BackupImages();
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

        public void WriteToFile(string TXTToWrite) { File.AppendAllText(Paths.tempDLFile, TXTToWrite); }
        public void DeleteLine(string TXTToDelete) // deletes the text that was entered, MUST ENTER EACH LINE AGAIN!
        {
            try
            {
                string item = TXTToDelete.Trim();
                var lines = File.ReadAllLines(Paths.tempDLFile).Where(line => line.Trim() != item).ToArray(); // creates an array with all the lines except for the line with the text.
                File.WriteAllLines(Paths.tempDLFile, lines); // re-writing all the lines.
            }
            catch (Exception e)
            {
                MessageBox.Show($"ERROR: {e}");
            }
            
        }

        public void OverrideFile(List<string> LinesToWrite)
        {
            File.WriteAllText(Paths.tempDLFile, String.Empty);
            File.WriteAllLines(Paths.tempDLFile, LinesToWrite);
        }
        public void OverrideLine(string newLine, string oldLine)
        {
            DeleteLine(oldLine);
            WriteToFile($"{newLine}");
        }

        public bool CheckLineExistens(string LineToCheck)
        {
            if (File.ReadAllLines(Paths.tempDLFile).Contains(LineToCheck))
                return true;
            return false;
        }
        public bool CheckWordExistens(string WordToCheck)
        {
            foreach (var line in File.ReadAllLines(Paths.tempDLFile))
            {
                if (line.Contains(WordToCheck))
                    return true;
            }
            return false;
        }

        public List<string> GetAllRawLines()
        {
            List<string> AllLines = new List<string>();
            foreach (var line in File.ReadAllLines(Paths.tempDLFile))
            {
                AllLines.Add(line);
            }
            return AllLines;
        }
        public string GetPartOfLine(string Header)
        {
            foreach (var line in File.ReadAllLines(Paths.tempDLFile))
            {
                if (line.Contains(Header))
                    return line.Substring(line.IndexOf("=") + 1);
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
    }
}
