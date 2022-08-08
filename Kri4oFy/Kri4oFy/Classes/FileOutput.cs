using Kri4oFy.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kri4oFy.Classes
{
    internal class FileOutput : IOutput
    {
        private string filePath;
        StreamWriter sw;

        public FileOutput(string filePath)
        {
            this.filePath = filePath;
            this.sw = new StreamWriter(filePath);
        }

        public void Write(string output)
        {
            sw.Write(output);
        }

        public void WriteLine(string output)
        {
            sw.WriteLine(output);
        }
    }
}
