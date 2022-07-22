using Kri4oFy.Interfaces;
using Kri4oFy.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;

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

        public SpotifyApp(IInputOutput comunicator, string filePath)
        {
            this.comunicator = comunicator;

            this.filePath = filePath;

            this.users = new List<IUser>();

            this.albums = new List<IAlbum>();

            this.playlists = new List<IPlayList>();

            this.songs = new List<ISong>();
        }
        public void Run()
        {
            throw new NotImplementedException();
        }

        private void TakeFromFile()
        {
            GetUsers();
            GetListeners();
            GetArtists();
            GetAlbums();
            GetPlayLists();
            GetSongs();
        }

        private void GetUsers()
        {
            FileInput iFile = new FileInput(filePath);
            string line;
            while ((line = iFile.ReadLine()) != null)
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

        private void GetListeners()
        {
            FileInput iFile = new FileInput(filePath);
            string line;
            while ((line = iFile.ReadLine()) != null)
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

        private void GetArtists()
        {
            FileInput iFile = new FileInput(filePath);
            string line;
            while ((line = iFile.ReadLine()) != null)
            {
                Match m = VariableConstants.artistReg.Match(line); //add a new regex
                if (m.Success)
                {
                    String username = m.Groups[1].ToString();
                    int userInd = IndexOfUser(username, users);
                    if (userInd == -1 || users[userInd].Type != UserTypeEnum.listener)
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

        private void GetAlbums()
        {
            FileInput iFile = new FileInput(filePath);
            string line;
            while ((line = iFile.ReadLine()) != null)
            {
                Match m = VariableConstants.albumReg.Match(line); //add a new regex
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
                        album.DateOfCreation = DateTime.ParseExact($"00/00/{m.Groups[2].ToString()}", "dd/mm/yyyy", CultureInfo.InvariantCulture);
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

        private void GetPlayLists()
        {

            FileInput iFile = new FileInput(filePath);
            string line;
            while ((line = iFile.ReadLine()) != null)
            {
                Match m = VariableConstants.playlistReg.Match(line); //add a new regex
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

        private void GetSongs()
        {
            FileInput iFile = new FileInput(filePath);
            string line;
            while ((line = iFile.ReadLine()) != null)
            {
                Match m = VariableConstants.songReg.Match(line); //add a new regex
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
        private void SaveInFile()
        {
            FileOutput oFile=new FileOutput(filePath);
            SaveUsers(oFile);
            SaveListeners(oFile);
            SaveArtists(oFile);
            SaveAlbums(oFile);
            SavePlaylists(oFile);
            SaveSongs(oFile);
        }

        private void SaveUsers(IOutput ofile)
        {
            foreach(IUser user in users)
            {
                ofile.WriteLine(((User)user).GetFileString);
            }
        }
        private void SaveArtists(IOutput ofile)
        {
            foreach (IUser user in users)
            {
                if(user.Type==UserTypeEnum.artist)
                {
                    ofile.WriteLine(((Artist)user).GetFileString);
                }
                
            }
        }
        private void SaveListeners(IOutput ofile)
        {
            foreach (IUser user in users)
            {
                if (user.Type == UserTypeEnum.listener)
                {
                    ofile.WriteLine(((Listener)user).GetFileString);
                }

            }
        }

        private void SaveAlbums(IOutput ofile)
        {
            foreach(IAlbum album in albums)
            {
                ofile.WriteLine(album.GetFileString);
            }
        }

        private void SavePlaylists(IOutput ofile)
        {
            foreach (IPlayList playlist in playlists)
            {
                ofile.WriteLine(playlist.GetFileString);
            }
        }

        private void SaveSongs(IOutput ofile)
        {
            foreach (ISong song in songs)
            {
                ofile.WriteLine(song.GetFileString);
            }
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
