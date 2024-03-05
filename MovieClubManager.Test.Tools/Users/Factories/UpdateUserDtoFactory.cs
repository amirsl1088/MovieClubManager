using MovieClubManager.Entities.Users;
using MovieClubManager.Service.Users.Contrcts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Test.Tools.Users.Factories
{
    public static class UpdateUserDtoFactory
    {
        public static UpdateUserDto Create()
        {
            return new UpdateUserDto
            {
                FirstName = "dvv",
                LastName = "wefw",
                Age = 23,
                Adress = "rgverg",
                Gender = Gender.male,
                MobileNumber = "wfwef"
            };
        }
    }
}
