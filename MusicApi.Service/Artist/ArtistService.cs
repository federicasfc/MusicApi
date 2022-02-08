using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicApi.Data;
using MusicApi.Data.Entities;
using MusicApi.Models.Artist;
using Microsoft.EntityFrameworkCore;

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
                .FirstOrDefaultAsync(e => e.ArtistId == artistId);

            //If artistEntity is null then return null, otherwise initialize and return a new ArtistDetail

            return artistEntity is null ? null : new ArtistDetail
            {
                ArtistId = artistEntity.ArtistId,
                Name = artistEntity.Name,
                Genre = artistEntity.Genre,
                NumberOfStudioAlbums = artistEntity.NumberOfStudioAlbums,
                LabelId = artistEntity.LabelId
            };
        }

        // UpdateArtist method
        public async Task<bool> UpdateArtistAsync(ArtistUpdate request)
        {
            //Find the artist and validate the ArtistId exists

            var artistEntity = await _dbContext.Artists.FindAsync(request.ArtistId);

            if (artistEntity?.ArtistId == null)
                return false;

            // update the entity's properties
            artistEntity.Name = request.Name;
            artistEntity.Genre = request.Genre;
            artistEntity.NumberOfStudioAlbums = request.NumberOfStudioAlbums;

            //Save the changes to the database and capture how many rows were updated

            var numberOfChanges = await _dbContext.SaveChangesAsync();

            //numberOfChanges is stated to be equal to 1 because only one row is updated

            return numberOfChanges == 1;
        }
    }
}