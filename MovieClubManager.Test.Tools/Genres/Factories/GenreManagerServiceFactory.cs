using MovieClubManager.Persistence.EF;
using MovieClubManager.Persistence.EF.Genres;
using MovieClubManager.Service.Genres;
using MovieClubManager.Service.Genres.Contracts;

namespace MovieClubManager.Test.Tools.Genres.Factories
{
    public static class GenreManagerServiceFactory
    {
        public static GenreManagerService Create(EFDataContext context)
        {
            return new GenreManagerAppService
                (new EFGenreManagerRepository(context)
                , new EFUnitOfWork(context));
        }
    }
}
