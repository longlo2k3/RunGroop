﻿using Object.Data.Enum;
using Object.Models;

namespace Object.ViewModel
{
    public class CreateRaceViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Address Address { get; set; }
        public IFormFile Image { get; set; }

        public RaceCategory RaceCategory { get; set; }
    }
}
