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
        private string artistName;

        private DateTime dateOfCreation;

        private GenreEnum genre;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="AlbumName">name of the album</param>
        /// <param name="ArtistName">name of the artist behind the album</param>
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
        
        new public string GetFileString
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
