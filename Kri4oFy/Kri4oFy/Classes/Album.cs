using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kri4oFy.Constants;
using Kri4oFy.Interfaces;

namespace Kri4oFy.Classes
{
    public class Album : SongCollection, IAlbum
    {
        //fields
        private IArtist artist;

        private DateTime dateOfCreation;

        private GenreEnum genre;

        public Album(string AlbumName,IArtist artist=null) : base(AlbumName, SongCollectionTypeEnum.Album)
        {
            this.Artist= artist;
            this.Genre = GenreEnum.none;
            this.DateOfCreation = DateTime.Today;
        }

        //properties
        public IArtist Artist
        {
            get { return artist; }
            set { artist = value; }
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

        /// <summary>
        /// returns the collection of song names as a single sting
        /// </summary>
        /// <returns></returns>
        private string GetSongsAsString()
        {
            return string
                .Join(", ", base.Songs.Select(x => $"'{x.SongName}'")
                .ToArray());
        }
        
        public override string GetFileString
        {
            get 
            { 
                return $"<album>" +
                    $"<{base.CollectionName}>" +
                    $"[{dateOfCreation.Year}]" +
                    $"(genre: [{Genre}])" +
                    $"(songs: [{{{GetSongsAsString()}}}])" +
                    $"</album>"; 
            }
        }
    }
}
