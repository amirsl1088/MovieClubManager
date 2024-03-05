﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Service.Movies.Contracts.Dto
{
    public class UpdateMovieDto
    {
        public string Name { get; set; }
        public string PublishYear { get; set; }
        public int DailyPriceRent { get; set; }
        public int AgeLimit { get; set; }
        public int DelayPenalty { get; set; }
        public double Duration { get; set; }
        public string Director { get; set; }
        public string? Description { get; set; }
        public int GenreId { get; set; }
    }
}