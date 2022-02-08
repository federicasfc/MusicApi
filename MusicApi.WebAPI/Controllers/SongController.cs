using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicApi.Models.Song;
using MusicApi.Service.Song;

namespace MusicApi.WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        //Field with access to Service methods

        private readonly ISongService _songService;

        //Constructor

        public SongController(ISongService songService)
        {

            _songService = songService;

        }

        //CreateSongs

        [HttpPost]

        public async Task<IActionResult> CreateSong([FromForm] SongCreate request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _songService.CreateSongAsync(request) == false)
                return BadRequest("Song could not be created.");

            return Ok("Song created successfully");


        } //works

        //GetAllSongs

        [HttpGet]

        public async Task<IActionResult> GetAllSongs()
        {
            var songs = await _songService.GetAllSongsAsync();

            return Ok(songs);
        }





    }
}