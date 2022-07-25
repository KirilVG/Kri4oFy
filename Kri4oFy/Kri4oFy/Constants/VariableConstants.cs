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
        //regular expresions
        static public Regex userReg = new Regex("<user><(\\w+)>\\((\\w+)\\){(\\w+)}<\\/user>");

        static public Regex listenerReg = new Regex("<listener><(\\w+)><([\\w ]+)>\\[([0-9]{2}/[0-9]{2}/[0-9]{4})\\]\\(genres: \\[([\\w', ]*)\\]\\)\\(likedSongs: \\[([\\w', ]*)\\]\\)\\(playlists: \\[([\\w', ]*)\\]\\)<\\/listener>");

        static public Regex artistReg = new Regex("<artist><(\\w+)><([\\w ]+)>\\[([0-9]{2}/[0-9]{2}/[0-9]{4})\\]\\(genres: \\[([\\w', ]*)\\]\\)\\(albums: \\[([\\w', ]*)]\\)</artist>");

        static public Regex albumReg = new Regex("<album><([\\w ]+)>\\[([0-9]{4})\\]\\(genre: \\[([\\w ]+)\\]\\)\\(songs: \\[([\\w', ]*)\\]\\)</album>");

        static public Regex playlistReg = new Regex("<playlist><(\\w+)>\\(songs: \\[([\\w', ]*)\\]\\)<\\/playlist>");

        static public Regex songReg = new Regex("<song><([\\w ]+)>\\[([0-9]+):([0-9]{2})\\]</song>");

        static public Regex arrNamesReg = new Regex("'([\\w ]+)'");

        //commands
        public const string exitCommand = "Exit";
        public const string logInCommand = "Log In";
        public const string logOutCommand = "Log Out";
        public const string saveCommand = "Save";
        public const string printInfoCommand = "Print Information";
        public const string printAlbumsCommand = "Print Albums";
        public const string printAlbumContentCommand = "Print the content of an album";
        public const string addAlbumCommand = "Add Album";
        public const string removeAlbumCommand = "Remove Album";
        public const string addSongToAlbumCommand = "Add Song to Album";
        public const string removeSongFromAlbumCommand = "Remove Song from Album";
        public const string printPlaylistsCommand = "Print Playlists";
        public const string printFavuriteSongsCommand = "Print Favourite Songs";
        public const string printSongsFromPlaylistCommand = "Print the content of a playlist";
        public const string addPlaylistCommand = "Add Playlist";
        public const string removePlaylistCommand = "Remove Playlist";
        public const string addSongToFavouritesCommand = "Add Song to Favourites";
        public const string addSongToPlaylistCommand = "Add song to playlist";
        public const string removeSongFromFavouritesCommand = "Remove Song From Favourites";
        public const string removeSongFromPlaylistCommand = "Remove Song from Playlist";
        public const string helpCommand = "Help";

        //messages
        public const string wrongCommandMSG = "This command has not been implemented yet";
        public const string userMustLogInMSG = "A user must log in first";
        public const string userMustLogOutMSG = "The current user must log out first";
        public const string inputUsernameMSG = "Username:";
        public const string inputPasswordMSG = "Password:";
        public const string inputAlbumNameMSG = "Input the name of the album";
        public const string inputPlaylistNameMSG = "Input the name of the playlist";
        public const string inputYearMSG = "Input the year of creation";
        public const string inputGenreMSG = "Input the genre";
        public const string inputSongNameMSG = "Input the name of the song";
        public const string inputSonglengthMSG = "Input the length of the song";
        public const string incorrectLogInInfoMSG = "Incorrect username or password";
        public const string noUserCurrentlyLoggedInMSG = "No user is currently logged in";
        public const string successfulyLoggedOutMSG = "User has successfuly logged out";
        public const string successfulyLoggedInMSG = "User has successfuly logged in";
        public const string userMustBeArtistMSG = "The user must be an artist";
        public const string userMustBeListenerMSG = "The user must be a listener";
        public const string albumWithNameExistsMSG = "An album with this name already exists";
        public const string albumWithNameDoesNotExistsMSG = "No album with this name does not exists";
        public const string successfulyAddedAlbumMSG = "Successfuly added the album";
        public const string successfulyRemovedAlbumMSG = "Successfuly removed the album";
        public const string songWithNameExistsMSG = "A song with this name already exists";
        public const string songWithNameDoesNotExistMSG = "A song with this name does not exists";
        public const string successfulyAddedSongMSG = "Successfuly added the song";
        public const string successfulyAddedPlaylistMSG = "Successfuly added the playlist";
        public const string successfulyRemovedSongMSG = "Successfuly removed the song";
        public const string successfulyRemovedPlaylistMSG = "Successfuly removed the playlist";
    }
}
