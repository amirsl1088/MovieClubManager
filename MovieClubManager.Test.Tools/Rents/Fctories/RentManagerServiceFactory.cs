using MovieClubManager.Persistence.EF;
using MovieClubManager.Persistence.EF.Rents;
using MovieClubManager.Service.Rents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Test.Tools.Rents.Fctories
{
    public static class RentManagerServiceFactory
    {
        public static RentManagerAppService Create(EFDataContext context)
        {
            return new RentManagerAppService(new EFRentRepository(context), new EFUnitOfWork(context));
        }
    }
}
