using System;
using System.IO;

namespace bank_project.common
{
    internal class datafilehelper
    {
        public static string getresolvedfilepath(string fileName)
        {
            string programFolder = AppDomain.CurrentDomain.BaseDirectory;
            string programFolderFile = Path.Combine(programFolder, fileName);
            if (File.Exists(programFolderFile))
            {
                return programFolderFile;
            }
            DirectoryInfo folder = new DirectoryInfo(programFolder);
            while (folder != null)
            {
                string candidateFile = Path.Combine(folder.FullName, fileName);
                if (File.Exists(candidateFile))
                {
                    return candidateFile;
                }
                folder = folder.Parent;
            }
            return programFolderFile;
        }
    }
}
