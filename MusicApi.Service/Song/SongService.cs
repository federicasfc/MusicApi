using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicApi.Data;
using MusicApi.Data.Entities;
using MusicApi.Models.Song;

namespace MusicApi.Service.Song
{
    public class SongService : ISongService
    {
        //Field for db access
        private readonly ApplicationDbContext _dbContext;

        //Constructor

        public SongService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        //CreateSong

        public async Task<bool> CreateSongAsync(SongCreate request)
        {
            var songEntity = new SongEntity
            {
                SongId = request.SongId,
                LabelId = request.LabelId,
                ArtistId = request.ArtistId,
                Name = request.Name,
                YearReleased = request.YearReleased,
                Genre = request.Genre


            };

            _dbContext.Songs.Add(songEntity);

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        //GetAllSongs

        public async Task<IEnumerable<SongListItem>> GetAllSongsAsync()
        {
            var songs = await _dbContext.Songs
            .Select(entity => new SongListItem
            {
                SongId = entity.SongId,
                Name = entity.Name
            }).ToListAsync();

            return songs;
        }

    }
}