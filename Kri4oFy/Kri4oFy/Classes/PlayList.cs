using Kri4oFy.Interfaces;
using Kri4oFy.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kri4oFy.Classes
{
    internal class PlayList:SongCollection ,IPlayList
    {
        //constructors
        public PlayList(string PlayListName)
            : base(PlayListName,SongCollectionTypeEnum.PlayList)
        {

        }

        new public string GetFileString
        {
            get { return $"//<playlists><{base.CollectionName}>(songs: [{string.Join(", ",base.Songs.Select(x=>x.SongName).ToArray())}])</playlists>"; }
        }


    }
}
