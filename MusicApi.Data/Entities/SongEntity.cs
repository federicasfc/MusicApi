using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApi.Data.Entities
{
    public class SongEntity
    {
        [Key]
        public int SongId { get; set; }

        [Required]

        public string Name { get; set; }

        public string RunTime { get; set; }

        [Required]

        public int YearReleased { get; set; }

        [Required]
        public string Genre { get; set; }

        public string Album { get; set; }

        //Foreign key navigation properties
        //Label and Song have a one to many relationship, so Song only takes in one label and is connected by the LabelId
        [Required]

        public int LabelId { get; set; }
        public LabelEntity Label { get; set; }

        //Artist and Song have a new many to many relationship, so Song is able to take in a list of different artists
        //Accounts for the fact that songs can have more than one artist

        public List<ArtistEntity> Artists { get; set; }


    }
}