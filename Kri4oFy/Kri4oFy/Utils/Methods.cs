using Kri4oFy.Constants;
using Kri4oFy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kri4oFy.Utils
{
    public static class Methods
    {
        public static List<string> GetStringsFromList(string input)
        {
            List<string> strings = new List<string>();

            foreach (Match match in Regex.Matches(input, VariableConstants.arrNamesReg.ToString(), RegexOptions.None))
            {
                strings.Add(match.Groups[1].ToString());
            }
            return strings;
        }

        public static int IndexOfUser(string username, List<IUser> usersList)
        {
            int ind = -1;

            for (int i = 0; i < usersList.Count; i++)
            {
                if (usersList[i].Username == username)
                {
                    ind = i;

                    break;
                }
            }

            return ind;
        }

        public static int IndexOfAlbum(string albumName, List<IAlbum> albumsList)
        {
            int ind = -1;

            for (int i = 0; i < albumsList.Count; i++)
            {
                if (albumsList[i].CollectionName == albumName)
                {
                    ind = i;

                    break;
                }
            }

            return ind;
        }

        public static int IndexOfPlaylist(string playlistName, List<ISongCollection> playlistsList)
        {
            int ind = -1;

            for (int i = 0; i < playlistsList.Count; i++)
            {
                if (playlistsList[i].CollectionName == playlistName)
                {
                    ind = i;

                    break;
                }
            }

            return ind;
        }

        public static int IndexOfSong(string songName, List<ISong> songsList)
        {
            int ind = -1;

            for (int i = 0; i < songsList.Count; i++)
            {
                if (songsList[i].SongName == songName)
                {
                    ind = i;

                    break;
                }
            }

            return ind;
        }
    }
}
