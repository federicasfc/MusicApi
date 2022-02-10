using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicApi.Data;
using MusicApi.Data.Entities;
using MusicApi.Models.Artist;
using Microsoft.EntityFrameworkCore;
using MusicApi.Models.Label;
using MusicApi.Models.Song;

namespace MusicApi.Service.Artist
{
    public class ArtistService : IArtistService
    {
        private readonly ApplicationDbContext _dbContext;

        //Constructor
        public ArtistService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /* CRUD */

        // CreateArtist method
        public async Task<bool> CreateArtistAsync(ArtistCreate request)
        {
            var artistEntity = new ArtistEntity
            {
                Name = request.Name,
                Genre = request.Genre,
                LabelId = request.LabelId
            };

            _dbContext.Artists.Add(artistEntity);

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        // GetAllArtists method
        public async Task<IEnumerable<ArtistListItem>> GetAllArtistsAsync()
        {
            var artists = await _dbContext.Artists
                .Select(entity => new ArtistListItem
                {
                    ArtistId = entity.ArtistId,
                    Name = entity.Name
                }).ToListAsync();
            return artists;
        }

        // GetArtistById method
        public async Task<ArtistDetail> GetArtistByIdAsync(int artistId)
        {
            //Find the first artist that has the given Id

            var artistEntity = await _dbContext.Artists
                .Include(a => a.Label)
                .Include(a => a.Songs)
                .FirstOrDefaultAsync(e => e.ArtistId == artistId);

            //If artistEntity is null then return null, otherwise initialize and return a new ArtistDetail

            return artistEntity is null ? null : new ArtistDetail
            {
                ArtistId = artistEntity.ArtistId,
                Name = artistEntity.Name,
                Genre = artistEntity.Genre,
                NumberOfStudioAlbums = artistEntity.NumberOfStudioAlbums,
                Label = new LabelListItem()
                {
                    LabelId = artistEntity.Label.LabelId,
                    Name = artistEntity.Label.Name
                },
                Songs = artistEntity.Songs.Select(entity => new SongListItem
                {
                    SongId = entity.SongId,
                    Name = entity.Name

                }).ToList()

            };
        } //works with many to many refactor!

        // GetArtistByName method
        public async Task<ArtistDetail> GetArtistByNameAsync(string artistName)
        {
            //Find the first artist that has the given Name

            var artistEntity = await _dbContext.Artists
                .FirstOrDefaultAsync(e => e.Name == artistName);

            //If artistEntity is null then return null, otherwise initialize and return a new ArtistDetail

            return artistEntity is null ? null : new ArtistDetail
            {
                ArtistId = artistEntity.ArtistId,
                Name = artistEntity.Name,
                Genre = artistEntity.Genre,
                NumberOfStudioAlbums = artistEntity.NumberOfStudioAlbums,
                //LabelId = artistEntity.LabelId
            }; //Will not work until refactored with many to many 
        }

        // UpdateArtist method
        public async Task<bool> UpdateArtistAsync(ArtistUpdate request)
        {
            // Find the artist and validate the ArtistId exists

            var artistEntity = await _dbContext.Artists.FindAsync(request.ArtistId);


            //Original if for reference:
            //if (artistEntity?.ArtistId == null)
            if (artistEntity is null)
                return false;

            // update entity's properties on the condition that they are addressed by request
            // if not addressed, they will retain previous value instead of being updated to null

            if (!string.IsNullOrWhiteSpace(request.Name))
                artistEntity.Name = request.Name;

            if (!string.IsNullOrWhiteSpace(request.Genre))
                artistEntity.Genre = request.Genre;

            // conditional prevents int from being updated to 0 if property isn't addressed
            // only loss of functionality is that now it cannot be updated to 0 if it is currently a non zero value

            if (request.NumberOfStudioAlbums != default)
                artistEntity.NumberOfStudioAlbums = request.NumberOfStudioAlbums;

            // Save the changes to the database and capture how many rows were updated

            var numberOfChanges = await _dbContext.SaveChangesAsync();

            // numberOfChanges is stated to be equal to 1 because only one row is updated

            return numberOfChanges == 1;
        }

        // DeleteArtist method
        public async Task<bool> DeleteArtistAsync(int artistId)
        {
            // Find the artist and validate the ArtistId exists
            var artistEntity = await _dbContext.Artists.FindAsync(artistId);

            if (artistEntity?.ArtistId == null)
                return false;

            //Remove the note from the DbContet and assert that the one change was saved

            _dbContext.Artists.Remove(artistEntity);
            return await _dbContext.SaveChangesAsync() == 1;
        }
    }
}