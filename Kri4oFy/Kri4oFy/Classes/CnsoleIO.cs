using Kri4oFy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kri4oFy.Classes
{
    internal class CnsoleIO : IInputOutput

    {
        public int Read()
        {
            return Console.Read();
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Write(string output)
        {
            Console.Write(output);
        }

        public void WriteLine(string output)
        {
            Console.WriteLine(output);
        }
    }
}
