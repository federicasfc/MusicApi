using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApi.Models.Artist
{
    public class ArtistCreate
    {
        [Required]
        [MinLength(2, ErrorMessage = "{0} must be at least {1} characters long.")]
        public string Name { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "{0} must be at least {1} characters long.")]
        public string Genre { get; set; }

        [Required]
        public int LabelId { get; set; }
    }
}