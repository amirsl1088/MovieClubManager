using MovieClubManager.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Test.Tools.Users.Builders
{
    public class UserBuilder
    {
        private readonly User _user;
        public UserBuilder()
        {
            _user = new User
            {
                FirstName = "jyhgkj,",
                LastName = "ukghjlk",
                Age = 22,
                Adress = "jyghjlk",
                MobileNumber = "rfssyt",
                Gender = Gender.female,
                CreatedAt=DateTime.UtcNow
            };
        }
        public UserBuilder WithFirstName(string firstname)
        {
            _user.FirstName = firstname;
            return this;
        }
        public UserBuilder WithLastName(string lastname)
        {
            _user.LastName = lastname;
            return this;
        }
        public UserBuilder WithAge(int age)
        {
            _user.Age = age;
            return this;
        }
        public UserBuilder WithGender(Gender gender)
        {
            _user.Gender = gender;
            return this;
        }
        public User Build()
        {
            return _user;
        }
    }
}
