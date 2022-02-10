using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicApi.Models.Artist;
using MusicApi.Models.Song;

namespace MusicApi.Models.Label
{
    public class LabelDetail
    {
        public int LabelId { get; set; }

        public string Name { get; set; }

        public int YearFounded { get; set; }

        public string Location { get; set; }

        public List<ArtistListItem> Artists { get; set; }

        public List<SongListItem> Songs { get; set; }

    }
}