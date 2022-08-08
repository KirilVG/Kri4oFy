﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kri4oFy.Constants;

namespace Kri4oFy.Interfaces
{
    public interface ISongCollection
    {
        string CollectionName { get; set; }

        SongCollectionTypeEnum Type { get; set; }

        int Length { get; }

        List<ISong> Songs { get; }

        string GetFileString { get; }

        int IndexOfSongWithName(string songName);

        string GetInfo { get; }

        string GetSongsInfo { get; }

        ISong RemoveSongByName(string songName);

        bool AddSong(ISong song);

    }
}
