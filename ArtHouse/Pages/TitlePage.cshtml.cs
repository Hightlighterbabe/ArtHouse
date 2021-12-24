using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ArtHouse.Helpers;
using ArtHouse.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using ArtHouse.Models;
using Microsoft.AspNetCore.Http;

namespace ArtHouse.Pages
{
    public class TitlePageModel : PageModel
    {
        public Art anime = new Art();
        public bool isLike;
        public Like like = new Like();
        public async Task<RedirectResult> OnGet()
        {
            string string_id = HttpContext.Request.Query["anime_id"];
            var rep = HttpContext.RequestServices.GetService<IArtR>();
            var rep2 = HttpContext.RequestServices.GetService<ILikerArtR>();
            if (string_id == null)
                return Redirect("/Index");
            else
            {
                anime = await rep.GetArtById(Convert.ToInt32(string_id));
                if (HttpContext.Session.GetString("role") == Roles.Guest.ToString())
                {
                    isLike = true;

                }
                else
                {
                    like = new Like(HttpContext.Session.Get<User>("Current_user").Id);
                    like.Art_id = Convert.ToInt32(HttpContext.Request.Query["anime_id"]);
                    isLike = rep2.IsLiked(like);
                }
                return null;
            }
                
            
        }
        public async Task<RedirectResult> OnPost()
        {
            var rep = HttpContext.RequestServices.GetService<ILikerArtR>();
            like = new Like(HttpContext.Session.Get<User>("Current_user").Id);
            like.Art_id = Convert.ToInt32(HttpContext.Request.Query["anime_id"]);
            await rep.AddLikeArt(like);
            return Redirect($"/TitlePage?anime_id={Convert.ToInt32(HttpContext.Request.Query["anime_id"])}");
        }
    }
}
