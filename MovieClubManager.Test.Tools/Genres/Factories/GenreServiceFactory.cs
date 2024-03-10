using MovieClubManager.Persistence.EF;
using MovieClubManager.Persistence.EF.Genres;
using MovieClubManager.Service.Genres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Test.Tools.Genres.Factories
{
    public static class GenreServiceFactory
    {
        public static GenreAppService Create(EFDataContext context)
        {
            return new GenreAppService(new EFGenreManagerRepository(context), new EFUnitOfWork(context));
        }
    }
}
