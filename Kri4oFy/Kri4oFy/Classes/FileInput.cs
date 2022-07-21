using Kri4oFy.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kri4oFy.Classes
{
    internal class FileInput : IInput
    {
        private string filePath;
        StreamReader sr;
        public FileInput(string filePath)
        {
            this.filePath = filePath;
            sr = new StreamReader(filePath);
        }
        public int Read()
        {
            return sr.Read();
        }

        public string ReadLine()
        {
            return sr.ReadLine();
        }

        ~FileInput()
        {
            sr.Close();
        }
    }
}
