using Kri4oFy.Interfaces;
using Kri4oFy.Constants;
using System;
using System.Linq;
using System.Globalization;
using Kri4oFy.Utils;
using System.Collections.Generic;
namespace Kri4oFy.Classes
{
    internal class SpotifyApp : ISpotifyApp
    {
        IInputOutput comunicator;

        ISpData data;

        IUser currentUser;

        IDataHelper dataHelper;

        List<string> changes;

        public SpotifyApp(IInputOutput Comunicator, IDataHelper dataHelper)
        {
            this.comunicator = Comunicator;

            this.data = new SpData();

            this.dataHelper = dataHelper;

            currentUser = null;

            changes = new List<string>();
        }

        public void Run()
        {
            TakeData();

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
                            SaveData();
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

                int songInd = Methods.IndexOfSong(songName, data.Songs);

                if (songInd == -1)
                {
                    comunicator.WriteLine(VariableConstants.songWithNameDoesNotExistMSG);
                }
                else
                {
                    ((IListener)currentUser).RemoveSongFromFavourites(data.Songs[songInd]);

                    changes.Add($"[{VariableConstants.cngRemoveSongFromFav}] [{currentUser.Username}] [{songName}]");

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

                changes.Add($"[{VariableConstants.cngRemoveSongFromPlayL}] [{playlistName}] [{songName}]");

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

                int songInd = Methods.IndexOfSong(songName, data.Songs);

                if (songInd == -1)
                {
                    comunicator.WriteLine(VariableConstants.songWithNameDoesNotExistMSG);
                }
                else
                {
                    ((IListener)currentUser).AddSongToPlayList(playlistName, data.Songs[songInd]);

                    changes.Add($"[{VariableConstants.cngAddSongToPlayL}] [{playlistName}] [{songName}]");

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

                int songInd = Methods.IndexOfSong(songName, data.Songs);

                if (songInd == -1)
                {
                    comunicator.WriteLine(VariableConstants.songWithNameDoesNotExistMSG);
                }
                else
                {
                    ((IListener)currentUser).AddSongToFavourites(data.Songs[songInd]);

                    changes.Add($"[{VariableConstants.cngAddSongToFav}] [{currentUser.Username}] [{songName}]");

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

                ISongCollection playlist = ((IListener)currentUser).RemovePlayList(playlistName);

                data.Playlists.Remove(playlist);

                changes.Add($"[{VariableConstants.cngRemovePlaylist}] [{playlistName}]");

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

                ISongCollection playlist = new PlayList(playlistName);

                ((IListener)currentUser).AddPlayList(playlist);

                data.Playlists.Add(playlist);

                changes.Add($"[{VariableConstants.cngCreatePlaylist}] [{currentUser.Username}] [{playlistName}]");

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

                string albumName;
                comunicator.WriteLine(VariableConstants.inputAlbumNameMSG);
                albumName = comunicator.ReadLine();

                string songName;
                comunicator.WriteLine(VariableConstants.inputSongNameMSG);
                songName = comunicator.ReadLine();

                ISong song = ((IArtist)currentUser).RemoveSongFromAlbum(albumName, songName);
                data.Songs.Remove(song);

                foreach (ISongCollection playlist in data.Playlists)
                {
                    if (playlist.Songs.Contains(song))
                    {
                        playlist.Songs.Remove(song);
                    }
                }

                foreach (IUser user in data.Users)
                {
                    if (user.Type == UserTypeEnum.listener && ((IListener)user).LikedSongs.Contains(song))
                    {
                        ((IListener)user).LikedSongs.Remove(song);
                    }
                }

                changes.Add($"[{VariableConstants.cngRemoveSongFromAlb}] [{songName}]");

                comunicator.WriteLine(VariableConstants.successfulyRemovedSongMSG);
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

                int songIdex = Methods.IndexOfSong(songName, data.Songs);
                if (songIdex == -1)
                {

                    ISong song = new Song(songName);

                    comunicator.WriteLine(VariableConstants.inputSonglengthMSG);

                    string lenstr = comunicator.ReadLine();
                    int[] lengthinp = lenstr.Split(':').Select(x => int.Parse(x)).ToArray();
                    int length = lengthinp[0] * 60 + lengthinp[1];
                    song.Time = length;

                    ((IArtist)currentUser).AddSongToAlbum(albumName, song);

                    data.Songs.Add(song);

                    changes.Add($"[{VariableConstants.cngAddSongToAlbum}] [{albumName}] [{songName}] [{length}]");

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

                if (Methods.IndexOfAlbum(albumName, data.Albums) == -1)
                {
                    comunicator.WriteLine(VariableConstants.albumWithNameDoesNotExistsMSG);
                }
                else
                {
                    IAlbum album = ((IArtist)currentUser).RemoveAlbum(albumName);

                    foreach (ISong song in album.Songs)
                    {
                        data.Songs.Remove(song);

                        foreach (ISongCollection playlist in data.Playlists)
                        {
                            if (playlist.Songs.Contains(song))
                            {
                                playlist.Songs.Remove(song);
                            }
                        }

                        foreach (IUser user in data.Users)
                        {
                            if (user.Type == UserTypeEnum.listener && ((IListener)user).LikedSongs.Contains(song))
                            {
                                ((IListener)user).LikedSongs.Remove(song);
                            }
                        }
                    }

                    data.Albums.Remove(album);

                    changes.Add($"[{VariableConstants.cngRemoveAlb}] [{albumName}]");

                    comunicator.WriteLine(VariableConstants.successfulyRemovedAlbumMSG);
                }
            }
        }

        private void AddAlbumFunc()
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

                if (Methods.IndexOfAlbum(albumName, data.Albums) == -1)
                {
                    IAlbum album = new Album(albumName, (IArtist)currentUser);

                    comunicator.WriteLine(VariableConstants.inputYearMSG);

                    album.DateOfCreation = DateTime.ParseExact($"01/01/{comunicator.ReadLine()}", "dd/mm/yyyy", CultureInfo.InvariantCulture);

                    comunicator.WriteLine(VariableConstants.inputGenreMSG);

                    album.Genre = (GenreEnum)Enum.Parse(typeof(GenreEnum), comunicator.ReadLine());

                    ((IArtist)currentUser).AddAlbum(album);

                    data.Albums.Add(album);

                    changes.Add($"[{VariableConstants.cngAddAlbum}] [{currentUser.Username}] [{albumName}] [{album.DateOfCreation.Year}] [{album.Genre}]");

                    comunicator.WriteLine(VariableConstants.successfulyAddedAlbumMSG);
                }
                else
                {
                    comunicator.WriteLine(VariableConstants.albumWithNameExistsMSG);
                }
            }
        }

        private void PrintAlbumContentFunc()
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

        private void PrintAlbums()
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

        private void WrongCommandFunc()
        {
            comunicator.WriteLine(VariableConstants.wrongCommandMSG);
        }

        private void LogInFunc()
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

                foreach (IUser user in data.Users)
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

        private void LogOutFunc()
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

        private void PrintUserInformationFunc()
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

        private void PreviewPossibleCommands()
        {
            if (currentUser == null)
            {
                comunicator.WriteLine(VariableConstants.logInCommand);
                comunicator.WriteLine(VariableConstants.saveCommand);
            }
            else if (currentUser.Type == UserTypeEnum.artist)
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
            else if (currentUser.Type == UserTypeEnum.listener)
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

        private void TakeData()
        {
            data = dataHelper.TakeData();
        }

        private void SaveData()
        {
            dataHelper.SaveData(data, changes);
        }
    }
}
