using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperCalculator
{
    public class UsersStorageManager
    {
        FileHandler<UserFile> _handler;

        public UsersStorageManager(FileHandler<UserFile> handler)
        {
            _handler = handler;
        }

        public void SetUsersFile(string path)
        {
            _handler.CreateFile(path);
        }

    }
}
