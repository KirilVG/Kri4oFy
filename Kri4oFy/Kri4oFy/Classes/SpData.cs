using Kri4oFy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kri4oFy.Classes
{
    public class SpData : ISpData
    {
        public List<IUser> Users { get; set; }

        public List<IAlbum> Albums { get; set; }

        public List<ISongCollection> Playlists { get; set; }

        public List<ISong> Songs { get; set; }

        SpData(List<IUser> users, List<IAlbum> albums, List<ISongCollection> playlists, List<ISong> songs)
        {
            Users = users;
            Albums = albums;
            Playlists = playlists;
            Songs = songs;
        }

        public SpData()
        {
            Users = new List<IUser>();

            Albums = new List<IAlbum>();

            Playlists = new List<ISongCollection>();

            Songs = new List<ISong> { };
        }
    }
}
