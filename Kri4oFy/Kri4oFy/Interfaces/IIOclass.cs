using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kri4oFy.Interfaces
{
    internal interface IIOclass
    {
        void Write(string output);

        void WriteLine(string output);

        string Read();

        string ReadLine();
    }
}
