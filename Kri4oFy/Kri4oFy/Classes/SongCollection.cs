using Kri4oFy.Constants;
using Kri4oFy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kri4oFy.Classes
{
    internal class SongCollection : ISongCollection
    {
        //fields
        private string collectionName;
        private SongCollectionTypeEnum type;
        private List<ISong> songs;


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

                foreach (Song song in songs)
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

        public string GetFileString => throw new NotImplementedException();
    }
}
