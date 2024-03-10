using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Service.Movies.Contracts.Dto
{
    public class GetMovieDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PublishYear { get; set; }
        public decimal DailyPriceRent { get; set; }
        public int AgeLimit { get; set; }
        public decimal DelayPenalty { get; set; }
        public decimal Duration { get; set; }
        public string Director { get; set; }
        public string? Description { get; set; }
        public int GenreId { get; set; }
    }
}
