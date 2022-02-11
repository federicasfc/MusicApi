using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApi.Data.Entities
{
    public class ArtistEntity
    {
        [Key]
        public int ArtistId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Genre { get; set; }

        public int NumberOfStudioAlbums { get; set; }

        //Foreign Key navigation properties

        public int? LabelId { get; set; }

        public LabelEntity Label { get; set; }

        public List<SongEntity> Songs { get; set; }
    }
}