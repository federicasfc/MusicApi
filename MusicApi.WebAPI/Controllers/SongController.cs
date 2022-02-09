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


        } //works / haven't tested with adjustment to LabelId yet.

        //GetAllSongs

        [HttpGet]

        public async Task<IActionResult> GetAllSongs()
        {
            var songs = await _songService.GetAllSongsAsync();

            return Ok(songs);
        } //works

        //GetSongById

        [HttpGet("{songId:int}")]

        public async Task<IActionResult> GetSongById([FromRoute] int songId)
        {
            var detail = await _songService.GetSongByIdAsync(songId);

            if (detail is null)
                return NotFound();

            return Ok(detail);
        } //works with new many to many rship

        //UpdateSong

        [HttpPut]

        public async Task<IActionResult> UpdateSongById([FromForm] SongUpdate request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _songService.UpdateSongAsync(request) == false)
                return BadRequest("Song could not be updated.");

            return Ok("Song updated successfully.");

        } //works

        //Delete Songs

        [HttpDelete("{songId:int}")]

        public async Task<IActionResult> DeleteSong([FromRoute] int songId)
        {
            if (await _songService.DeleteSongAsync(songId) == false)
                return BadRequest($"Song {songId} could not be deleted");

            return Ok($"Song {songId} was deleted successfully");
        } //works

        //AssignSongToArtist

        [HttpPut("{songId:int}/{artistId:int}")]

        public async Task<IActionResult> AssignSongToArtists([FromRoute] int songId, [FromRoute] int artistId)
        {
            if (songId == 0 || artistId == 0)
                return NotFound("Song or Artist could not be found");

            if (await _songService.AssignSongToArtists(songId, artistId) == false)
                return BadRequest("Song could not be added to Artists");

            return Ok("Song added to Artists");
        } //works!!











    }
}
//Potential issues:
/*
- If Label and artist have many to one relationship, that should mean that for every song created, the LabelId in artist should always match the LabelId in songs. So, really, we shouldn't be able to affect the labelId in the song creation, and just add the artistId 
- For the moment, I have manually matched up the artist's label Id to the song's label Id -- also revisit this, because writing it out made it make less sense
- A way to test this will be to intentionally create a new song with a differing labelId to the one from the artist id that is inputed

*/