using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicApi.Models.Song;

namespace MusicApi.Models.Artist
{
    public class ArtistSongsListItem
    {
        public List<SongListItem> Songs { get; set; }
    }
}