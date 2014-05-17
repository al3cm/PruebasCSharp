using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperCalculator
{
    public interface FileHandler<T> where T : DataFile
    {
        T CreateFile(string path);
    }
}
