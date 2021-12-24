using System.Collections.Generic;
using ArtHouse.Models;
using System.Threading.Tasks;

namespace ArtHouse.Repository.Interfaces
{
    interface ICommentR
    {
        Task<List<Comment>> GetCommentsByArtIdAsync(int id);
        Task AddComment(Comment comment);
    }
}
