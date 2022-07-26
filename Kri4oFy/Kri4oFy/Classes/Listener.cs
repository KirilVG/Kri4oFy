using Kri4oFy.Constants;
using Kri4oFy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kri4oFy.Classes
{
    internal class Listener : User, IListener
    {
        //fields
        private string fullName;

        private DateTime dateOfBirth;

        private List<GenreEnum> genres;

        private List<ISong> likedSongs;

        private List<IPlayList> playLists;

        //constructor
        public Listener(User user, string fullName = "")
            : base(user.Username, user.Password, user.Type)
        {
            this.fullName = fullName; ;
            this.DateOfBirth = DateTime.Today;
            this.genres = new List<GenreEnum>();
            this.likedSongs = new List<ISong>();
            this.playLists = new List<IPlayList>();
        }

        //properties
        private string GetDateInFormat()
        {
            return dateOfBirth.ToString("dd/mm/yyyy");
        }

        private string GetGenresAsString()
        {
            return String.Join(", ", Genres.Select(x => $"'{x}'"));
        }

        private string GetLikedSongsAsString()
        {
            return String.Join(", ", likedSongs.Select(x => $"'{x.SongName}'"));
        }

        private string GetPlayListsAsString()
        {
            return String.Join(", ", playLists.Select(x => $"'{x.CollectionName}'"));
        }
        public override string GetFileString
        {
            get
            {
                return $"<listener>" +
                    $"<{Username}>" +
                    $"<{FullName}>" +
                    $"[{GetDateInFormat()}]" +
                    $"(genres: [{GetGenresAsString()}])" +
                    $"(likedSongs: [{GetLikedSongsAsString()}])" +
                    $"(playlists: [{GetPlayListsAsString()}])</listener>";
            }
        }

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

        public List<ISong> LikedSongs
        {
            get { return likedSongs; }
        }

        public List<IPlayList> Playlists
        {
            get { return playLists; }
        }

        //methods
        private IPlayList GetPlayListByName(string playListName)
        {
            if (playListName == null)
            {
                throw new ArgumentException("playList should not be null");
                //return null;
            }

            IPlayList playList = null;

            foreach (IPlayList playListItem in playLists)
            {
                if (playListItem.CollectionName == playListName)
                {
                    playList = playListItem;
                    break;
                }
            }

            if (playList == null)
            {
                throw new ArgumentException("This playlist id not part of this listers menagerie");
                //return null;
            }
            else
            {
                return playList;
            }
        }
        public bool AddPlayList(IPlayList playList)
        {
            if (playList == null)
            {
                throw new ArgumentException("The playlist can not be null");
                //return false;
            }
            else if (playLists.Contains(playList))
            {
                throw new ArgumentException("This playList already exists in this listener's menagerie");
                //return false;
            }
            else
            {
                playLists.Add(playList);
                return true;
            }
        }

        public bool AddSongToFavourites(ISong song)
        {
            if (song == null)
            {
                throw new ArgumentException("Song should not be null");
                //return false;
            }
            else if (likedSongs.Contains(song))
            {
                throw new ArgumentException("This song already exists in this playlist");
            }
            else
            {
                likedSongs.Add(song);

                return true;
            }
        }

        public bool AddSongToPlayList(string playListName, ISong song)
        {
            if (playListName == null || song == null)
            {
                throw new ArgumentException("Arguments should not be null");
                //return false;
            }

            IPlayList playList = GetPlayListByName(playListName);

            if (playList == null)
            {
                throw new ArgumentException("This playlist id not part of this listers menagerie");
                //return false;
            }
            else
            {
                playList.AddSong(song);
                return true;
            }
        }

        public string PrintFavouriteSongs()
        {
            return String.Join("\n", likedSongs.Select(x => $"<song> {x.SongName} [{x.Time / 60}:{x.Time % 60:D2}]"));
        }

        public string PrintPlayLists()
        {
            return String.Join("\n", playLists.Select(x => x.GetInfo));
        }

        public string PrintSongsFromPlayList(string playListName)
        {
            if (playListName == null)
            {
                throw new ArgumentException("Playlist name should not be null");
                //return "";
            }

            IPlayList playList = GetPlayListByName(playListName);

            if (playList == null)
            {
                throw new ArgumentException("This playList is not a part of this Listener's menagerie");
                //return "";
            }
            else
            {
                return playList.GetSongsInfo;
            }
        }

        public IPlayList RemovePlayList(string playListName)
        {
            if (playListName == null)
            {
                throw new ArgumentException("Playlist name should not be null");
                //return null;
            }

            IPlayList playList = GetPlayListByName(playListName);

            if (playList == null)
            {
                throw new ArgumentException("This playList is not a part of this Listener's menagerie");
                //return null;
            }
            else
            {
                playLists.Remove(playList);

                return playList;
            }
        }

        public bool RemoveSongFromFavourites(ISong song)
        {
            if (song == null)
            {
                throw new ArgumentException("Song can not be null");
                //return false;
            }
            else if (!likedSongs.Contains(song))
            {
                throw new ArgumentException("this song is not a part of this listeners menagerie");
                //return false;
            }
            else
            {
                likedSongs.Remove(song);
                return true;
            }
        }

        public bool RemoveSongFromPlayList(string playListName, string songName)
        {
            if (playListName == null || songName == null)
            {
                throw new ArgumentException("Playlist name should not be null");
                //return false;
            }

            IPlayList playList = GetPlayListByName(playListName);

            if (playList == null)
            {
                throw new ArgumentException("This playList is not a part of this Listener's menagerie");
                //return null;
            }
            else
            {
                playList.RemoveSongByName(songName);

                return true;
            }
        }
    }
}
