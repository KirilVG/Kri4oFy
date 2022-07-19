using Kri4oFy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kri4oFy.Classes
{
    internal class Song : ISong
    {
        //fields
        private string songName;
        private int time;
        private IAlbum album;

        //constructors
        public Song(string songName)
        {
            this.songName = songName;
            time = 0;
            album = null;
        }

        //properties
        public string SongName 
        {
            get { return songName; } 
            set { songName = value; } 
        }
        public int Time
        {
            get { return time; }
            set { time = Math.Abs(value); }
        }

        public IAlbum Album
        {
            get { return album; }
            set { album = value; }
        }
    }
}
