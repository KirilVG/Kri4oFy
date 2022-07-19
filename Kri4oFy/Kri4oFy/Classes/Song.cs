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

        //constructors
        public Song(string songName)
        {
            this.songName = songName;
            time = 0;
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
    }
}
