using MovieClubManager.Persistence.EF;
using MovieClubManager.Persistence.EF.Genres;
using MovieClubManager.Persistence.EF.Movies;
using MovieClubManager.Service.Movies;
using MovieClubManager.Service.Movies.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Test.Tools.Movies.Factories
{
    public static class MovieManagerSerciceFactory
    {
        public static MovieManagerService Create(EFDataContext context)
        {
            return new MovieManagerAppService(new EFMovieManagerRepository(context)
                , new EFUnitOfWork(context), new EFGenreManagerRepository(context));
        }
    }
}
