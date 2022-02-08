using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicApi.Models.Artist;

namespace MusicApi.Service.Artist
{
    public interface IArtistService
    {
        Task<bool> CreateArtistAsync(ArtistCreate request);
    }
}