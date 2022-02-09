using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApi.Models.Artist
{
    public class ArtistUpdate
    {
        [Required]
        public int ArtistId { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "{0} must be at least {1} characters long.")]
        [MaxLength(50, ErrorMessage = "{0} must contain no more than {1} characters.")]
        public string Name { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "{0} must be at least {1} characters long.")]
        [MaxLength(50, ErrorMessage = "{0} must contain no more than {1} characters.")]
        public string Genre { get; set; }

        [Required]
        public int NumberOfStudioAlbums { get; set; }
    }
}