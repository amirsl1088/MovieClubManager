using MovieClubManager.Entities.Users;
using MovieClubManager.Service.Users.Contrcts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Test.Tools.Users.Factories
{
    public static class AddUserDtoFactory
    {
        public static AddUserDto Create()
        {
            return new AddUserDto
            {
                FirstName = "hkj",
                LastName = "juhkurg",
                Age = 18,
                Adress = "uhku.j",
                MobileNumber = "ukhjh",
                Gender = Gender.male,
                
            };
        }
    }
}
