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


        [MinLength(2, ErrorMessage = "{0} must be at least {1} characters long.")]
        [MaxLength(50, ErrorMessage = "{0} must contain no more than {1} characters.")]
        public string Name { get; set; }


        [MinLength(2, ErrorMessage = "{0} must be at least {1} characters long.")]
        [MaxLength(50, ErrorMessage = "{0} must contain no more than {1} characters.")]
        public string Genre { get; set; }

        
        // [Range(1, 50, ErrorMessage = "{0} can only be updated to an interger that's at least {1} and no more than {2}.")]
        public int NumberOfStudioAlbums { get; set; }

        /* "Range" annotation above was introduced to provide a more specific error message in the event a user tried to update this prop's value to 0 from a non-zero value (which is prohibited by logic in ArtistService method). It was then removed because it would force the user to change this prop's value to a non-zero int when performing an artist update on any prop (NumberOfStudioAlbums's value will always intially be 0 after creating an artist). Allowing this prop's value to remain 0 was a priority because artists can be signed to a label without having any studio albums. */
    }
}