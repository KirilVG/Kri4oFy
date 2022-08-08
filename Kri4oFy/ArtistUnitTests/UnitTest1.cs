using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Kri4oFy.Classes;
using Kri4oFy.Constants;
using System.Globalization;
using Kri4oFy.Interfaces;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace ArtistUnitTests
{
    [TestClass]
    public class ArtistUnitTest
    {
        //private UserTypeEnum artist;

        Artist SetUpMetallica()
        {
            User user1 = new User("Metallica", "TheBest", UserTypeEnum.artist);
            Artist artist1 = new Artist(user1, "Metallica");
            Album album = new Album("Black Album", artist1);
            Song song1 = new Song("Ener Sandman", 5 * 60 + 31, album);
            album.Songs.Add(song1);
            Song song2 = new Song("Sad but True", 5 * 60 + 24, album);
            album.Songs.Add(song2);
            Song song3 = new Song("Nothing Else Matters", 6 * 60 + 28, album);
            album.Songs.Add(song3);
            
            artist1.Genres.Add(GenreEnum.rock);
            artist1.Genres.Add(GenreEnum.metal);
            artist1.Albums.Add(album);
            artist1.DateOfBirth = DateTime.ParseExact("28/10/1981", "dd/mm/yyyy", CultureInfo.InvariantCulture);
            return artist1;
        }

        [TestMethod]
        public void TestMethod1()
        {
            Artist metallica = SetUpMetallica();

            Assert.AreEqual(metallica.GetFileString, "<artist><Metallica><Metallica>[28/10/1981](genres: ['rock', 'metal'])(albums: ['Black Album'])</artist>");
        }

        [TestMethod]
        public void SongConstructor_Assigns_Name()
        {
            Song song3 = new Song("Nothing Else Matters", 6 * 60 + 28);
            Assert.AreEqual("Nothing Else Matters", song3.SongName);
        }

        [TestMethod]
        public void SongConstructor_Assigns_Length()
        {
            Song song3 = new Song("Nothing Else Matters", 6 * 60 + 28);
            Assert.AreEqual("[6:28]", $"[{song3.Time / 60}:{song3.Time % 60:D2}]");
        }

        [TestMethod]
        public void Song_GetFileString_Returns_Correct_String()
        {
            Song song3 = new Song("Nothing Else Matters", 6 * 60 + 28);

            Assert.AreEqual("<song><Nothing Else Matters>[6:28]</song>", song3.GetFileString);
        }

        [TestMethod]
        public void PrintAlbums_Prints_Albums_Correctly()
        {
            Artist metallica = SetUpMetallica();

            Assert.AreEqual("<Album> Name:Black Album length:[17:23]", metallica.PrintAlbums());
        }

        [TestMethod]
        public void PrintAlbumContent_Prints_Albums_Correctly()
        {
            Artist metalica = SetUpMetallica();

            Assert.AreEqual("<song> Ener Sandman [5:31]\n<song> Sad but True [5:24]\n<song> Nothing Else Matters [6:28]", metalica.PrintAlbumContent("Black Album"));
        }

        [TestMethod]
        public void PrintAlbumContent_Throws_Error_When_Given_Incorrect_Albumname()
        {
            Artist metalica = SetUpMetallica();

            Assert.ThrowsException<ArgumentException>(() => metalica.PrintAlbumContent("Fake Album"));
        }

        [TestMethod]
        public void RemoveAlbum_Removes_ItemsCorrectly()
        {
            Artist metallica = SetUpMetallica();
            Album demoalbum = new Album("White Album", metallica);

            metallica.Albums.Add(demoalbum);
            metallica.RemoveAlbum("White Album");

            Assert.AreEqual("<Album> Name:Black Album length:[17:23]", metallica.PrintAlbums());
        }

        [TestMethod]
        public void RemoveAlbum_Throws_Error_When_Given_Incorrect_Albumname()
        {
            Artist metallica = SetUpMetallica();

            Assert.ThrowsException<ArgumentException>(() => metallica.RemoveAlbum("FakeAlbum"));
        }

        [TestMethod]
        public void RemoveAlbum_Rreturns_The_Correct_Album()
        {
            Artist metallica = SetUpMetallica();

            Album demoalbum = new Album("White Album", metallica);
            metallica.Albums.Add(demoalbum);

            Assert.AreEqual(demoalbum, metallica.RemoveAlbum("White Album"));
        }

        [TestMethod]
        public void RemoveSongFromAlbum_Removes_The_Song_Correctly()
        {
            Artist metallica = SetUpMetallica();
            ISong demosong = new Song("Demo Song", 184);
            Album demoalbum = new Album("White Album", metallica);

            metallica.Albums.Add(demoalbum);
            demoalbum.Songs.Add(demosong);
            metallica.RemoveSongFromAlbum("White Album", "Demo Song");

            Assert.AreEqual("", metallica.PrintAlbumContent("White Album"));

        }

        [TestMethod]
        public void RemoveSongFromAlbum_Returns_The_Correct_Song()
        {
            Artist metallica = SetUpMetallica();
            ISong demosong = new Song("Demo Song", 184);
            Album demoalbum = new Album("White Album", metallica);

            metallica.Albums.Add(demoalbum);
            demoalbum.Songs.Add(demosong);

            Assert.AreEqual(demosong, metallica.RemoveSongFromAlbum("White Album", "Demo Song"));
        }

        [TestMethod]
        public void RemoveSongFormAlbum_Throws_Exeption_When_Given_InCorrect_AlbumName()
        {
            Artist metallica = SetUpMetallica();

            Assert.ThrowsException<ArgumentException>(() => metallica.RemoveSongFromAlbum("Wrong Album", "Nothing Even Matters"));
        }

        [TestMethod]
        public void RemoveSongFormAlbum_Throws_Exeption_When_Given_InCorrect_SongName()
        {
            Artist metallica = SetUpMetallica();

            Assert.ThrowsException<ArgumentException>(() => metallica.RemoveSongFromAlbum("Black Album", "Wrong Song"));
        }

        [TestMethod]
        public void AddAlbum_Adds_the_Album_correctly()
        {
            Artist metallica = SetUpMetallica();
            Album demoalbum = new Album("White Album", metallica);

            metallica.AddAlbum(demoalbum);

            Assert.AreEqual("<Album> Name:Black Album length:[17:23]\n<Album> Name:White Album length:[0:00]", metallica.PrintAlbums());
        }

        [TestMethod]
        public void AddAlbum_Adds_the_Album_Successfuly_Returns_True()
        {
            Artist metallica = SetUpMetallica();
            Album demoalbum = new Album("White Album", metallica);

            Assert.AreEqual(true, metallica.AddAlbum(demoalbum));
        }

        [TestMethod]
        public void AddAlbum_Throws_Error_If_Album_Already_Exists()
        {
            Artist metallica = SetUpMetallica();
            Album demoalbum = new Album("White Album", metallica);

            metallica.AddAlbum(demoalbum);

            Assert.ThrowsException<ArgumentException>(() => metallica.AddAlbum(demoalbum));
        }

        [TestMethod]
        public void AddSongToAlbum_Adds_The_Song_Correctly()
        {
            Artist metallica = SetUpMetallica();
            ISong demosong = new Song("Demo Song", 184);
            metallica.AddSongToAlbum("Black Album", demosong);
            Assert.AreEqual("<song> Ener Sandman [5:31]\n<song> Sad but True [5:24]\n<song> Nothing Else Matters [6:28]\n<song> Demo Song [3:04]", metallica.PrintAlbumContent("Black Album"));
        }

        [TestMethod]
        public void AddSongToAlbum_Adds_The_Song_Successfuly_Returns_True()
        {
            Artist metallica = SetUpMetallica();
            ISong demosong = new Song("Demo Song", 184);
            Assert.AreEqual(true,metallica.AddSongToAlbum("Black Album", demosong));
        }

        [TestMethod]
        public void AddSongToAlbum_Throws_Exeption_When_AlbumName_Is_Incorrect()
        {
            Artist metallica = SetUpMetallica();
            ISong demosong = new Song("Demo Song", 184);
            Assert.ThrowsException<ArgumentException>(() => metallica.AddSongToAlbum("Fake Album", demosong));
        }

        [TestMethod]
        public void AddSongToAlbum_Throws_Exeption_When_the_Song_Already_Exists_There()
        {
            Artist metallica = SetUpMetallica();
            ISong demosong = new Song("Demo Song", 184);
            metallica.AddSongToAlbum("Black Album", demosong);
            Assert.ThrowsException<ArgumentException>(() => metallica.AddSongToAlbum("Black Album", demosong));
        }

        [TestMethod]
        public void Regex_Matches_User()
        {
            bool res = VariableConstants.userReg.IsMatch("<user><Metallica>(TheBest){artist}</user>");

            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Regex_Does_Not_Match_Other_Class()
        {
            bool res = VariableConstants.userReg.IsMatch("<listener><Go6koy><Georgi D>[17/12/1996](genres: ['rock', 'metal'])(likedSongs: ['Nothing Else Matters', 'Obseben'])(playlists: [])</listener>");

            Assert.IsFalse(res);
        }

        [TestMethod]
        public void GetUsers_Logic_Is_Correct_1()
        {
            Match m = VariableConstants.userReg.Match(" <user><Metallica>(TheBest){artist}</user> ");
            if (m.Success)
            {
                String username = m.Groups[1].ToString();
                String password = m.Groups[2].ToString();
                UserTypeEnum type = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), m.Groups[3].ToString());

                User newUser = new User(username, password, type);
                
                Assert.AreEqual("Metallica", newUser.Username);
            }
        }

        [TestMethod]
        public void GetUsers_Logic_Is_Correct_2()
        {
            Match m = VariableConstants.userReg.Match(" <user><Metallica>(TheBest){artist}</user> ");
            if (m.Success)
            {
                String username = m.Groups[1].ToString();
                String password = m.Groups[2].ToString();
                UserTypeEnum type = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), m.Groups[3].ToString());

                User newUser = new User(username, password, type);

                Assert.AreEqual("TheBest", newUser.Password);
            }
        }

        [TestMethod]
        public void GetUsers_Logic_Is_Correct_3()
        {
            Match m = VariableConstants.userReg.Match(" <user><Metallica>(TheBest){artist}</user> ");
            if (m.Success)
            {
                String username = m.Groups[1].ToString();
                String password = m.Groups[2].ToString();
                UserTypeEnum type = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), m.Groups[3].ToString());

                User newUser = new User(username, password, type);

                Assert.AreEqual(UserTypeEnum.artist, newUser.Type);
            }
        }

        [TestMethod]
        public void GetStrings_Logic_Is_correct()
        {
            List<string> strings = new List<string>();
            foreach (Match match in Regex.Matches("'Enter Sandman', 'Sad but True', 'Nothing Else Matters'", VariableConstants.arrNamesReg.ToString(), RegexOptions.None))
            {
                strings.Add(match.Groups[1].ToString());
            }
            Assert.AreEqual("Enter Sandman", strings[0]);
        }
    }
}
