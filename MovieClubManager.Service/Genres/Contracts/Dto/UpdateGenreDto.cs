﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Service.Genres.Contracts.Dto
{
    public class UpdateGenreDto
    {
        [Required]
        public string Title { get; set; }
    }
}
