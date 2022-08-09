using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kri4oFy.Interfaces
{
    public interface ISpData
    {
        List<IUser> Users { get; set; }

        List<IAlbum> Albums { get; set; }  

        List<ISongCollection> Playlists { get; set; }

        List<ISong> Songs { get; set; }
    }
}
