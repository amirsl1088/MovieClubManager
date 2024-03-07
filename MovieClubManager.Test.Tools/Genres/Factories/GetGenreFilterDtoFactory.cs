﻿using MovieClubManager.Service.Genres.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Test.Tools.Genres.Factories
{
    public static class GetGenreFilterDtoFactory
    {
        public static GetGenreFilterDto Create(string? title=null)
        {
            return new GetGenreFilterDto
            {
                Title = title ?? null
            };
        }
    }
}