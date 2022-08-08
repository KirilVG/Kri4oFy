using Kri4oFy.Classes;
using Kri4oFy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kri4oFy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IInputOutput comunicator = new ConsoleIO();
            ISpotifyApp Kri4oFy = new SpotifyApp(comunicator, "SpotifyData.txt");
            Kri4oFy.Run();
        }
    }
}
