using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApi.Models.Song
{
    public class SongUpdate
    {

        public int SongId { get; set; }


        [MinLength(2, ErrorMessage = "{0} must be at least {1} characters long.")]
        [MaxLength(100, ErrorMessage = "{0} must contain no more than {1} characters.")]

        public string Name { get; set; }


        [MinLength(2, ErrorMessage = "{0} must be at least {1} characters long.")]
        [MaxLength(10, ErrorMessage = "{0} must contain no more than {1} characters.")]

        public string RunTime { get; set; }



        public int YearReleased { get; set; }


        [MinLength(2, ErrorMessage = "{0} must be at least {1} characters long.")]
        [MaxLength(100, ErrorMessage = "{0} must contain no more than {1} characters.")]
        public string Genre { get; set; }


        [MinLength(2, ErrorMessage = "{0} must be at least {1} characters long.")]
        [MaxLength(100, ErrorMessage = "{0} must contain no more than {1} characters.")]

        public string Album { get; set; }


    }
}