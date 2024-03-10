using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieClubManager.Service.Movies.Contracts.Dto
{
    public class AddMovieDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string PublishYear { get; set; }
        [Required]
        public decimal DailyPriceRent { get; set; }
        [Required]
        public int AgeLimit { get; set; }
        [Required]
        public decimal DelayPenalty { get; set; }
        [Required]
        public decimal Duration { get; set; }
        [Required]
        public string Director { get; set; }
        [ForeignKey("GenreId")]
        public int GenreId { get; set; }
        public string? Description { get; set; }
       
    }
}
