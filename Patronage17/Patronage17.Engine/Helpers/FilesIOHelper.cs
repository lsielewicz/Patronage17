using System.Collections.Generic;
using System.IO;

namespace Patronage17.Engine.Helpers
{
    public class FilesIoHelper
    {
        private static FilesIoHelper _instance;

        public static FilesIoHelper Instance
        {
            get { return _instance ?? (_instance = new FilesIoHelper()); }
        }

        private FilesIoHelper() {    }

        public IEnumerable<FileInfo> GetFilesFromDirectory(string path, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            if (Directory.Exists(path))
            {
                var rootDir = new DirectoryInfo(path);
                return rootDir.GetFiles("*", searchOption);
            }
            return null;
        }

    }
}
