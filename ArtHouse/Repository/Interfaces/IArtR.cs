using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtHouse.Repository.Interfaces
{
    public interface IArtR
    { 
        Task<List<Models.Art>> GetAllAnimeAsync();
        Task AddAnime(Models.Art art);
        Task<List<Models.Art>> SearchArt(string input);
        Task<Models.Art> GetArtById(int id);
        List<Models.Studios> GetAllStudios();
    }
}
