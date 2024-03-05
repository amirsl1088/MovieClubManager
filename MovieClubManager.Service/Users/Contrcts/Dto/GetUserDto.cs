using MovieClubManager.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Service.Users.Contrcts.Dto
{
    public class GetUserDto
    {
        [Required]
        public int Id { get; set; }
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
        public int? Rate { get; set; }
    }
}
