using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        // GetLabelById method
        public async Task<LabelDetail> GetLabelByIdAsync(int labelId)
        {
            var labelEntity = await _dbContext.Labels
                .FirstOrDefaultAsync(e => e.LabelId == labelId);

            return labelEntity is null ? null : new LabelDetail
            {
                LabelId = labelEntity.LabelId,
                Name = labelEntity.Name,
                YearFounded = labelEntity.YearFounded,
                Location = labelEntity.Location
            };
        }

        // GetAllLabels method
        public async Task<IEnumerable<LabelListItem>> GetAllLabelsAsync()
        {
            var labels = await _dbContext.Labels
                .Select(entity => new LabelListItem
                {
                    LabelId = entity.LabelId,
                    Name = entity.Name
                }).ToListAsync();
            return labels;
        }

        // UpdateLabel method
        public async Task<bool> UpdateLabelAsync(LabelUpdate request)
        {
            var labelEntity = await _dbContext.Labels.FindAsync(request.LabelId);

            if (labelEntity?.LabelId == null)
                return false;
            
            labelEntity.Name = request.Name;
            labelEntity.YearFounded = request.YearFounded;
            labelEntity.Location = request.Location;

            var numberOfChanges = await _dbContext.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        //DeleteLabel method
        public async Task<bool> DeleteLabelAsync(int labelId)
        {
            var labelEntity = await _dbContext.Labels.FindAsync(labelId);

            if (labelEntity?.LabelId == null)
                return false;

            _dbContext.Labels.Remove(labelEntity);
            return await _dbContext.SaveChangesAsync() == 1;
        }

        
    }
}