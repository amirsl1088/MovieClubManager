using MovieClubManager.Entities.Genres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Test.Tools.Genres.Builders
{
    public class GenreBuilder
    {
        private readonly Genre _genre;
        public GenreBuilder()
        {
            
            _genre= new Genre
            {
                Title = "hkgjl"
            };
        }
        public GenreBuilder WithTitle(string titlename)
        {
            _genre.Title = titlename;
            return this;
        }
        public Genre Build()
        {
            return _genre;
        }
    }
}
