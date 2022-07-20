using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Kri4oFy.Classes;
using Kri4oFy.Constants;
using System.Globalization;

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

       
    }
}
