using Patronage17.Engine.Interfaces;
using Patronage17.Engine.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Patronage17.Engine.Helpers
{
    public class FilesIoHelper
    {
        private static FilesIoHelper _instance;

        public static FilesIoHelper Instance
        {
            get { return _instance ?? (_instance = new FilesIoHelper()); }
        }

        private FilesIoHelper() { }

        public IEnumerable<FileInfo> GetFilesFromDirectory(string path, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            if (Directory.Exists(path))
            {
                var rootDir = new DirectoryInfo(path);
                return rootDir.GetFiles("*", searchOption);
            }
            return null;
        }

        public IEnumerable<string> GetFiles(string path)
        {
            if (Directory.Exists(path))
            {
                var filePathes = new List<string>();
                var rootDir = new DirectoryInfo(path);
                var files = rootDir.GetFiles("*", SearchOption.AllDirectories);
                files.ToList().ForEach(file => filePathes.Add(file.FullName));

                return filePathes;
            }
            return null;
        }

        public IFileInfo GetFileMetadata(string filePath)
        {
            if (File.Exists(filePath))
            {
                var fileData = new FileInfo(filePath);
                return new CustomFileInfo()
                {
                    Name = fileData.Name,
                    CreationTime = fileData.CreationTime.ToString(CultureInfo.CurrentUICulture),
                    LastWriteTime = fileData.LastWriteTime.ToString(CultureInfo.CurrentUICulture),
                    Extension = fileData.Extension
                };
            }
            return null;
        }

    }
}
