using Moq;
using MovieClubManager.Contracts.Interfaces;
using MovieClubManager.Persistence.EF;
using MovieClubManager.Persistence.EF.Movies;
using MovieClubManager.Persistence.EF.Rents;
using MovieClubManager.Persistence.EF.Users;
using MovieClubManager.Service.Movies.Contracts;
using MovieClubManager.Service.Rents;
using MovieClubManager.Service.Rents.Contracts;
using MovieClubManager.Service.Users.Contrcts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Test.Tools.Rents.Fctories
{
    public static class RentServiceFactory
    {
        public static RentAppService Create(EFDataContext context,
            DateTime? fakeTime=null,
            RentRepository? rentRepository=null,
            UnitOfWork? unitOfWork=null,
            UserRepository? userRepository=null,
            MovieManagerRepository? movieManagerRepository = null)
        {
            var dateTimeServiceMock = new Mock<DateTimeService>();
            dateTimeServiceMock.Setup(_ => _.Now()).Returns(fakeTime ?? new DateTime(2024, 03, 10));
            return new RentAppService(rentRepository ?? new EFRentRepository(context),
                unitOfWork ?? new EFUnitOfWork(context),
                userRepository ?? new EFUserRepository(context),
                movieManagerRepository ?? new EFMovieManagerRepository(context),
                dateTimeServiceMock.Object);
        }
    }
}
