using System;
using System.IO;

namespace Juniansoft.Termission.Core.Services
{
    public class FileService: IFileService
    {
        public FileService()
        {
        }

        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public void WriteAllText(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}
