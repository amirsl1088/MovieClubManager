using System.ComponentModel.DataAnnotations;

namespace MovieClubManager.Service.Genres.Contracts.Dto
{
    public class AddGenreDto
    {
        [Required]
        public string Title { get; set; }
    }
}