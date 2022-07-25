using Kri4oFy.Interfaces;
using Kri4oFy.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;

namespace Kri4oFy.Classes
{
    internal class SpotifyApp : ISpotifyApp
    {
        IInputOutput comunicator;

        string filePath;

        List<IUser> users;

        List<IAlbum> albums;

        List<IPlayList> playlists;

        List<ISong> songs;

        IUser currentUser;

        public SpotifyApp(IInputOutput Comunicator, string filePath)
        {
            this.comunicator = Comunicator;

            this.filePath = filePath;

            this.users = new List<IUser>();

            this.albums = new List<IAlbum>();

            this.playlists = new List<IPlayList>();

            this.songs = new List<ISong>();

            currentUser = null;
        }

        public void Run()
        {
            TakeFromFile();

            string command = "";

            while (command != VariableConstants.exitCommand)
            {
                command = comunicator.ReadLine();
                try
                {
                    switch (command)
                    {
                        case VariableConstants.logInCommand:
                            LogInFunc();
                            break;

                        case VariableConstants.logOutCommand:
                            LogOutFunc();
                            break;

                        case VariableConstants.printInfoCommand:
                            PrintUserInformationFunc();
                            break;

                        case VariableConstants.saveCommand:
                            SaveInFile();
                            break;

                        case VariableConstants.printAlbumsCommand:
                            PrintAlbums();
                            break;

                        case VariableConstants.printAlbumContentCommand:
                            PrintAlbumContentFunc();
                            break;

                        case VariableConstants.addAlbumCommand:
                            AddAlbumFunc();
                            break;

                        case VariableConstants.removeAlbumCommand:
                            RemoveAlbumFunc();
                            break;

                        case VariableConstants.addSongToAlbumCommand:
                            AddSongToAlbumFunc();
                            break;

                        case VariableConstants.removeSongFromAlbumCommand:
                            RemoveSongFromAlbumFunc();
                            break;

                        case VariableConstants.printPlaylistsCommand:
                            PrintPlaylistsFunc();
                            break;

                        case VariableConstants.printFavuriteSongsCommand:
                            PrintFavouriteSongsFunc();
                            break;

                        case VariableConstants.printSongsFromPlaylistCommand:
                            PrintSongsFromPlaylistFunc();
                            break;

                        case VariableConstants.addPlaylistCommand:
                            AddPlaylistFunc();
                            break;

                        case VariableConstants.removePlaylistCommand:
                            RemovePlaylistFunc();
                            break;

                        case VariableConstants.addSongToFavouritesCommand:
                            AddSongToFavouritesFunc();
                            break;

                        case VariableConstants.addSongToPlaylistCommand:
                            AddSongToPlaylistFunc();
                            break;

                        case VariableConstants.removeSongFromPlaylistCommand:
                            RemoveSongFromPlaylist();
                            break;

                        case VariableConstants.removeSongFromFavouritesCommand:
                            RemoveSongFromFavourites();
                            break;

                        case VariableConstants.helpCommand:
                            PreviewPossibleCommands();
                            break;

                        default:
                            WrongCommandFunc();
                            break;
                    }
                }
                catch (Exception e)
                {
                    comunicator.WriteLine(e.Message);
                }

            }

            //SaveInFile();
        }

        private void RemoveSongFromFavourites()
        {
            if (currentUser == null)
            {
                comunicator.WriteLine(VariableConstants.userMustLogInMSG);
            }
            else if (currentUser.Type != UserTypeEnum.listener)
            {
                comunicator.WriteLine(VariableConstants.userMustBeListenerMSG);
            }
            else
            {
                string songName;

                comunicator.WriteLine(VariableConstants.inputSongNameMSG);

                songName = comunicator.ReadLine();

                int songInd = IndexOfSong(songName, songs);

                if (songInd == -1)
                {
                    comunicator.WriteLine(VariableConstants.songWithNameDoesNotExistMSG);
                }
                else
                {
                    ((IListener)currentUser).RemoveSongFromFavourites(songs[songInd]);

                    comunicator.WriteLine(VariableConstants.successfulyRemovedSongMSG);
                }
            }
        }

        private void RemoveSongFromPlaylist()
        {
            if (currentUser == null)
            {
                comunicator.WriteLine(VariableConstants.userMustLogInMSG);
            }
            else if (currentUser.Type != UserTypeEnum.listener)
            {
                comunicator.WriteLine(VariableConstants.userMustBeListenerMSG);
            }
            else
            {
                string playlistName;
                comunicator.WriteLine(VariableConstants.inputPlaylistNameMSG);
                playlistName = comunicator.ReadLine();

                string songName;
                comunicator.WriteLine(VariableConstants.inputSongNameMSG);
                songName = comunicator.ReadLine();

                ((IListener)currentUser).RemoveSongFromPlayList(playlistName, songName);

                comunicator.WriteLine(VariableConstants.successfulyRemovedSongMSG);
            }
        }

        private void AddSongToPlaylistFunc()
        {
            if (currentUser == null)
            {
                comunicator.WriteLine(VariableConstants.userMustLogInMSG);
            }
            else if (currentUser.Type != UserTypeEnum.listener)
            {
                comunicator.WriteLine(VariableConstants.userMustBeListenerMSG);
            }
            else
            {
                string playlistName;
                comunicator.WriteLine(VariableConstants.inputPlaylistNameMSG);
                playlistName = comunicator.ReadLine();

                string songName;
                comunicator.WriteLine(VariableConstants.inputSongNameMSG);
                songName = comunicator.ReadLine();

                int songInd = IndexOfSong(songName, songs);

                if (songInd == -1)
                {
                    comunicator.WriteLine(VariableConstants.songWithNameDoesNotExistMSG);
                }
                else
                {
                    ((IListener)currentUser).AddSongToPlayList(playlistName, songs[songInd]);

                    comunicator.WriteLine(VariableConstants.successfulyAddedSongMSG);
                }
            }
        }

        private void AddSongToFavouritesFunc()
        {
            if (currentUser == null)
            {
                comunicator.WriteLine(VariableConstants.userMustLogInMSG);
            }
            else if (currentUser.Type != UserTypeEnum.listener)
            {
                comunicator.WriteLine(VariableConstants.userMustBeListenerMSG);
            }
            else
            {
                string songName;

                comunicator.WriteLine(VariableConstants.inputSongNameMSG);

                songName = comunicator.ReadLine();
                int songInd = IndexOfSong(songName, songs);

                if (songInd == -1)
                {
                    comunicator.WriteLine(VariableConstants.songWithNameDoesNotExistMSG);
                }
                else
                {
                    ((IListener)currentUser).AddSongToFavourites(songs[songInd]);

                    comunicator.WriteLine(VariableConstants.successfulyAddedSongMSG);
                }
            }
        }

        private void RemovePlaylistFunc()
        {
            if (currentUser == null)
            {
                comunicator.WriteLine(VariableConstants.userMustLogInMSG);
            }
            else if (currentUser.Type != UserTypeEnum.listener)
            {
                comunicator.WriteLine(VariableConstants.userMustBeListenerMSG);
            }
            else
            {
                string playlistName;

                comunicator.WriteLine(VariableConstants.inputPlaylistNameMSG);

                playlistName = comunicator.ReadLine();

                IPlayList playlist = ((IListener)currentUser).RemovePlayList(playlistName);

                playlists.Remove(playlist);
                comunicator.WriteLine(VariableConstants.successfulyRemovedPlaylistMSG);
            }
        }

        private void AddPlaylistFunc()
        {
            if (currentUser == null)
            {
                comunicator.WriteLine(VariableConstants.userMustLogInMSG);
            }
            else if (currentUser.Type != UserTypeEnum.listener)
            {
                comunicator.WriteLine(VariableConstants.userMustBeListenerMSG);
            }
            else
            {
                string playlistName;

                comunicator.WriteLine(VariableConstants.inputPlaylistNameMSG);

                playlistName = comunicator.ReadLine();

                IPlayList playlist = new PlayList(playlistName);

                ((IListener)currentUser).AddPlayList(playlist);

                playlists.Add(playlist);

                comunicator.WriteLine(VariableConstants.successfulyAddedPlaylistMSG);
            }
        }

        private void PrintSongsFromPlaylistFunc()
        {
            if (currentUser == null)
            {
                comunicator.WriteLine(VariableConstants.userMustLogInMSG);
            }
            else if (currentUser.Type != UserTypeEnum.listener)
            {
                comunicator.WriteLine(VariableConstants.userMustBeListenerMSG);
            }
            else
            {
                string playlistName;

                comunicator.WriteLine(VariableConstants.inputPlaylistNameMSG);

                playlistName = comunicator.ReadLine();

                comunicator.WriteLine(((IListener)currentUser).PrintSongsFromPlayList(playlistName));
            }
        }

        private void PrintFavouriteSongsFunc()
        {
            if (currentUser == null)
            {
                comunicator.WriteLine(VariableConstants.userMustLogInMSG);
            }
            else if (currentUser.Type != UserTypeEnum.listener)
            {
                comunicator.WriteLine(VariableConstants.userMustBeListenerMSG);
            }
            else
            {
                comunicator.WriteLine(((IListener)currentUser).PrintFavouriteSongs());
            }
        }

        private void PrintPlaylistsFunc()
        {
            if (currentUser == null)
            {
                comunicator.WriteLine(VariableConstants.userMustLogInMSG);
            }
            else if (currentUser.Type != UserTypeEnum.listener)
            {
                comunicator.WriteLine(VariableConstants.userMustBeListenerMSG);
            }
            else
            {
                comunicator.WriteLine(((IListener)currentUser).PrintPlayLists());
            }
        }

        private void RemoveSongFromAlbumFunc()
        {
            if (currentUser == null)
            {
                comunicator.WriteLine(VariableConstants.userMustLogInMSG);
            }
            else if (currentUser.Type != UserTypeEnum.artist)
            {
                comunicator.WriteLine(VariableConstants.userMustBeArtistMSG);
            }
            else
            {
                try
                {
                    string albumName;
                    comunicator.WriteLine(VariableConstants.inputAlbumNameMSG);
                    albumName = comunicator.ReadLine();

                    string songName;
                    comunicator.WriteLine(VariableConstants.inputSongNameMSG);
                    songName = comunicator.ReadLine();

                    ISong song = ((IArtist)currentUser).RemoveSongFromAlbum(albumName, songName);
                    songs.Remove(song);

                    foreach (IPlayList playlist in playlists)
                    {
                        if (playlist.Songs.Contains(song))
                        {
                            playlist.Songs.Remove(song);
                        }
                    }

                    foreach (IUser user in users)
                    {
                        if (user.Type == UserTypeEnum.listener && ((IListener)user).LikedSongs.Contains(song))
                        {
                            ((IListener)user).LikedSongs.Remove(song);
                        }
                    }

                    comunicator.WriteLine(VariableConstants.successfulyRemovedSongMSG);
                }
                catch (Exception e)
                {
                    comunicator.WriteLine(e.Message);
                }

            }
        }

        private void AddSongToAlbumFunc()
        {
            if (currentUser == null)
            {
                comunicator.WriteLine(VariableConstants.userMustLogInMSG);
            }
            else if (currentUser.Type != UserTypeEnum.artist)
            {
                comunicator.WriteLine(VariableConstants.userMustBeArtistMSG);
            }
            else
            {
                string albumName;
                comunicator.WriteLine(VariableConstants.inputAlbumNameMSG);
                albumName = comunicator.ReadLine();

                string songName;
                comunicator.WriteLine(VariableConstants.inputSongNameMSG);
                songName = comunicator.ReadLine();

                int songIdex = IndexOfSong(songName, songs);
                if (songIdex == -1)
                {

                    ISong song = new Song(songName);

                    comunicator.WriteLine(VariableConstants.inputSonglengthMSG);

                    int[] lengthinp = comunicator.ReadLine().Split(':').Select(x => int.Parse(x)).ToArray();
                    int length = lengthinp[0] * 60 + lengthinp[1];
                    song.Time = length;

                    ((IArtist)currentUser).AddSongToAlbum(albumName, song);

                    songs.Add(song);

                    comunicator.WriteLine(VariableConstants.successfulyAddedSongMSG);

                }
                else
                {
                    comunicator.WriteLine(VariableConstants.songWithNameExistsMSG);
                }


            }
        }

        private void RemoveAlbumFunc()
        {
            if (currentUser == null)
            {
                comunicator.WriteLine(VariableConstants.userMustLogInMSG);
            }
            else if (currentUser.Type != UserTypeEnum.artist)
            {
                comunicator.WriteLine(VariableConstants.userMustBeArtistMSG);
            }
            else
            {
                string albumName;

                comunicator.WriteLine(VariableConstants.inputAlbumNameMSG);

                albumName = comunicator.ReadLine();

                if (IndexOfAlbum(albumName, albums) == -1)
                {
                    comunicator.WriteLine(VariableConstants.albumWithNameDoesNotExistsMSG);
                }
                else
                {
                    IAlbum album = ((IArtist)currentUser).RemoveAlbum(albumName);

                    foreach (ISong song in album.Songs)
                    {
                        songs.Remove(song);

                        foreach (IPlayList playlist in playlists)
                        {
                            if (playlist.Songs.Contains(song))
                            {
                                playlist.Songs.Remove(song);
                            }
                        }

                        foreach (IUser user in users)
                        {
                            if (user.Type == UserTypeEnum.listener && ((IListener)user).LikedSongs.Contains(song))
                            {
                                ((IListener)user).LikedSongs.Remove(song);
                            }
                        }
                    }

                    albums.Remove(album);

                    comunicator.WriteLine(VariableConstants.successfulyRemovedAlbumMSG);
                }
            }
        }

        void AddAlbumFunc()
        {
            if (currentUser == null)
            {
                comunicator.WriteLine(VariableConstants.userMustLogInMSG);
            }
            else if (currentUser.Type != UserTypeEnum.artist)
            {
                comunicator.WriteLine(VariableConstants.userMustBeArtistMSG);
            }
            else
            {
                string albumName;

                comunicator.WriteLine(VariableConstants.inputAlbumNameMSG);

                albumName = comunicator.ReadLine();

                if (IndexOfAlbum(albumName, albums) == -1)
                {
                    IAlbum album = new Album(albumName, (IArtist)currentUser);

                    comunicator.WriteLine(VariableConstants.inputYearMSG);
                    album.DateOfCreation = DateTime.ParseExact($"01/01/{comunicator.ReadLine()}", "dd/mm/yyyy", CultureInfo.InvariantCulture);

                    comunicator.WriteLine(VariableConstants.inputGenreMSG);
                    album.Genre = (GenreEnum)Enum.Parse(typeof(GenreEnum), comunicator.ReadLine());

                    ((IArtist)currentUser).AddAlbum(album);

                    albums.Add(album);

                    comunicator.WriteLine(VariableConstants.successfulyAddedAlbumMSG);
                }
                else
                {
                    comunicator.WriteLine(VariableConstants.albumWithNameExistsMSG);
                }
            }
        }

        void PrintAlbumContentFunc()
        {
            if (currentUser == null)
            {
                comunicator.WriteLine(VariableConstants.userMustLogInMSG);
            }
            else if (currentUser.Type != UserTypeEnum.artist)
            {
                comunicator.WriteLine(VariableConstants.userMustBeArtistMSG);
            }
            else
            {
                string albumName;

                comunicator.WriteLine(VariableConstants.inputAlbumNameMSG);

                albumName = comunicator.ReadLine();

                comunicator.WriteLine(((IArtist)currentUser).PrintAlbumContent(albumName));
            }
        }

        void PrintAlbums()
        {
            if (currentUser == null)
            {
                comunicator.WriteLine(VariableConstants.userMustLogInMSG);
            }
            else if (currentUser.Type != UserTypeEnum.artist)
            {
                comunicator.WriteLine(VariableConstants.userMustBeArtistMSG);
            }
            else
            {
                comunicator.WriteLine(((IArtist)currentUser).PrintAlbums());
            }
        }

        void WrongCommandFunc()
        {
            comunicator.WriteLine(VariableConstants.wrongCommandMSG);
        }

        void LogInFunc()
        {
            if (currentUser != null)
            {
                comunicator.WriteLine(VariableConstants.userMustLogOutMSG);
            }
            else
            {
                string userName;
                string password;

                comunicator.WriteLine(VariableConstants.inputUsernameMSG);
                userName = comunicator.ReadLine();

                comunicator.WriteLine(VariableConstants.inputPasswordMSG);
                password = comunicator.ReadLine();

                foreach (IUser user in this.users)
                {
                    if (user.CheckLogInInfo(userName, password))
                    {
                        currentUser = user;
                        break;
                    }
                }

                if (currentUser == null)
                {
                    comunicator.WriteLine(VariableConstants.incorrectLogInInfoMSG);
                }
                else
                {
                    comunicator.WriteLine(VariableConstants.successfulyLoggedInMSG);
                }
            }
        }

        void LogOutFunc()
        {
            if (currentUser == null)
            {
                comunicator.WriteLine(VariableConstants.noUserCurrentlyLoggedInMSG);
            }
            else
            {
                currentUser = null;
                comunicator.WriteLine(VariableConstants.successfulyLoggedOutMSG);
            }
        }

        void PrintUserInformationFunc()
        {
            if (currentUser == null)
            {
                comunicator.WriteLine(VariableConstants.userMustLogInMSG);
            }
            else
            {
                comunicator.WriteLine(currentUser.getUserInformation());
            }
        }

        private void TakeFromFile()
        {
            if (!(File.Exists(filePath)))
            {
                using (FileStream fs = File.Create(filePath))
                {

                }
            }
            else
            {
                GetUsers();
                GetListeners();
                GetArtists();
                GetAlbums();
                GetPlayLists();
                GetSongs();
            }
        }

        private void GetUsers()
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Match m = VariableConstants.userReg.Match(line);
                    if (m.Success)
                    {
                        String username = m.Groups[1].ToString();
                        String password = m.Groups[2].ToString();

                        UserTypeEnum type = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), m.Groups[3].ToString());

                        if (IndexOfUser(username, users) == -1)
                        {
                            User newUser = new User(username, password, type);

                            users.Add(newUser);
                        }
                        else
                        {
                            throw new Exception("There already is a user with the same username");
                        }
                    }
                }
            }
        }

        private void GetListeners()
        {
            using (StreamReader sr = new StreamReader(filePath))
            {

                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Match m = VariableConstants.listenerReg.Match(line);
                    if (m.Success)
                    {
                        String username = m.Groups[1].ToString();
                        int userInd = IndexOfUser(username, users);
                        if (userInd == -1 || users[userInd].Type != UserTypeEnum.listener)
                        {
                            throw new Exception("a user with that username does not exist or isn't a listener");
                        }
                        else
                        {
                            IListener listener = new Listener((User)users[userInd]);
                            users[userInd] = listener;

                            listener.FullName = m.Groups[2].ToString();

                            listener.DateOfBirth = DateTime.ParseExact(m.Groups[3].ToString(), "dd/mm/yyyy", CultureInfo.InvariantCulture);

                            GetStringsFromList(m.Groups[4].ToString())
                                .ForEach(x => listener.Genres.Add((GenreEnum)Enum.Parse(typeof(GenreEnum), x)));

                            GetStringsFromList(m.Groups[5].ToString())
                                .ForEach(x =>
                                {
                                    ISong song;
                                    int index = IndexOfSong(x, songs);
                                    if (index == -1)
                                    {
                                        song = new Song(x);
                                        songs.Add(song);
                                    }
                                    else
                                    {
                                        song = songs[index];
                                    }
                                    listener.AddSongToFavourites(song);
                                });

                            GetStringsFromList(m.Groups[6].ToString())
                                .ForEach(x =>
                                {
                                    IPlayList playList;
                                    int index = IndexOfPlaylist(x, playlists);
                                    if (index == -1)
                                    {
                                        playList = new PlayList(x);
                                        playlists.Add(playList);
                                    }
                                    else
                                    {
                                        playList = playlists[index];
                                    }
                                    listener.AddPlayList(playList);
                                });
                        }
                    }
                }
            }
        }

        private void GetArtists()
        {
            using (StreamReader sr = new StreamReader(filePath))
            {

                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Match m = VariableConstants.artistReg.Match(line);
                    if (m.Success)
                    {
                        String username = m.Groups[1].ToString();

                        int userInd = IndexOfUser(username, users);

                        if (userInd == -1 || users[userInd].Type != UserTypeEnum.artist)
                        {
                            throw new Exception("a user with that username does not exist or isn't an artist");
                        }
                        else
                        {
                            IArtist artist = new Artist((User)users[userInd]);
                            users[userInd] = artist;

                            artist.FullName = m.Groups[2].ToString();

                            artist.DateOfBirth = DateTime.ParseExact(m.Groups[3].ToString(), "dd/mm/yyyy", CultureInfo.InvariantCulture);

                            GetStringsFromList(m.Groups[4].ToString())
                                .ForEach(x => artist.Genres.Add((GenreEnum)Enum.Parse(typeof(GenreEnum), x)));

                            GetStringsFromList(m.Groups[5].ToString())
                                .ForEach(x =>
                                {
                                    IAlbum album;
                                    int index = IndexOfAlbum(x, albums);
                                    if (index == -1)
                                    {
                                        album = new Album(x);
                                        albums.Add(album);
                                    }
                                    else
                                    {
                                        album = albums[index];
                                    }
                                    artist.AddAlbum(album);
                                });
                        }
                    }
                }
            }
        }

        private void GetAlbums()
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    Match m = VariableConstants.albumReg.Match(line);

                    if (m.Success)
                    {
                        String albumName = m.Groups[1].ToString();

                        int albumInd = IndexOfAlbum(albumName, albums);

                        if (albumInd == -1)
                        {
                            throw new Exception("This album is not connected with an artist.");
                        }
                        else
                        {
                            IAlbum album = albums[albumInd];

                            album.DateOfCreation = DateTime.ParseExact($"01/01/{m.Groups[2].ToString()}", "dd/mm/yyyy", CultureInfo.InvariantCulture);

                            album.Genre = (GenreEnum)Enum.Parse(typeof(GenreEnum), m.Groups[3].ToString());

                            GetStringsFromList(m.Groups[4].ToString())
                                .ForEach(x =>
                                {
                                    ISong song;
                                    int index = IndexOfSong(x, songs);
                                    if (index == -1)
                                    {
                                        song = new Song(x);
                                        songs.Add(song);
                                    }
                                    else
                                    {
                                        song = songs[index];
                                    }
                                    album.AddSong(song);
                                    song.Album = album;
                                });
                        }
                    }
                }
            }
        }

        private void GetPlayLists()
        {

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    Match m = VariableConstants.playlistReg.Match(line);

                    if (m.Success)
                    {
                        String playlistName = m.Groups[1].ToString();

                        int playlistInd = IndexOfPlaylist(playlistName, playlists);

                        if (playlistInd == -1)
                        {
                            throw new Exception("This playlist is not connected with a listener.");
                        }
                        else
                        {
                            IPlayList playlist = playlists[playlistInd];
                            //Console.WriteLine(m.Groups[2].ToString());
                            GetStringsFromList(m.Groups[2].ToString())
                                .ForEach(x =>
                                {
                                    ISong song;
                                    int index = IndexOfSong(x, songs);
                                    if (index == -1)
                                    {
                                        song = new Song(x);
                                        songs.Add(song);
                                    }
                                    else
                                    {
                                        song = songs[index];
                                    }
                                    playlist.AddSong(song);
                                });
                        }
                    }
                }
            }
        }

        private void PreviewPossibleCommands()
        {
            if(currentUser==null)
            {
                comunicator.WriteLine(VariableConstants.logInCommand);
                comunicator.WriteLine(VariableConstants.saveCommand);
            }
            else if(currentUser.Type==UserTypeEnum.artist)
            {
                comunicator.WriteLine(VariableConstants.printInfoCommand);
                comunicator.WriteLine(VariableConstants.printAlbumsCommand);
                comunicator.WriteLine(VariableConstants.printAlbumContentCommand);
                comunicator.WriteLine(VariableConstants.addAlbumCommand);
                comunicator.WriteLine(VariableConstants.removeAlbumCommand);
                comunicator.WriteLine(VariableConstants.addSongToAlbumCommand);
                comunicator.WriteLine(VariableConstants.removeSongFromAlbumCommand);
                comunicator.WriteLine(VariableConstants.logOutCommand);
                comunicator.WriteLine(VariableConstants.saveCommand);
            }
            else if(currentUser.Type==UserTypeEnum.listener)
            {
                comunicator.WriteLine(VariableConstants.printInfoCommand);
                comunicator.WriteLine(VariableConstants.printPlaylistsCommand);
                comunicator.WriteLine(VariableConstants.printFavuriteSongsCommand);
                comunicator.WriteLine(VariableConstants.printSongsFromPlaylistCommand);
                comunicator.WriteLine(VariableConstants.addPlaylistCommand);
                comunicator.WriteLine(VariableConstants.removePlaylistCommand);
                comunicator.WriteLine(VariableConstants.addSongToFavouritesCommand);
                comunicator.WriteLine(VariableConstants.addSongToPlaylistCommand);
                comunicator.WriteLine(VariableConstants.removeSongFromFavouritesCommand);
                comunicator.WriteLine(VariableConstants.removeSongFromPlaylistCommand);
                comunicator.WriteLine(VariableConstants.logOutCommand);
                comunicator.WriteLine(VariableConstants.saveCommand);
            }
        }
        private void GetSongs()
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    Match m = VariableConstants.songReg.Match(line);

                    if (m.Success)
                    {
                        String songName = m.Groups[1].ToString();

                        int songInd = IndexOfSong(songName, songs);

                        if (songInd == -1)
                        {
                            throw new Exception("This song is not been mentioned before");
                        }
                        else
                        {
                            ISong song = songs[songInd];

                            song.Time = int.Parse(m.Groups[2].ToString()) * 60 + int.Parse(m.Groups[3].ToString());
                        }
                    }
                }
            }
        }

        private void SaveInFile()
        {
            //FileOutput oFile=new FileOutput(filePath);
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(FileStringUsers());
                sb.Append(FileStringListeners());
                sb.Append(FileStringArtists());
                sb.Append(FileStringAlbums());
                sb.Append(FileStringSongs());
                sb.Append(FileStringPlaylists());

                sw.WriteLine(sb);
            }
        }

        private string FileStringUsers()
        {
            return string.Join("\n", users.Select(x => x.GetUserFileString)) + "\n\n";
        }

        private string FileStringArtists()
        {
            string res = "";

            foreach (IUser user in users)
            {
                if (user.Type == UserTypeEnum.artist)
                {
                    res += user.GetFileString + "\n";
                }
            }
            return res + "\n";
        }

        private string FileStringListeners()
        {
            string res = "";

            foreach (IUser user in users)
            {
                if (user.Type == UserTypeEnum.listener)
                {
                    res += user.GetFileString + "\n";
                }
            }
            return res + "\n";
        }

        private string FileStringAlbums()
        {
            return string.Join("\n", albums.Select(x => x.GetFileString)) + "\n\n";
        }

        private string FileStringPlaylists()
        {
            return string.Join("\n", playlists.Select(x => x.GetFileString)) + "\n\n";
        }

        private string FileStringSongs()
        {
            return string.Join("\n", songs.Select(x => x.GetFileString)) + "\n\n";
        }

        private List<string> GetStringsFromList(string input)
        {
            List<string> strings = new List<string>();

            foreach (Match match in Regex.Matches(input, VariableConstants.arrNamesReg.ToString(), RegexOptions.None))
            {
                strings.Add(match.Groups[1].ToString());
            }
            return strings;
        }

        private int IndexOfUser(string username, List<IUser> usersList)
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

        private int IndexOfAlbum(string albumName, List<IAlbum> albumsList)
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

        private int IndexOfPlaylist(string playlistName, List<IPlayList> playlistsList)
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

        private int IndexOfSong(string songName, List<ISong> songsList)
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
