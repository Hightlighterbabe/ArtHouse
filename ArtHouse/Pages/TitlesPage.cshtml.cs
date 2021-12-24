using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ArtHouse.Models;
using Microsoft.Extensions.DependencyInjection;
using ArtHouse.Repository.Interfaces;
namespace ArtHouse.Pages
{
    public class TitlesPageModel : PageModel
    {
        public List<Art> animes = new List<Art>();
        public async Task OnGet()
        {
            string searchText = HttpContext.Request.Query["search_input"];
            var rep = HttpContext.RequestServices.GetService<IArtR>();
            if(searchText == null)
            {
                var task = rep.GetAllAnimeAsync();
                animes = await task;
            }
            else
            {
                animes = await rep.SearchArt(searchText);
            }
        }
    }
}
