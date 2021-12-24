using System.Collections.Generic;
using System.Threading.Tasks;
using ArtHouse.Models;
namespace ArtHouse.Repository.Interfaces
{
    interface ILikerArtR
    {
        Task<List<Art>> GetLikedAnimes(int user_id);
        Task AddLikeArt(Like like);
        bool IsLiked(Like like);
    }
}
