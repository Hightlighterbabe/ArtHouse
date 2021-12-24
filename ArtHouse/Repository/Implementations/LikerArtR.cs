using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using ArtHouse.Repository.Interfaces;
using ArtHouse.Models;
using System.Data;
namespace ArtHouse.Repository.Implementations
{
    public class LikerArtR : ILikerArtR
    {
        private string connection = "Host=localhost; " + "Username=postgres; " + "Password=Werrew123@; " + "Database=ArtDB";
        public async Task AddLikeArt(Like like)
        {
            await using (var connection = new NpgsqlConnection(this.connection))
            {
                await connection.OpenAsync();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.Parameters.AddWithValue("@user_id", like.User_id);
                    cmd.Parameters.AddWithValue("@art_id", like.Art_id);
                    cmd.CommandText = "insert into likedanime(user_id, art_id) values(@user_id, @art_id)";
                    await cmd.ExecuteNonQueryAsync();
                }
            } 
        }

        public async Task<List<Art>> GetLikedAnimes(int user_id)
        {
            List<Art> likes = new List<Art>();
            await using (var connection = new NpgsqlConnection(this.connection))
            {
                await connection.OpenAsync();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.Parameters.AddWithValue("@user_id", user_id);
                    cmd.CommandText = "select likedanime.art_id, img_path, art_shows.name from likedanime join art_shows on likedanime.art_id = anime_shows.art_id where likedart.user_id = @user_id";
                    using (var rdr = cmd.ExecuteReader())
                    {
                        Art like;
                        while (rdr.Read())
                        {
                            like = new Art(rdr.GetInt32(rdr.GetOrdinal("art_id")));
                            like.Name = rdr.GetString(rdr.GetOrdinal("name"));
                            if (!await rdr.IsDBNullAsync("img_path"))
                                like.img_path = rdr.GetString(rdr.GetOrdinal("img_path"));
                            else
                                like.img_path = "/img/nopreview.jpg";
                            likes.Add(like);
                        }
                    }
                }
                return likes;
            }
        }

        public bool IsLiked(Like like)
        {
            using (var connection = new NpgsqlConnection(this.connection))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.Parameters.AddWithValue("@art_id", like.Art_id);
                    cmd.Parameters.AddWithValue("@user_id", like.User_id);
                    cmd.CommandText = "select * from likedart where art_id = @art_id and user_id=@user_id";
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var check = $"{rdr.GetInt32(rdr.GetOrdinal("art_id"))}";
                            if (check != null)
                                return true;//найдено совпадение
                        }
                    }
                    return false;//совпадений не найдено
                }
            }
        }
    }
}
