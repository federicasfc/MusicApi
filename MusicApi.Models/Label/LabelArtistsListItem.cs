using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicApi.Models.Artist;

namespace MusicApi.Models.Label
{
    public class LabelArtistsListItem
    {
        public List<ArtistListItem> Artists { get; set; }
    }
}