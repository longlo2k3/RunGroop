﻿using Object.Data.Enum;
using Object.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Object.ViewModel
{
    public class CreateClubViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Address Address { get; set; }
        public IFormFile Image { get; set; }

        public ClubCategory ClubCategory { get; set; }
    }
}
