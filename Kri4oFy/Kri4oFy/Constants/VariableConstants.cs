using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kri4oFy.Constants
{
    static public class VariableConstants
    {
        static public Regex userReg = new Regex("<user><(\\w+)>\\((\\w+)\\){(\\w+)}<\\/user>");
        static public Regex listenerReg = new Regex("<listener><(\\w+)><([\\w ]+)>\\[([0-9]{2}/[0-9]{2}/[0-9]{4})\\]\\(genres: \\[([\\w', ]*)\\]\\)\\(likedSongs: \\[([\\w', ]*)\\]\\)\\(playlists: \\[([\\w', ]*)\\]\\)<\\/listener>");
        static public Regex artistReg = new Regex("<artist><(\\w+)><([\\w ]+)>\\[([0-9]{2}/[0-9]{2}/[0-9]{4})\\]\\(genres: \\[([\\w', ]*)\\]\\)\\(albums: \\[([\\w', ]*)]\\)</artist>");
        static public Regex albumReg = new Regex("<album><([\\w ]+)>\\[([0-9]{4})\\]\\(genres: \\[([\\w ]+)\\]\\)\\(songs: \\[([\\w', ]*)\\]\\)</album>");
        static public Regex playlistReg = new Regex("<playlist><(\\w+)>\\(songs: \\[([\\w', ]*)\\]\\)<\\/playlists>");
        static public Regex songReg = new Regex("<song><([\\w ]+)>\\[([0-9]+):([0-9]{2})\\]</song>");
        static public Regex arrNamesReg = new Regex("'([\\w ]+)'");
    }
}
