using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicApi.Models.Label;

namespace MusicApi.Service.Label
{
    public interface ILabelService
    {
         Task<bool> CreateLabelAsync(LabelCreate request);

         Task<LabelDetail> GetLabelByIdAsync(int labelId);

         Task<LabelDetail> GetLabelByNameAsync(string labelName);

         Task<IEnumerable<LabelListItem>> GetAllLabelsAsync();

         Task<LabelArtistsListItem> GetArtistsByLabelAsync(string labelName);

         Task<bool> UpdateLabelAsync(LabelUpdate request);

         Task<bool> DeleteLabelAsync(int labelId);
    }
}