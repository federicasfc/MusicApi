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
        [Required]

        public int LabelId { get; set; }
        public LabelEntity Label { get; set; }

        //FK for many to many with Artists

        public List<ArtistEntity> Artists { get; set; }


    }
}