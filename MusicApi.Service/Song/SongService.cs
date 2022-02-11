using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicApi.Data;
using MusicApi.Data.Entities;
using MusicApi.Models.Artist;
using MusicApi.Models.Label;
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
                Name = request.Name,
                YearReleased = request.YearReleased,
                Genre = request.Genre


            }; //if many to many relationship between song and artist doesn't work, add an include to the artist so that the artist's labelid is accessible and can be automatically matched to the song's labelId (have been doing it manually to test so far)

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
            .Include(s => s.Label)
            .Include(s => s.Artists)
            .FirstOrDefaultAsync(e => e.SongId == songId);

            if (songEntity is null)
                return null;

            return new SongDetail
            {

                SongId = songEntity.SongId,
                Name = songEntity.Name,
                RunTime = songEntity.RunTime,
                YearReleased = songEntity.YearReleased,
                Genre = songEntity.Genre,
                Album = songEntity.Album,
                Label = new LabelListItem()
                {
                    LabelId = songEntity.Label?.LabelId ?? 0,
                    Name = songEntity.Label?.Name ?? "Song not associated with a label"
                },
                Artists = songEntity.Artists.Select(entity => new ArtistListItem
                {
                    ArtistId = entity.ArtistId,
                    Name = entity.Name
                }).ToList()

            };

        }

        //GetSongByName

        public async Task<SongDetail> GetSongByNameAsync(string songName)
        {
            var songEntity = await _dbContext.Songs
            .Include(s => s.Label)
            .Include(s => s.Artists)
            .FirstOrDefaultAsync(e => e.Name == songName);

            if (songEntity is null)
                return null;

            return new SongDetail
            {

                SongId = songEntity.SongId,
                Name = songEntity.Name,
                RunTime = songEntity.RunTime,
                YearReleased = songEntity.YearReleased,
                Genre = songEntity.Genre,
                Album = songEntity.Album,
                Label = new LabelListItem()
                {
                    LabelId = songEntity.Label?.LabelId ?? 0,
                    Name = songEntity.Label?.Name ?? "Song not associated with a label"
                },
                Artists = songEntity.Artists.Select(entity => new ArtistListItem
                {
                    ArtistId = entity.ArtistId,
                    Name = entity.Name
                }).ToList()

            };

        }

        //UpdateSongAsync 

        //For future improvements: Make it so that not all of the properties HAVE to be updated (remove annotations); Add in some logic that tests if the target property that we don't want to update is null. If not null, don't overwrite to null when it's not adjusted in the method; maybe look at RR for reference?

        public async Task<bool> UpdateSongAsync(SongUpdate request)
        {
            var songEntity = await _dbContext.Songs.FindAsync(request.SongId);

            if (songEntity is null)
                return false;

            //Conditionals are ensuring that the information only gets updated, if new information is being inputed
            //Check is using default values (null for strings and 0 for ints)
            //Also, additional function is that built-in .IsNullOrWhiteSpace ensures that accidental spaces from user don't get recorded as new info

            if (!string.IsNullOrWhiteSpace(request.Name))
                songEntity.Name = request.Name;

            if (!string.IsNullOrWhiteSpace(request.RunTime))
                songEntity.RunTime = request.RunTime;

            if (request.YearReleased != default)
                songEntity.YearReleased = request.YearReleased;

            if (!string.IsNullOrWhiteSpace(request.Genre))
                songEntity.Genre = request.Genre;

            if (!string.IsNullOrWhiteSpace(request.Album))
                songEntity.Album = request.Album;

            var numberOfChanges = await _dbContext.SaveChangesAsync();

            return numberOfChanges == 1; //just as a reminder, this number is signifying the number of rows in the db changed
        }

        //DeleteSong

        public async Task<bool> DeleteSongAsync(int songId)
        {
            var songEntity = await _dbContext.Songs.FindAsync(songId);

            if (songEntity is null)
                return false;

            _dbContext.Songs.Remove(songEntity);
            var numberOfChanges = await _dbContext.SaveChangesAsync(); //set to bool, so if only one row is changed, will return true
            return numberOfChanges == 1;


        }

        //AssignSongToArtists Method

        public async Task<bool> AssignSongToArtists(int songId, int artistId)
        {
            var songToAdd = await _dbContext.Songs.FindAsync(songId);

            if (songToAdd is null)
                return false;

            var artistToAssignTo = await _dbContext.Artists.Include(a => a.Songs).FirstOrDefaultAsync(a => a.ArtistId == artistId);

            if (artistToAssignTo is null)
                return false;


            artistToAssignTo.Songs.Add(songToAdd);

            var numberOfChanges = await _dbContext.SaveChangesAsync();

            return numberOfChanges == 1;


        } //Http Put? will have to adjust route to account for both int parameters

        //Assigning Songs to Artists
        //Task<bool> 
        //Parameters: songId and artistId
        //songToAdd = await _dbContext.Songs.FindAsync(songId)
        //Dealing with a SongEntity first - validate not null
        //Then dealing with ArtistEntity - validate not null
        //artistToAssing already has list of songs so just add song gotten with id to that property Songs
        //artistToAssign.Songs.Add(songToAdd)
        //savechangesasync

    }
}