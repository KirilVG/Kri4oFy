using Kri4oFy.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kri4oFy.Interfaces
{
    internal interface IArtist:IUser
    {
        string FullName { get; set; }

        DateTime DateOfBirth { get; set; }

        List<GenreEnum> Genres { get; }

        List<IAlbum> Albums { get; }

        void PrintAlbums();

        void PrintAlbumContent(string albumName);

        void AddAlbum(IAlbum album);

        IAlbum RemoveAlbum(string albumName);

        void AddSongToAlbum(string albumName,ISong song);

        ISong RemoveSongFromAlbum(string albumName,string songName);
    }
}
