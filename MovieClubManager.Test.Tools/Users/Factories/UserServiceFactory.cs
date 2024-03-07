using Moq;
using MovieClubManager.Contracts.Interfaces;
using MovieClubManager.Persistence.EF;
using MovieClubManager.Persistence.EF.Users;
using MovieClubManager.Service.Users;
using MovieClubManager.Service.Users.Contrcts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Contracts.Interfaces;

namespace MovieClubManager.Test.Tools.Users.Factories
{
    public static class UserServiceFactory
    {
        public static UserAppService Create(
            EFDataContext context
            ,DateTime? faketime=null,
            UserRepository? userRepository = null,
            UnitOfWork? unitOfWork = null)
        {
            var dateTimeServiceMock = new Mock<DateTimeService>();
            dateTimeServiceMock.Setup(_ => _.Now()).Returns(faketime ?? new DateTime(2024, 03, 07));
            return new UserAppService(userRepository ?? new EFUserRepository(context)
                , unitOfWork ?? new EFUnitOfWork(context),dateTimeServiceMock.Object);
        }
    }
}
