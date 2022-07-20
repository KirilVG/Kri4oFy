using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Kri4oFy.Classes;
using Kri4oFy.Constants;
using System.Globalization;
using Kri4oFy.Interfaces;

namespace ArtistUnitTests
{
    [TestClass]
    public class ArtistUnitTest
    {
        private UserTypeEnum artist;

        Artist SetUpMetallica()
        {
            User user1 = new User("Metallica", "TheBest", artist);
            Album album = new Album("Black Album", "default");
            Song song1 = new Song("Ener Sandman", 5 * 60 + 31, album);
            album.Songs.Add(song1);
            Song song2 = new Song("Sad but True", 5 * 60 + 24, album);
            album.Songs.Add(song2);
            Song song3 = new Song("Nothing Else Matters", 6 * 60 + 28, album);
            album.Songs.Add(song3);
            Artist artist1 = new Artist(user1, "Metallica");
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

            Assert.AreEqual("Black Album", metallica.PrintAlbums());
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
            Album demoalbum = new Album("White Album", "default");

            metallica.Albums.Add(demoalbum);
            metallica.RemoveAlbum("White Album");

            Assert.AreEqual("Black Album", metallica.PrintAlbums());
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

            Album demoalbum = new Album("White Album", "default");
            metallica.Albums.Add(demoalbum);

            Assert.AreEqual(demoalbum, metallica.RemoveAlbum("White Album"));
        }

        [TestMethod]
        public void RemoveSongFromAlbum_Removes_The_Song_Correctly()
        {
            Artist metallica = SetUpMetallica();
            ISong demosong = new Song("Demo Song", 184);
            Album demoalbum = new Album("White Album", "default");

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
            Album demoalbum = new Album("White Album", "default");

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
            Album demoalbum = new Album("White Album", "default");

            metallica.AddAlbum(demoalbum);

            Assert.AreEqual("Black Album\nWhite Album", metallica.PrintAlbums());
        }

        [TestMethod]
        public void AddAlbum_Adds_the_Album_Successfuly_Returns_True()
        {
            Artist metallica = SetUpMetallica();
            Album demoalbum = new Album("White Album", "default");

            Assert.AreEqual(true, metallica.AddAlbum(demoalbum));
        }

        [TestMethod]
        public void AddAlbum_Throws_Error_If_Album_Already_Exists()
        {
            Artist metallica = SetUpMetallica();
            Album demoalbum = new Album("White Album", "default");

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
    }
}
