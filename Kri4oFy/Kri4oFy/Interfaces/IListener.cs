using Kri4oFy.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kri4oFy.Interfaces
{
    internal interface IListener:IUser
    {
        string FullName { get; set; }

        DateTime DateOfBirth { get; set; }

        List<GenreEnum> Genres { get; }

        List<ISong> LikedSongs { get; }

        List<IPlayList> Playlists { get; }


        string PrintPlayLists();

        string PrintFavouriteSongs();

        string PrintSongsFromPlayList(string PlayListName);

        bool AddPlayList(IPlayList playList);

        IPlayList RemovePlayLisr(IPlayList playList);

        bool AddSongToFavourites(ISong song);

        bool RemoveSongFromFavourites(ISong song);

        bool AddSongToPlayList(string playListName, ISong song);

        bool RemoveSongFromPlayList(string playlistName, ISong song);
    }
}
