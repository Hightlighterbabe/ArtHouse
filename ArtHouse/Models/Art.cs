using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ArtHouse.Models
{
    public class Art
    {
        public readonly int Id;
        public string img_path { get; set; }
        public DateTime year { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public Art(int id)
        {
            Id = id;
        }
        public Art()
        {

        }
    }
}
