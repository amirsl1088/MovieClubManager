using MovieClubManager.Service.Movies.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Test.Tools.Movies.Factories
{
    public static class GetMovieFilterDtoFactory
    {
        public static GetMovieFilterDto Create(string? name = null)
        {
            return new GetMovieFilterDto
            {
                Name = name ?? null
            };
        }
    }
}
