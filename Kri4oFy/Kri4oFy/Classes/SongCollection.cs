using Kri4oFy.Constants;
using Kri4oFy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kri4oFy.Classes
{
    public class SongCollection : ISongCollection
    {
        //fields
        private string collectionName;
        private SongCollectionTypeEnum type;
        private List<ISong> songs;

        //constructors
        public SongCollection(string collectionName)
        {
            this.CollectionName = collectionName;
            songs= new List<ISong>();
            type = SongCollectionTypeEnum.Default;
        }

        public SongCollection(string collectionName,SongCollectionTypeEnum type)
        {
            this.CollectionName = collectionName;
            songs = new List<ISong>();
            this.Type = type;
        }

        //properties
        public string CollectionName
        {
            get { return collectionName; }
            set { collectionName = value; }
        }
        public SongCollectionTypeEnum Type
        {
            get { return type; }
            set { type = value; }
        }

        public int Length
        {
            get
            {
                int counter = 0;

                foreach (ISong song in songs)
                {
                    counter += song.Time;
                }

                return counter;
            }
        }

        public List<ISong> Songs
        {
            get { return songs; }
        }

        //methods
        public string GetFileString => throw new NotImplementedException();

        public string GetInfo
        {
            get
            {
                return $"<{type}> - {collectionName} length:{Length}";
            }
        }

        public string GetSongsInfo
        {
            get
            {
                return String.Join("\n",Songs.Select(x=>$"<song> {x.SongName} [{x.Time/60}:{x.Time%60:D2}]"));
            }
        }

        public bool AddSong(ISong song)
        {
            if(songs.Contains(song))
            {
                throw new ArgumentException("The song already exists ion this collection");
                return false;
            }
            else
            {
                songs.Add(song);
                return true;
            }
        }

        public int IndexOfSongWithName(string songName)
        {
            int index = -1;

            for (int i = 0; i < songs.Count; i++)
            {
                if (songs[i].SongName == songName)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        public ISong RemoveSongByName(string songName)
        {
            ISong song = null;

            foreach(ISong songElement in Songs)
            {
                if(songElement.SongName == songName)
                {
                    song = songElement;
                    break;
                }
            }

            if(song==null)
            {
                throw new ArgumentException("The song does not Exist in the current collection");
            }
            else
            {
                songs.Remove(song);
                return song;
            }
        }
    }
}
