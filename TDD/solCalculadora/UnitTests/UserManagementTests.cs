using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using SuperCalculator;
using System.IO;

namespace UnitTests
{
    [TestFixture]
    class UserManagementTests
    {
        [Test]
        public void ConfigUsersFile()
        {
            string filePath = "/home/carlosble/data.txt";
            FileHandler<UserFile> handlerMock = MockRepository.GenerateMock<FileHandler<UserFile>>();
            handlerMock.Expect(x => x.CreateFile(filePath)).Return(new UserFile());
                        
            UsersStorageManager manager = new UsersStorageManager(handlerMock);
            manager.SetUsersFile(filePath);

            handlerMock.VerifyAllExpectations();
        }

        [Test]
        [ExpectedException(typeof(DirectoryNotFoundException))]
        public void TryCreateFilewhenDirectoryNotFound()
        {
            FileHandler<UserFile> handlerMock = MockRepository.GenerateStub<FileHandler<UserFile>>();
            handlerMock.Expect(x => x.CreateFile("")).Throw(new DirectoryNotFoundException());

            UsersStorageManager manager = new UsersStorageManager(handlerMock);
            manager.SetUsersFile("");
        }

      

    }
}
