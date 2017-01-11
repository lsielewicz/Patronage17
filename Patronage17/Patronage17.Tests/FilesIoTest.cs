using Patronage17.Engine.Helpers;
using Patronage17.Tests.Helpers;
using System;
using System.IO;
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


    }
}
