using MovieClubManager.Entities.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Service.Rents.Contracts.Dtos
{
    public class UpdateRentDto
    {
        public decimal MovieRate { get; set; }
        public DateTime GiveBack { get; set; }
    }
}
