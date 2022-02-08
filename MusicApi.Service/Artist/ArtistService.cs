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
    }
}