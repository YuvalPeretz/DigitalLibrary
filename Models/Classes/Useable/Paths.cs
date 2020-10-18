using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DigitalLibrary.Models.Classes.Useable
{
    public abstract class Paths
    {
        // This class holdes and controls the information regarding the Paths of all the files

        static StaticInformationHandler SIH = StaticInformationHandler.Instance;
        public static string tempDL { get; } = $@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\DigitalLibraryStaticInformation"; //unchangable path to the temp directory
        public static string tempDLFile { get; } = $@"{tempDL}\DigitalLibraryStaticInformation.txt"; //unchangable file path. holds paths info therfore must be static.

        public static string tempDirectory { get; private set; } //main files
        public static string imgDirectory { get; private set; }
        public static string studentsFile { get; private set; }
        public static string booksFile { get; private set; }

        public static string BA_tempDir { get; private set; } //backup files
        public static string BA_imgDir { get; private set; }
        public static string BA_sFile { get; private set; }
        public static string BA_bFile { get; private set; }

        private static void SetPath(string newPath, string pathToWriteTo)
        {
            if (pathToWriteTo.Equals("tempDirectory"))
                tempDirectory = newPath;
            else if (pathToWriteTo.Equals("imgDirectory"))
                imgDirectory = newPath;
            else if (pathToWriteTo.Equals("studentsFile"))
                studentsFile = newPath;
            else if (pathToWriteTo.Equals("booksFile"))
                booksFile = newPath;
            else if (pathToWriteTo.Equals("BA_tempDir"))
                BA_tempDir = newPath;
            else if (pathToWriteTo.Equals("BA_imgDir"))
                BA_imgDir = newPath;
            else if (pathToWriteTo.Equals("BA_sFile"))
                BA_sFile = newPath;
            else if (pathToWriteTo.Equals("BA_bFile"))
                BA_bFile = newPath;
        }

        public static void InitializeSettings()
        {
            if (!SIH.CheckWordExistens("tutorial"))
                SIH.WriteToFile("tutorial=true");
            if (SIH.CheckLineExistens("firstTime=true")) // checks if this is the first time entering
            {
                SIH.DeleteLine("firstTime=true");
                SIH.WriteToFile("firstTime=false\n");// reset the entering value to not being the first time

                if (!SIH.CheckWordExistens("tempDirectory"))// for each of the following checks, if the file path doesnt exists, will create one (as it should always on the first run)
                {
                    SIH.WriteToFile($"tempDirectory={$@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\DigitalLibraryData"}\n");
                    SetPath(SIH.GetValueAfterEqual("tempDirectory"), "tempDirectory");
                }
                if (!SIH.CheckWordExistens("imgDirectory"))
                {
                    SIH.WriteToFile($"imgDirectory={$@"{tempDirectory}\imgDirectory"}\n");
                    SetPath(SIH.GetValueAfterEqual("imgDirectory"), "imgDirectory");
                }
                if (!SIH.CheckWordExistens("studentsFile"))
                {
                    SIH.WriteToFile($"studentsFile={$@"{tempDirectory}\studentsFile.txt"}\n");
                    SetPath(SIH.GetValueAfterEqual("studentsFile"), "studentsFile");
                }
                if (!SIH.CheckWordExistens("booksFile"))
                {
                    SIH.WriteToFile($"booksFile={$@"{tempDirectory}\booksFile.txt"}\n");
                    SetPath(SIH.GetValueAfterEqual("booksFile"), "booksFile");
                }
                if (!SIH.CheckWordExistens($@"BA_tempDir={tempDL}\BA_tempDir"))
                {
                    SIH.WriteToFile($"BA_tempDir={$@"{tempDL}\BA_tempDir"}\n");
                    SetPath(SIH.GetValueAfterEqual("BA_tempDir"), "BA_tempDir");
                }
                if (!SIH.CheckWordExistens("BA_imgDir"))
                {
                    SIH.WriteToFile($"BA_imgDir={$@"{BA_tempDir}\BA_imgDir"}\n");
                    SetPath(SIH.GetValueAfterEqual("BA_imgDir"), "BA_imgDir");
                }
                if (!SIH.CheckWordExistens("BA_sFile"))
                {
                    SIH.WriteToFile($"BA_sFile={$@"{BA_tempDir}\BA_sFile.txt"}\n");
                    SetPath(SIH.GetValueAfterEqual("BA_sFile"), "BA_sFile");
                }
                if (!SIH.CheckWordExistens("BA_bFile"))
                {
                    SIH.WriteToFile($"BA_bFile={$@"{BA_tempDir}\BA_bFile.txt"}\n");
                    SetPath(SIH.GetValueAfterEqual("BA_bFile"), "BA_bFile");
                }
            }
            else if (SIH.CheckLineExistens("firstTime=false")) // if it is not the first time entering, will rewrite the paths to the directories and folders
            {
                if (SIH.CheckWordExistens("tempDirectory"))
                {
                    SetPath(SIH.GetValueAfterEqual("tempDirectory"), "tempDirectory");

                    SIH.OverrideLine(
                        $"imgDirectory={$@"{tempDirectory}\imgDirectory"}\n",
                        "imgDirectory=" + SIH.GetValueAfterEqual("imgDirectory"));
                    SetPath(SIH.GetValueAfterEqual("imgDirectory"), "imgDirectory");

                    SIH.OverrideLine(
                        $"studentsFile={$@"{tempDirectory}\studentsFile.txt"}\n",
                        "studentsFile=" + SIH.GetValueAfterEqual("studentsFile"));
                    SetPath(SIH.GetValueAfterEqual("studentsFile"), "studentsFile");

                    SIH.OverrideLine(
                        $"booksFile={$@"{tempDirectory}\booksFile.txt"}\n",
                        "booksFile=" + SIH.GetValueAfterEqual("booksFile"));
                    SetPath(SIH.GetValueAfterEqual("booksFile"), "booksFile");
                }
                if (SIH.CheckWordExistens("BA_tempDir"))
                {
                    SetPath(SIH.GetValueAfterEqual("BA_tempDir"), "BA_tempDir");

                    SIH.OverrideLine(
                        $"BA_imgDir={$@"{BA_tempDir}\BA_imgDir"}\n",
                        "BA_imgDir=" + SIH.GetValueAfterEqual("BA_imgDir"));
                    SetPath(SIH.GetValueAfterEqual("BA_imgDir"), "BA_imgDir");

                    SIH.OverrideLine(
                        $"BA_sFile={$@"{BA_tempDir}\BA_sFile.txt"}\n",
                        "BA_sFile=" + SIH.GetValueAfterEqual("BA_sFile"));
                    SetPath(SIH.GetValueAfterEqual("BA_sFile"), "BA_sFile");

                    SIH.OverrideLine(
                        $"BA_bFile={$@"{BA_tempDir}\BA_bFile.txt"}\n",
                        "BA_bFile=" + SIH.GetValueAfterEqual("BA_bFile"));
                    SetPath(SIH.GetValueAfterEqual("BA_bFile"), "BA_bFile");
                }
            }
        }
    }
}
