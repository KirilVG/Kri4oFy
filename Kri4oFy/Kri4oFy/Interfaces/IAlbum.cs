using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kri4oFy.Constants;

namespace Kri4oFy.Interfaces
{
    internal interface IAlbum:ISongCollection
    {
        string ArtistName { get; set; }

        DateTime DateOfCreation { get; set; }

        GenreEnum Genre { get; set; }
    }
}
