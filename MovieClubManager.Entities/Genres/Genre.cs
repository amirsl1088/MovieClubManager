using MovieClubManager.Entities.Movies;

namespace MovieClubManager.Entities.Genres
{
    public class Genre
    {
        public Genre()
        {
            Movies = new List<Movie>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public int Rate { get; set; }
        public List<Movie> Movies { get; set; }
    }
}