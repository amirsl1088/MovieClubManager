using MovieClubManager.Entities.Movies;
using MovieClubManager.Entities.Users;
using MovieClubManager.Service.Rents.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Test.Tools.Rents.Fctories
{
    public static class AddRentDtoFactory
    {
        public static AddRentDto Create(int? userid=null,int? movieid = null)
        {
            return  new AddRentDto
            {
                UserId = userid?? 1,
                MovieId = movieid?? 1
            };
        }
    }
}
