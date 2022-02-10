using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicApi.Models.Label;
using MusicApi.Models.Song;

namespace MusicApi.Models.Artist
{
    public class ArtistDetail
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public int NumberOfStudioAlbums { get; set; }

        public LabelListItem Label { get; set; }

        public List<SongListItem> Songs { get; set; }
    }
}