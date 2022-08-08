using Kri4oFy.Interfaces;
using Kri4oFy.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kri4oFy.Classes
{
    internal class PlayList : SongCollection
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="PlayListName">name of the plalist</param>
        public PlayList(string PlayListName)
            : base(PlayListName, SongCollectionTypeEnum.PlayList)
        {

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
        public override string GetFileString
        {
            get 
            { 
                return $"<playlists>" +
                    $"<{base.CollectionName}>" +
                    $"(songs: [{GetSongsAsString()}])" +
                    $"</playlists>"; 
            }
        }
    }
}
