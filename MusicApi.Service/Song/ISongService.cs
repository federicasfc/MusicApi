using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicApi.Models.Song;

namespace MusicApi.Service.Song
{
    public interface ISongService
    {
        Task<bool> CreateSongAsync(SongCreate request);

        Task<IEnumerable<SongListItem>> GetAllSongsAsync();

        Task<SongDetail> GetSongByIdAsync(int songId);

        Task<SongDetail> GetSongByNameAsync(string songName);

        Task<bool> UpdateSongAsync(SongUpdate request);

        Task<bool> DeleteSongAsync(int songId);

        Task<bool> AssignSongToArtists(int songId, int artistId);





    }
}