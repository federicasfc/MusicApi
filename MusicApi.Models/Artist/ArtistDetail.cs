using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApi.Models.Artist
{
    public class ArtistDetail
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public int NumberOfStudioAlbums { get; set; }
        public int LabelId { get; set; }
    }
}