using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kri4oFy.Constants;
using Kri4oFy.Interfaces;

namespace Kri4oFy.Classes
{
    internal class Album : SongCollection, IAlbum
    {
        //fields
        private string artistName;

        private DateTime dateOfCreation;

        private GenreEnum genre;

        //constructors
        public Album(string AlbumName,string ArtistName) : base(AlbumName, SongCollectionTypeEnum.Album)
        {
            this.ArtistName = ArtistName;
            this.Genre = GenreEnum.none;
            this.DateOfCreation = DateTime.Today;
        }

        //properties
        public string ArtistName
        {
            get { return artistName; }
            set { artistName = value; }
        }

        public DateTime DateOfCreation
        {
            get { return dateOfCreation; }
            set { dateOfCreation = value; }
        }

        public GenreEnum Genre
        {
            get { return genre; }
            set { genre = value; }
        }

        new public string GetFileString
        {
            get { return $"<album><{base.CollectionName}>[{dateOfCreation.Year}](genre: [{Genre}])(songs: [{{{string.Join(", ", base.Songs.Select(x => $"'{x.SongName}'").ToArray())}}}])</album>"; }
        }
    }
}
