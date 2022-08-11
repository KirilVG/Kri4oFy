using Kri4oFy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Kri4oFy.Constants;
using Kri4oFy.Utils;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Kri4oFy.Classes
{
    public class DBDataHelper : IDataHelper
    {
        string connectionString;

        SqlConnection connection;

        public DBDataHelper(string connstr)
        {
            connectionString = ConfigurationManager.ConnectionStrings[connstr].ConnectionString;
        }

        public void SaveData(ISpData data, List<string> changes)
        {
            foreach(var change in changes)
            {
                List<string> tokens = new List<string>();

                foreach (Match match in Regex.Matches(change, VariableConstants.changesReg.ToString(), RegexOptions.None))
                {
                    tokens.Add(match.Groups[1].ToString());
                }

                switch(tokens[0])
                {
                    case VariableConstants.cngAddAlbum:
                        ChangeAddAlbum(tokens);
                        break;
                    case VariableConstants.cngAddSongToAlbum:
                        ChangeAddSongToALbum(tokens);
                        break;
                    case VariableConstants.cngAddSongToFav:
                        ChangeAddSongToFav(tokens);
                        break;
                    case VariableConstants.cngAddSongToPlayL:
                        ChangeAddSongToPlayL(tokens);
                        break;
                    case VariableConstants.cngCreatePlaylist:
                        ChangeCreatePlaylist(tokens);
                        break;
                    case VariableConstants.cngRemoveAlb:
                        ChangeRemoveAlbum(tokens);
                        break;
                    case VariableConstants.cngRemovePlaylist:
                        ChangeRemovePlaylist(tokens);
                        break;
                    case VariableConstants.cngRemoveSongFromAlb:
                        ChangeRemoveSongFromAlb(tokens);
                        break;
                    case VariableConstants.cngRemoveSongFromFav:
                        ChangeRemoveSongFromFav(tokens);
                        break;
                    case VariableConstants.cngRemoveSongFromPlayL:
                        ChangeRemoveSongFromPlayL(tokens);
                        break;
                    default:
                        throw new ArgumentException("This change case has not been implemented yet");
                }
            }
            changes.Clear();
        }

        void ExecuteQuerry(string querry)
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(querry, connection))
            {
                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        private int GetUserId(string Username)
        {
            int userId = 0;

            string querry = $"select * from Users where Username = '{Username}';";

            using (connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand(querry, connection))

            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    userId = int.Parse(dataReader.GetValue(0).ToString());
                }
            }
            return userId;
        }

        private int GetSongId(string SongName)
        {
            int songId = 0;

            string querry = $"select * from Songs where Name = '{SongName}';";

            using (connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand(querry, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    songId = int.Parse(dataReader.GetValue(0).ToString());
                }
            }
            return songId;
        }

        private int GetPlaylistId(string Playlistname)
        {
            int PlaylistId = 0;

            string querry = $"select * from Playlists where PlaylistName = '{Playlistname}';";

            using (connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand(querry, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    PlaylistId = int.Parse(dataReader.GetValue(0).ToString());
                }
            }
            return PlaylistId;
        }

        private int GetGenreId(string GenreName)
        {
            int GenreId = 0;

            string querry = $"select * from Genres where Name = '{GenreName}';";

            using (connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand(querry, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    GenreId = int.Parse(dataReader.GetValue(0).ToString());
                }
            }
            return GenreId;
        }

        private int GetAlbumId(string AlbumName)
        {
            int AlbumId = 0;

            string querry = $"select * from Albums where AlbumName = '{AlbumName}';";

            using (connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand(querry, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    AlbumId = int.Parse(dataReader.GetValue(0).ToString());
                }
            }
            return AlbumId;
        }

        private void DeleteSong(string Title)
        {
            int songId = GetSongId(Title);

            string query = $"delete from LikedSongsByListener where SongId={songId}";

            ExecuteQuerry(query);

            query = $"delete from SongsInPlaylist where SongId={songId}";

            ExecuteQuerry(query);

            query = $"delete from Songs where Id={songId}";

            ExecuteQuerry(query);
        }

        private void ChangeRemoveAlbum(List<string> tokens)
        {
            int albumId = GetAlbumId(tokens[1]);

            List<string> songsTitles=new List<string>();

            string querry = $"select Songs.Name from Songs where Songs.AlbumId={albumId}";

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(querry, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    songsTitles.Add(dataReader.GetValue(0).ToString());
                }

                dataReader.Close();
            }

            foreach(string songTitle in songsTitles)
            {
                DeleteSong(songTitle);
            }

            querry = $"delete from Albums where Id={albumId}";

            ExecuteQuerry(querry);
        }
        private void ChangeRemoveSongFromAlb(List<string> tokens)
        {
            DeleteSong(tokens[1]);
        }

        private void ChangeAddSongToALbum(List<string> tokens)
        {
            int albumId = GetAlbumId(tokens[1]);

            int time=int.Parse(tokens[3]);

            string querry = $"insert into Songs values('{tokens[2]}','00:{time / 60}:{time % 60:D2}',{albumId})";

            ExecuteQuerry(querry);
        }

        private void ChangeAddAlbum(List<string> tokens)
        {
            int userId = GetUserId(tokens[1]);

            int genreId = GetGenreId(tokens[4]);

            string querry = $"insert into Albums values('{tokens[2]}',{userId},{tokens[3]},{genreId})";

            ExecuteQuerry(querry);
        }

        private void ChangeRemovePlaylist(List<string> tokens)
        {
            int playlistind = GetPlaylistId(tokens[1]);

            string querry = $"delete from PlaylistByListener where PlaylistId={playlistind}";

            ExecuteQuerry(querry);

            querry = $"delete from SongsInPLaylist where PlaylistId={playlistind}";

            ExecuteQuerry(querry);

            querry = $"delete from PLaylists where Id={playlistind}";

            ExecuteQuerry(querry);
        }

        private void ChangeRemoveSongFromPlayL(List<string> tokens)
        {
            int PlayistId = GetPlaylistId(tokens[1]);

            int songId = GetSongId(tokens[2]);

            string querry = $"delete from SongsInPlaylist where PlaylistId={PlayistId} and SongId={songId};";

            ExecuteQuerry(querry);
        }

        private void ChangeRemoveSongFromFav(List<string> tokens)
        {
            int userId = GetUserId(tokens[1]);

            int songId = GetSongId(tokens[2]);

            string querry = $"delete from LikedSongsByListener where ListenerId={userId} and SongId={songId};";

            ExecuteQuerry(querry);
        }

        private void ChangeCreatePlaylist(List<string> tokens)
        {
            int userind = GetUserId(tokens[1]);

            string querry = $"insert into Playlists values('{tokens[2]}',{userind});";

            ExecuteQuerry(querry);

            int playlistind = GetPlaylistId(tokens[2]);

            querry = $"insert into PlaylistByListener values({userind},{playlistind});";

            ExecuteQuerry(querry);
        }

        private void ChangeAddSongToPlayL(List<string> tokens)
        {
            int PlayistId = GetPlaylistId(tokens[1]);

            int songId = GetSongId(tokens[2]);

            string querry = $"insert into SongsInPlaylist values({PlayistId},{songId});";

            ExecuteQuerry(querry);
        }

        private void ChangeAddSongToFav(List<string> tokens)
        {
            int userId = GetUserId(tokens[1]);

            int songId=GetSongId(tokens[2]);

            string querry = $"insert into LikedSongsByListener values({userId},{songId});";

            ExecuteQuerry(querry);
        }

        public ISpData TakeData()
        {
            ISpData data = new SpData();

            TakeUsers(data);
            TakeArtists(data);
            TakeListeners(data);
            TakeAlbums(data);
            TakePlaylists(data);
            TakeSongs(data);

            return data;
        }

        private void TakeSongs(ISpData data)
        {
            string querry = "select Songs.Name, Songs.Duration, Albums.AlbumName from Songs join Albums on Songs.AlbumId = Albums.Id";

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(querry, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    int albumInd = Methods.IndexOfAlbum(dataReader.GetValue(2).ToString(), data.Albums);

                    if (albumInd == -1)
                    {
                        throw new InvalidOperationException("An album with this Id does not exist");
                    }

                    if (Methods.IndexOfSong(dataReader.GetValue(0).ToString(), data.Songs) != -1)
                    {
                        throw new InvalidOperationException("A song with this name already exists");
                    }

                    ISong song = new Song(dataReader.GetValue(0).ToString(), (int)TimeSpan.Parse(dataReader.GetValue(1).ToString()).TotalSeconds, data.Albums[albumInd]);

                    data.Songs.Add(song);

                    data.Albums[albumInd].AddSong(song);
                }

                dataReader.Close();
            }

            querry = "select Songs.Name, Users.Username from Songs join LikedSongsByListener on Songs.Id = LikedSongsByListener.SongId join Users on LikedSongsByListener.ListenerId = Users.Id";

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(querry, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    int UserInd = Methods.IndexOfUser(dataReader.GetValue(1).ToString(), data.Users);

                    if (UserInd == -1)
                    {
                        throw new InvalidOperationException("This user does Not exist");
                    }

                    int songInd = Methods.IndexOfSong(dataReader.GetValue(0).ToString(), data.Songs);

                    if (songInd == -1)
                    {
                        throw new InvalidOperationException("This song does not exist");
                    }

                    ((IListener)data.Users[UserInd]).AddSongToFavourites(data.Songs[songInd]);
                }

                dataReader.Close();
            }

            querry = "select Songs.Name, Playlists.PlaylistName from Songs join SongsInPlaylist on Songs.Id=SongsInPlaylist.SongId join Playlists on SongsInPlaylist.PlaylistId = Playlists.Id";

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(querry, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    int songInd = Methods.IndexOfSong(dataReader.GetValue(0).ToString(), data.Songs);

                    if (songInd == -1)
                    {
                        throw new InvalidOperationException("This Song does Not exist");
                    }

                    int playlistInd = Methods.IndexOfPlaylist(dataReader.GetValue(1).ToString(), data.Playlists);

                    if (playlistInd == -1)
                    {
                        throw new InvalidOperationException("This song does not exist");
                    }

                    data.Playlists[playlistInd].AddSong(data.Songs[songInd]);
                }

                dataReader.Close();
            }
        }

        private void TakePlaylists(ISpData data)
        {
            string querry = "select Playlists.PlaylistName, Users.Username from Playlists join PlaylistByListener on Playlists.Id=PlaylistByListener.PlaylistId join Users on PlaylistByListener.ListenerId=Users.Id";

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(querry, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    int listenerInd = Methods.IndexOfUser(dataReader.GetValue(1).ToString(), data.Users);

                    if (listenerInd == -1)
                    {
                        throw new InvalidOperationException("The listener responsible for this playlist does not exist");
                    }

                    if (Methods.IndexOfPlaylist(dataReader.GetValue(0).ToString(), data.Playlists) != -1)
                    {
                        throw new InvalidOperationException("A playlist with this name already exists");
                    }

                    ISongCollection newPlaylist = new PlayList(dataReader.GetValue(0).ToString());

                    data.Playlists.Add(newPlaylist);

                    ((IListener)data.Users[listenerInd]).AddPlayList(newPlaylist);
                }

                dataReader.Close();
            }
        }

        private void TakeAlbums(ISpData data)
        {
            string querry = "select Albums.AlbumName, Albums.DateOfCreation, Genres.Name, Users.Username from Albums join Users on Albums.ArtistID = Users.Id join Genres on Albums.GenreID = Genres.Id";

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(querry, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    int artistInd = Methods.IndexOfUser(dataReader.GetValue(3).ToString(), data.Users);

                    if (artistInd == -1)
                    {
                        throw new InvalidOperationException("The artist responsible for this album does not exist");
                    }

                    if (Methods.IndexOfPlaylist(dataReader.GetValue(0).ToString(), data.Playlists) != -1)
                    {
                        throw new InvalidOperationException("An album with this name exists");
                    }

                    IAlbum newAlbum = new Album(dataReader.GetValue(0).ToString(), (IArtist)data.Users[artistInd]);

                    newAlbum.DateOfCreation = DateTime.ParseExact($"01/01/{dataReader.GetValue(1)}", "dd/mm/yyyy", CultureInfo.InvariantCulture);

                    newAlbum.Genre = (GenreEnum)Enum.Parse(typeof(GenreEnum), dataReader.GetValue(2).ToString());

                    data.Albums.Add(newAlbum);

                    ((IArtist)data.Users[artistInd]).AddAlbum(newAlbum);
                }

                dataReader.Close();
            }
        }

        private void TakeListeners(ISpData data)
        {
            string querry = "select * from Users join Listeners on Users.Id = Listeners.UserId";

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(querry, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    int listenerInd = Methods.IndexOfUser(dataReader.GetValue(1).ToString(), data.Users);

                    if (listenerInd == -1)
                    {
                        throw new InvalidOperationException("This artist does not belong to the Users' list.");
                    }

                    IListener listener = new Listener((User)data.Users[listenerInd]);

                    data.Users[listenerInd] = listener;

                    listener.FullName = dataReader.GetValue(6).ToString();

                    listener.DateOfBirth = DateTime.Parse(dataReader.GetValue(5).ToString());
                }

                dataReader.Close();
            }
            //add genres
            querry = "select Users.Username, Genres.Name from Users join ListenerGenres on Users.Id = ListenerGenres.ListenerId join Genres on ListenerGenres.GenreId=Genres.Id";

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(querry, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    int listenerInd = Methods.IndexOfUser(dataReader.GetValue(0).ToString(), data.Users);

                    if (listenerInd == -1)
                    {
                        throw new InvalidOperationException("A listener is missing from definition.");
                    }

                    IListener listener = (IListener)data.Users[listenerInd];

                    listener.Genres.Add((GenreEnum)Enum.Parse(typeof(GenreEnum), dataReader.GetValue(1).ToString()));
                }

                dataReader.Close();
            }
        }

        private void TakeArtists(ISpData data)
        {
            string querry = "select * from Users join Artists on Users.Id = Artists.UserId";

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(querry, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    int artistInd = Methods.IndexOfUser(dataReader.GetValue(1).ToString(), data.Users);

                    if (artistInd == -1)
                    {
                        throw new InvalidOperationException("This artist does not belong to the Users' list.");
                    }

                    IArtist artist = new Artist((User)data.Users[artistInd]);

                    data.Users[artistInd] = artist;

                    artist.FullName = dataReader.GetValue(6).ToString();

                    artist.DateOfBirth = DateTime.Parse(dataReader.GetValue(5).ToString());
                }

                dataReader.Close();
            }

            querry = "select Users.Username, Genres.Name from Users join ArtistGenres on Users.Id = ArtistGenres.ArtistId join Genres on ArtistGenres.GenreId=Genres.Id";

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(querry, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    int artistInd = Methods.IndexOfUser(dataReader.GetValue(0).ToString(), data.Users);

                    if (artistInd == -1)
                    {
                        throw new InvalidOperationException("An artist is missing from definition.");
                    }

                    IArtist artist = (IArtist)data.Users[artistInd];

                    artist.Genres.Add((GenreEnum)Enum.Parse(typeof(GenreEnum), dataReader.GetValue(1).ToString()));
                }

                dataReader.Close();
            }
        }

        private void TakeUsers(ISpData data)
        {
            string querry = "select * from Users;";

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(querry, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    IUser newUser = new User(dataReader.GetValue(1).ToString(), dataReader.GetValue(2).ToString(), (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), dataReader.GetValue(3).ToString()));

                    data.Users.Add(newUser);
                }

                dataReader.Close();
            }
        }
    }
}
