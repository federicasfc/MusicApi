using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicApi.Models.Artist;
using MusicApi.Service.Artist;

namespace MusicApi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        // field to access service methods
        private readonly IArtistService _artistService;

        // contructor
        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        // CreateArtist endpoint 
        
        [HttpPost]
        /* WORKS */
        public async Task<IActionResult> CreateArtist([FromForm] ArtistCreate request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _artistService.CreateArtistAsync(request) == false)
                return BadRequest("Artist could not be created."); 


            return Ok("Artist created successfully");
        }

        // GetAllArtists endpoint

        [HttpGet]
        /* WORKS */
        public async Task<IActionResult> GetAllArtists()
        {
            var artists = await _artistService.GetAllArtistsAsync();
            return Ok(artists);
        }

        // GetArtistById endpoint

        [HttpGet("{artistId:int}")]
        /* WORKS */
        public async Task<IActionResult> GetArtistById([FromRoute] int artistId)
        {
            var detail = await _artistService.GetArtistByIdAsync(artistId);

            return detail is not null
            ? Ok(detail)
            : NotFound();

        }
    }
}