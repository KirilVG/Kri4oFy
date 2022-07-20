using Kri4oFy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kri4oFy.Classes
{
    public class Song : ISong
    {
        //fields
        private string songName;
        private int time;
        private IAlbum album;

        //constructors
        public Song(string songName,int time=0, IAlbum album=null)
        {
            this.SongName = songName;
            this.Time = time;
            this.Album = album;
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

        public string GetFileString
        {
            get
            {
                return $"<song>" +
                    $"<{SongName}>" +
                    $"[6:28]" +
                    $"</song>";
            }
        }
    }
}
