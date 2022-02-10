using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicApi.Models.Artist;
using MusicApi.Models.Label;

namespace MusicApi.Models.Song
{
    public class SongDetail
    {
        public int SongId { get; set; }

        public string Name { get; set; }

        public string RunTime { get; set; }

        public int YearReleased { get; set; }

        public string Genre { get; set; }

        public string Album { get; set; }

        // Songs can only have one label, so only one listitem is necessary
        public LabelListItem Label { get; set; }

        //Songs can have many artists, so a collection of artistlistitems is necessary

        public List<ArtistListItem> Artists { get; set; }
    }
}