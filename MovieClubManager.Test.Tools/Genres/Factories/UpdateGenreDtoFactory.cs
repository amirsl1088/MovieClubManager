using MovieClubManager.Service.Genres.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Test.Tools.Genres.Factories
{
    public static class UpdateGenreDtoFactory
    {
        public static UpdateGenreDto Create()
        {
            return new UpdateGenreDto
            {
                Title = "gjfjhkj,"
            };
        }
    }
}
