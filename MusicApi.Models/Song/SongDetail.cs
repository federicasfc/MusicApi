using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public int LabelId { get; set; }
        public int ArtistId { get; set; }
    }
}