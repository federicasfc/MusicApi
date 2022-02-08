using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicApi.Data;
using MusicApi.Data.Entities;
using MusicApi.Models.Label;

namespace MusicApi.Service.Label
{
    public class LabelService : ILabelService
    {
        private readonly ApplicationDbContext _dbContext;

        public LabelService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //CreateLabel method
        public async Task<bool> CreateLabelAsync(LabelCreate request)
        {
            var labelEntity = new LabelEntity
            {
                Name = request.Name,
                YearFounded = request.YearFounded
            };

            _dbContext.Labels.Add(labelEntity);

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        
    }
}