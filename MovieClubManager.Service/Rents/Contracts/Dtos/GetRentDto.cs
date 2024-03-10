using MovieClubManager.Entities.Movies;
using MovieClubManager.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Service.Rents.Contracts.Dtos
{
    public class GetRentDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int MovieId { get; set; }
        [Required]
        public DateTime RentedAt { get; set; }
        public DateTime? GiveBack { get; set; }
        [Required]
        public decimal DailyPriceRent { get; set; }
        [Required]
        public decimal DelayPenalty { get; set; }
        public decimal? MovieRate { get; set; }
        public decimal? Cost { get; set; }
    }
}
