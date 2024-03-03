using System.ComponentModel.DataAnnotations;

namespace MovieClubManager.Service.Genres.Contracts.Dto
{
    public class GetGenreDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int? Rate { get; set; }
    }
}