using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SuperCalculator;
using System.IO;
using Rhino.Mocks;


namespace IntegrationTests
{
    [TestFixture]
    public class FileHandlerTests
    {
        private string getPlatformPath()
        {
            System.OperatingSystem osInfo = System.Environment.OSVersion;
            string path = String.Empty;
            switch (osInfo.Platform)
            {
                case PlatformID.MacOSX:
                    path = "/tmp/data.txt";
                    break;
                case PlatformID.Unix:
                    path = "/tmp/data.txt";
                    break;
                default:
                    path = @"D:\data.txt";
                    break;
            }
            return path;
        }

        [Test]
        public void CreateFileMultiPlatform()
        {
            string path = new EasyPath().GetPlatformPath();
            UserFileHandler handler = new UserFileHandler();
            DataFile dataFile = handler.CreateFile(path);

            if (!File.Exists(path))
                Assert.Fail("File was not created");

            Assert.IsNotNull(dataFile.Stream);
        }

        [Test]
        [ExpectedException(typeof(DirectoryNotFoundException))]
        public void CreateFileDirectoryNotFound()
        {
            string path = new NotFoundPath().GetPlatformPath();
            
            UserFileHandler handler = new UserFileHandler();

            DataFile dataFile = handler.CreateFile(path);
        }

    }
}
