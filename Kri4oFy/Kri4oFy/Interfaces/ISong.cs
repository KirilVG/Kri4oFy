using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kri4oFy.Interfaces
{
    public interface ISong
    {
        string SongName { get; set; }
        
        int Time { get; set; }

        IAlbum Album { get; set; }

        string GetFileString { get; }
    }
}
