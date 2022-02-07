using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApi.Data.Entities
{
    public class LabelEntity
    {
        [Key]
        public int LabelId { get; set; }

        [Required]

        public string Name { get; set; }

        [Required]

        public int YearFounded { get; set; }

        public string Location { get; set; }

        //foreign key Navigation property - will correspond to ModelBuilder method int AppDbCtx

        public List<ArtistEntity> Artists { get; set; }

        public List<SongEntity> Songs { get; set; }



    }
}