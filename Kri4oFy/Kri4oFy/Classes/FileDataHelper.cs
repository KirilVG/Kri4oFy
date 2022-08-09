using Kri4oFy.Constants;
using Kri4oFy.Interfaces;
using Kri4oFy.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kri4oFy.Classes
{
    public class FileDataHelper : IDataHelper
    {
        string filePath;

        public void SaveData(ISpData data)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(FileStringUsers(data));
                sb.Append(FileStringListeners(data));
                sb.Append(FileStringArtists(data));
                sb.Append(FileStringAlbums(data));
                sb.Append(FileStringSongs(data));
                sb.Append(FileStringPlaylists(data));

                sw.WriteLine(sb);
            }
        }

        private string FileStringUsers(ISpData data)
        {
            return string.Join("\n", data.Users.Select(x => x.GetUserFileString)) + "\n\n";
        }

        private string FileStringArtists(ISpData data)
        {
            string res = "";

            foreach (IUser user in data.Users)
            {
                if (user.Type == UserTypeEnum.artist)
                {
                    res += user.GetFileString + "\n";
                }
            }
            return res + "\n";
        }

        private string FileStringListeners(ISpData data)
        {
            string res = "";

            foreach (IUser user in data.Users)
            {
                if (user.Type == UserTypeEnum.listener)
                {
                    res += user.GetFileString + "\n";
                }
            }
            return res + "\n";
        }

        private string FileStringAlbums(ISpData data)
        {
            return string.Join("\n", data.Albums.Select(x => x.GetFileString)) + "\n\n";
        }

        private string FileStringPlaylists(ISpData data)
        {
            return string.Join("\n", data.Playlists.Select(x => x.GetFileString)) + "\n\n";
        }

        private string FileStringSongs(ISpData data)
        {
            return string.Join("\n", data.Songs.Select(x => x.GetFileString)) + "\n\n";
        }

        public ISpData TakeData()
        {
            ISpData CurrentSpData = new SpData();

            if (!(File.Exists(filePath)))
            {
                using (FileStream fs = File.Create(filePath))
                {

                }
            }
            else
            {
                GetUsers(CurrentSpData);
                GetListeners(CurrentSpData);
                GetArtists(CurrentSpData);
                GetAlbums(CurrentSpData);
                GetPlayLists(CurrentSpData);
                GetSongs(CurrentSpData);
            }

            return CurrentSpData;
        }

        public FileDataHelper(string filepath)
        {
            this.filePath = filepath;
        }

        private void GetUsers(ISpData data)
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

                        if (Methods.IndexOfUser(username, data.Users) == -1)
                        {
                            User newUser = new User(username, password, type);

                            data.Users.Add(newUser);
                        }
                        else
                        {
                            throw new Exception("There already is a user with the same username");
                        }
                    }
                }
            }
        }

        private void GetListeners(ISpData data)
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
                        int userInd = Methods.IndexOfUser(username, data.Users);
                        if (userInd == -1 || data.Users[userInd].Type != UserTypeEnum.listener)
                        {
                            throw new Exception("a user with that username does not exist or isn't a listener");
                        }
                        else
                        {
                            IListener listener = new Listener((User)data.Users[userInd]);
                            data.Users[userInd] = listener;

                            listener.FullName = m.Groups[2].ToString();

                            listener.DateOfBirth = DateTime.ParseExact(m.Groups[3].ToString(), "dd/mm/yyyy", CultureInfo.InvariantCulture);

                            Methods.GetStringsFromList(m.Groups[4].ToString())
                                .ForEach(x => listener.Genres.Add((GenreEnum)Enum.Parse(typeof(GenreEnum), x)));

                            Methods.GetStringsFromList(m.Groups[5].ToString())
                                .ForEach(x =>
                                {
                                    ISong song;
                                    int index = Methods.IndexOfSong(x, data.Songs);
                                    if (index == -1)
                                    {
                                        song = new Song(x);
                                        data.Songs.Add(song);
                                    }
                                    else
                                    {
                                        song = data.Songs[index];
                                    }
                                    listener.AddSongToFavourites(song);
                                });

                            Methods.GetStringsFromList(m.Groups[6].ToString())
                                .ForEach(x =>
                                {
                                    ISongCollection playList;
                                    int index = Methods.IndexOfPlaylist(x, data.Playlists);
                                    if (index == -1)
                                    {
                                        playList = new PlayList(x);
                                        data.Playlists.Add(playList);
                                    }
                                    else
                                    {
                                        playList = data.Playlists[index];
                                    }
                                    listener.AddPlayList(playList);
                                });
                        }
                    }
                }
            }
        }

        private void GetArtists(ISpData data)
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

                        int userInd = Methods.IndexOfUser(username, data.Users);

                        if (userInd == -1 || data.Users[userInd].Type != UserTypeEnum.artist)
                        {
                            throw new Exception("a user with that username does not exist or isn't an artist");
                        }
                        else
                        {
                            IArtist artist = new Artist((User)data.Users[userInd]);
                            data.Users[userInd] = artist;

                            artist.FullName = m.Groups[2].ToString();

                            artist.DateOfBirth = DateTime.ParseExact(m.Groups[3].ToString(), "dd/mm/yyyy", CultureInfo.InvariantCulture);

                            Methods.GetStringsFromList(m.Groups[4].ToString())
                                .ForEach(x => artist.Genres.Add((GenreEnum)Enum.Parse(typeof(GenreEnum), x)));

                            Methods.GetStringsFromList(m.Groups[5].ToString())
                                .ForEach(x =>
                                {
                                    IAlbum album;
                                    int index = Methods.IndexOfAlbum(x, data.Albums);
                                    if (index == -1)
                                    {
                                        album = new Album(x);
                                        data.Albums.Add(album);
                                    }
                                    else
                                    {
                                        album = data.Albums[index];
                                    }
                                    artist.AddAlbum(album);
                                });
                        }
                    }
                }
            }
        }

        private void GetAlbums(ISpData data)
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

                        int albumInd = Methods.IndexOfAlbum(albumName, data.Albums);

                        if (albumInd == -1)
                        {
                            throw new Exception("This album is not connected with an artist.");
                        }
                        else
                        {
                            IAlbum album = data.Albums[albumInd];

                            album.DateOfCreation = DateTime.ParseExact($"01/01/{m.Groups[2].ToString()}", "dd/mm/yyyy", CultureInfo.InvariantCulture);

                            album.Genre = (GenreEnum)Enum.Parse(typeof(GenreEnum), m.Groups[3].ToString());

                            Methods.GetStringsFromList(m.Groups[4].ToString())
                                .ForEach(x =>
                                {
                                    ISong song;
                                    int index = Methods.IndexOfSong(x, data.Songs);
                                    if (index == -1)
                                    {
                                        song = new Song(x);
                                        data.Songs.Add(song);
                                    }
                                    else
                                    {
                                        song = data.Songs[index];
                                    }
                                    album.AddSong(song);
                                    song.Album = album;
                                });
                        }
                    }
                }
            }
        }

        private void GetSongs(ISpData data)
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

                        int songInd = Methods.IndexOfSong(songName, data.Songs);

                        if (songInd == -1)
                        {
                            throw new Exception("This song has not been mentioned before");
                        }
                        else
                        {
                            ISong song = data.Songs[songInd];

                            song.Time = int.Parse(m.Groups[2].ToString()) * 60 + int.Parse(m.Groups[3].ToString());
                        }
                    }
                }
            }
        }

        private void GetPlayLists(ISpData data)
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

                        int playlistInd = Methods.IndexOfPlaylist(playlistName, data.Playlists);

                        if (playlistInd == -1)
                        {
                            throw new Exception("This playlist is not connected with a listener.");
                        }
                        else
                        {
                            ISongCollection playlist = data.Playlists[playlistInd];

                            Methods.GetStringsFromList(m.Groups[2].ToString())
                                .ForEach(x =>
                                {
                                    ISong song;
                                    int index = Methods.IndexOfSong(x, data.Songs);
                                    if (index == -1)
                                    {
                                        song = new Song(x);
                                        data.Songs.Add(song);
                                    }
                                    else
                                    {
                                        song = data.Songs[index];
                                    }
                                    playlist.AddSong(song);
                                });
                        }
                    }
                }
            }
        }
    }
}
