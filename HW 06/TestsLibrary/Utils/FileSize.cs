using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsLibrary.Utils
{
    public class FileSize
    {
        public static int GetFileSizeInKB(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open);
            return (int)fs.Length/1024;
        }
    }
}
