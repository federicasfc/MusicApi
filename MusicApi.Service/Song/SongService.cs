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

        //Needs adjusted so that the LabelId of the song is automatically set to whatever the LabelId is of the artist corresponding to the ArtistId it takes in

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

        //GetSongById

        public async Task<SongDetail> GetSongByIdAsync(int songId)
        {
            var songEntity = await _dbContext.Songs
            .FirstOrDefaultAsync(e => e.SongId == songId);

            if (songEntity is null)
                return null;

            return new SongDetail
            {

                SongId = songEntity.SongId,
                ArtistId = songEntity.ArtistId,
                LabelId = songEntity.LabelId,
                Name = songEntity.Name,
                RunTime = songEntity.RunTime,
                YearReleased = songEntity.YearReleased,
                Genre = songEntity.Genre,
                Album = songEntity.Album
            };

        }

        //UpdateSongAsync 

        //For future improvements: Make it so that not all of the properties HAVE to be updated (remove annotations); Add in some logic that tests if the target property that we don't want to update is null. If not null, don't overwrite to null when it's not adjusted in the method; maybe look at RR for reference?

        public async Task<bool> UpdateSongAsync(SongUpdate request)
        {
            var songEntity = await _dbContext.Songs.FindAsync(request.SongId);

            if (songEntity?.SongId is null)
                return false;

            songEntity.Name = request.Name;
            songEntity.RunTime = request.RunTime;
            songEntity.YearReleased = request.YearReleased;
            songEntity.Genre = request.Genre;
            songEntity.Album = request.Album;

            var numberOfChanges = await _dbContext.SaveChangesAsync();

            return numberOfChanges == 1; //just as a reminder, this number is signifying the number of rows in the db changed
        }

        //DeleteSong

        public async Task<bool> DeleteSongAsync(int songId)
        {
            var songEntity = await _dbContext.Songs.FindAsync(songId);

            if (songEntity?.SongId is null)
                return false;

            _dbContext.Songs.Remove(songEntity);
            var numberOfChanges = await _dbContext.SaveChangesAsync(); //set to bool, so if only one row is changed, will return true
            return numberOfChanges == 1;


        }

    }
}