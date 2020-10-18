using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalLibrary.Models.Interfaces
{
    public interface ITextHandler
    {
        // Base for the TextHandlerBase abstract class

        public void InitializeFiles();
        public void WriteToFile(string TXTToWrite, string FilePath);
        public void DeleteLines(string TXTToDelete, string FilePath);

        public void OverrideFile(List<string> LinesToWrite, string FilePath);
        public void OverrideLine(string newLine, string oldLine, string FilePath);

        public bool CheckLineExistens(string LineToCheck, string FilePath);
        public bool CheckWordExistens(string WordToCheck, string FilePath);

        public List<string> GetAllRawLines(string FilePath);
        public int GetSpecificLineNum(string LineToCheck, string FilePath);
    }
}
