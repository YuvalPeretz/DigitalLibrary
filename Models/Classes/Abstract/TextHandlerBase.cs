using DigitalLibrary.Models.Classes.Useable;
using DigitalLibrary.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DigitalLibrary.Models.Classes.Abstract
{
    public abstract class TextHandlerBase : ITextHandler
    {
        public bool CheckLineExistens(string LineToCheck, string FilePath)
        {
            // Checks if the given line (entire line) exists in the file
            foreach (var line in File.ReadAllLines(FilePath))
            {
                if (line.Equals(LineToCheck))
                    return true;
            }
            return false;
        }

        public bool CheckWordExistens(string WordToCheck, string FilePath)
        {
            // Checks if the given word exists in the file
            foreach (var line in File.ReadAllLines(FilePath))
            {
                if (line.Contains(WordToCheck))
                    return true;
            }
            return false;
        }

        public void DeleteLines(string LineToDelete, string FilePath)
        {
            // Deletes all the lines which has the value of the given line
            try
            {
                if (File.ReadLines(FilePath).Contains(LineToDelete))
                {
                    string item = LineToDelete.Trim();
                    var lines = File.ReadAllLines(FilePath).Where(line => line.Trim() != item).ToArray(); // creates an array with all the lines except for the line with the text.
                    File.WriteAllLines(FilePath, lines); // re-writing all the lines.
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"ERROR: {e}");
            }
        }
        public void DeleteLine(string LineToDelete, string FilePath)
        {
            // Deletes only the first line which has the given value
            // This is useful in the Override method, because we want to change the exact line everytime

            bool deleted = false;
            List<string> newLines = new List<string>();
            foreach (var line in GetAllRawLines(FilePath))
            {
                if (line.Equals(LineToDelete) & !deleted)
                {
                    deleted = true;
                    continue;
                }
                else
                    newLines.Add(line);
            }
            OverrideFile(newLines,FilePath);
        }


        public List<string> GetAllRawLines(string FilePath)
        {
            // Returns a List<string> which contains the whole file's lines
            // looks useless but I do want all the actions to be taken from inside of the TextHandler classes

            List<string> AllLines = File.ReadAllLines(FilePath).ToList();
            return AllLines;
        }

        public int GetSpecificLineNum(string LineToCheck, string FilePath)
        {
            // Return the lineIndex of the given line

            int lineNum = 0;
            foreach (var line in File.ReadAllLines(FilePath))
            {
                lineNum++;
                if (line.Equals(LineToCheck))
                    return lineNum;
            }
            return lineNum;
        }

        public virtual void InitializeFiles()
        {
            // This function must exist but will be initialized only in the inhereting class
            // This is because each class has its own set of files to control

            throw new NotImplementedException();
        }

        public void OverrideFile(List<string> LinesToWrite, string FilePath)
        {
            // Overrides the entire file

            File.WriteAllText(FilePath, String.Empty);
            File.WriteAllLines(FilePath, LinesToWrite);
        }
        public void OverrideLine(string newLine, string oldLine, string FilePath)
        {
            // Overrides specific line

            int lineIndex = GetSpecificLineNum(oldLine, FilePath) - 1;
            DeleteLine(oldLine, FilePath);
            WriteToFile(newLine, lineIndex, FilePath);
        }
        public void OverrideAllLine(string newLine, string oldLine, string FilePath)
        {
            // Override all the lines which as the value of the oldLine in the newLine's value

            foreach (var line in GetAllRawLines(FilePath))
            {
                if (line.Equals(oldLine))
                {
                    int lineIndex = GetSpecificLineNum(oldLine, FilePath) - 1;
                    DeleteLine(oldLine, FilePath);
                    WriteToFile(newLine, lineIndex, FilePath);
                }
            }
        }

        public void WriteToFile(string TXTToWrite, string FilePath) { File.AppendAllText(FilePath, TXTToWrite); } // Just appends to the file the text, almost useless, but I do want the calls to be called from this class
        public void WriteToFile(string TXTToWrite, int lineIndex, string FilePath)
        {
            // Insert to the file a text in a specific index

            var txtLines = File.ReadAllLines(FilePath).ToList();
            txtLines.Insert(lineIndex, TXTToWrite);
            File.WriteAllLines(FilePath, txtLines);
        }
    }
}
