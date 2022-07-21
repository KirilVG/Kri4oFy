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

        //constructor
        public Artist(User user, string fullName = "") : base(user.Username, user.Password, user.Type)
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

        new public string GetFileString
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

        public bool AddAlbum(IAlbum album)
        {
            if (albums.Contains(album))
            {
                throw new ArgumentException("Album is already a part of this artists managery");

                return false;
            }
            else
            {
                albums.Add(album);

                album.Artist = this;

                return true;
            }
        }

        public bool AddSongToAlbum(string albumName, ISong song)
        {
            IAlbum album = null;

            foreach (IAlbum albumItem in albums)
            {
                if (albumItem.CollectionName == albumName)
                {
                    album = albumItem;
                }
            }

            if (album == null)
            {
                throw new ArgumentException("Incorrect album name");

                return false;
            }
            else
            {
                album.AddSong(song);
                song.Album = album;
                return true;
            }
        }

        public string PrintAlbumContent(string albumName)
        {
            IAlbum album = null;

            foreach (IAlbum albumItem in albums)
            {
                if (albumItem.CollectionName == albumName)
                {
                    album = albumItem;
                }
            }

            if (album == null)
            {
                throw new ArgumentException("The album does not belong to this artist");

                return "";
            }
            else
            {
                return album.GetSongsInfo;
            }
        }

        public string PrintAlbums()
        {
            return String.Join("\n", Albums.Select(x => x.CollectionName));
        }

        public IAlbum RemoveAlbum(string albumName)
        {
            IAlbum album = null;

            foreach (IAlbum albumItem in albums)
            {
                if (albumItem.CollectionName == albumName)
                {
                    album = albumItem;
                }
            }

            if (album == null)
            {
                throw new ArgumentException("The album does not belong to this artist");

                return null;
            }
            else
            {
                this.Albums.Remove(album);

                return album;
            }
        }

        public ISong RemoveSongFromAlbum(string albumName, string songName)
        {
            IAlbum album = null;

            foreach (IAlbum albumItem in albums)
            {
                if (albumItem.CollectionName == albumName)
                {
                    album = albumItem;
                }
            }

            if (album == null)
            {
                throw new ArgumentException("The album does not belong to this artist");

                return null;
            }
            else
            {
                return album.RemoveSongByName(songName);
            }
        }
    }
}
