using System.IO;

namespace EmailApplication.Utils
{
    public class FileManager
    {
        private readonly string _rootPath;

        public FileManager(string rootPath)
        {
            this._rootPath = rootPath;
        }

        public string GetLastHtmlReportFile()
        {
            var htmlFile = Directory.GetFiles(_rootPath, "*.html");

            return htmlFile[htmlFile.Length - 1];
        }
    }
}
