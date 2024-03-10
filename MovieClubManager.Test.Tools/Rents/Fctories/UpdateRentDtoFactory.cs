using MovieClubManager.Service.Rents.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Test.Tools.Rents.Fctories
{
    public static class UpdateRentDtoFactory
    {
        public static UpdateRentDto Create(decimal? movierate = null, DateTime? giveback = null)
        {
            return new UpdateRentDto
            {
                MovieRate = movierate ?? 1,
                GiveBack = giveback ?? new DateTime(2023, 10, 10)
            };
        }
    }
}
