﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtHouse.Models
{
    public class Like
    {
        public readonly int User_id;
        public int Art_id { get; set; }
        public Like(int user_id)
        {
            User_id = user_id;
        }
        public Like()
        {

        }
    }
}
