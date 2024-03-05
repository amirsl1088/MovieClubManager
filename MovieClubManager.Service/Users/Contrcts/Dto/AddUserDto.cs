using MovieClubManager.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace MovieClubManager.Service.Users.Contrcts.Dto
{
    public class AddUserDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        public Gender Gender { get; set; }
       
    }
}
