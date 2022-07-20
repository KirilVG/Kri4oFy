using Kri4oFy.Constants;
using Kri4oFy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kri4oFy.Classes
{
    public class Artist : User, IArtist
    {
        //fields
        string fullName;
        DateTime dateOfBirth;
        List<GenreEnum> genres;
        List<IAlbum> albums;
        public Artist(User user,string fullName="") : base(user.Username, user.Password, user.Type)
        {
            this.fullName = fullName; ;
            this.DateOfBirth = DateTime.Today;
            this.genres = new List<GenreEnum>();
            this.albums = new List<IAlbum>();
        }

        //properties
        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }
        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        public List<GenreEnum> Genres
        {
            get { return genres; }
        }

        public List<IAlbum> Albums
        {
            get { return albums; }
        }

        public string GetFileString
        {
            get 
            { 
                return $"<artist>" +
                    $"<{Username}>" +
                    $"<{FullName}>" +
                    $"[{GetDateInFormat()}]" +
                    $"(genres: [{GetGenresAsString()}])" +
                    $"(albums: [{GetAlbumNamesAsString()}])" +
                    $"</artist>"; 
            }
        }

        //methods
        private string GetAlbumNamesAsString()
        {
            return string.Join(", ", Albums.Select(x => $"'{x.CollectionName}'").ToArray());
        }
        private string GetDateInFormat()
        {
            return dateOfBirth.ToString("dd/mm/yyyy");
        }
        private string GetGenresAsString()
        {
            return String.Join(", ", Genres.Select(x => $"'{x}'"));
        }
        public void AddAlbum(IAlbum album)
        {
            throw new NotImplementedException();
        }

        public void AddSongToAlbum(string albumName, ISong song)
        {
            throw new NotImplementedException();
        }

        public void PrintAlbumContent(string albumName)
        {
            throw new NotImplementedException();
        }

        public void PrintAlbums()
        {
            throw new NotImplementedException();
        }

        public IAlbum RemoveAlbum(string albumName)
        {
            throw new NotImplementedException();
        }

        public ISong RemoveSongFromAlbum(string albumName, string songName)
        {
            throw new NotImplementedException();
        }
    }
}
