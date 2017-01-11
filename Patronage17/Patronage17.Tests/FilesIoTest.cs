using Patronage17.Engine.Helpers;
using Patronage17.Engine.Interfaces;
using Patronage17.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Patronage17.Tests
{
    public class FilesIoTest
    {

        [Fact]
        public void GetFilesFromApplicationHomeDirectory()
        {
            var files = FilesIoHelper.Instance.GetFiles(LocalizationStringsProvider.ApplicationLocalization);
            Assert.NotNull(files);
            Assert.NotEmpty(files);
        }

        [Fact]
        public void GetFilesFromNotExistingDirectory()
        {
            var files = FilesIoHelper.Instance.GetFiles(LocalizationStringsProvider.NotExistingPath);
            Assert.Null(files);
        }

        [Fact]
        public void GetFilesFromTooLongPathArgument()
        {
            var files = FilesIoHelper.Instance.GetFiles(LocalizationStringsProvider.TooLongPathString);
            Assert.Null(files);
        }

        [Fact]
        public void GetFilesWithArgumentThatIsNotPath()
        {
            var files = FilesIoHelper.Instance.GetFiles(LocalizationStringsProvider.TooLongPathString);
            Assert.Null(files);
        }

        [Fact]
        public void CheckArgumentIsPointingToExistingDirectory()
        {
            Assert.True(Directory.Exists(LocalizationStringsProvider.ApplicationLocalization));
            Assert.False(Directory.Exists(LocalizationStringsProvider.NotExistingPath));
        }

        [Fact]
        public void TryToExecuteGetFilesWithIncorrectArgument()
        {
            Assert.Null(FilesIoHelper.Instance.GetFilesFromDirectory(null));
        }

        [Fact]
        public void GetMetataOfEveryFileInApplicationHomeDirectory()
        {
            var filePathes = FilesIoHelper.Instance.GetFiles(LocalizationStringsProvider.ApplicationLocalization);
            Assert.NotNull(filePathes);
            Assert.NotEmpty(filePathes);

            var fileInfos = new List<IFileInfo>();
            filePathes.ToList().ForEach(filePath =>
            {
                fileInfos.Add(FilesIoHelper.Instance.GetFileMetadata(filePath));
            });

            Assert.NotEmpty(fileInfos);
        }

        [Fact]
        public void GetMetadataOfNotExistingFile()
        {
            var file = FilesIoHelper.Instance.GetFileMetadata(Path.Combine(LocalizationStringsProvider.ApplicationLocalization, "NotExistringFile.exe"));
            Assert.Null(file);
        }
    }
}
