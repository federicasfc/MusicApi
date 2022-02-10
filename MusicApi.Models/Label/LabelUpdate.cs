using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApi.Models.Label
{
    public class LabelUpdate
    {
        [Required]
        public int LabelId { get; set; }

        [MinLength(2, ErrorMessage = "{0} must be at least {1} characters long.")]
        [MaxLength(50, ErrorMessage = "{0} must contain no more than {1} characters.")]
        public string Name { get; set; }

        public int YearFounded { get; set; }

        [MinLength(2, ErrorMessage = "{0} must be at least {1} characters long.")]
        [MaxLength(50, ErrorMessage = "{0} must contain no more than {1} characters.")]
        public string Location { get; set; }
    }
}