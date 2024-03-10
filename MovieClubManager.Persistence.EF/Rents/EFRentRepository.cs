using Microsoft.EntityFrameworkCore;
using MovieClubManager.Entities.Rents;
using MovieClubManager.Entities.Users;
using MovieClubManager.Persistence.EF;
using MovieClubManager.Service.Movies.Contracts;
using MovieClubManager.Service.Rents.Contracts;
using MovieClubManager.Service.Rents.Contracts.Dtos;
using MovieClubManager.Service.Users.Contrcts;

namespace MovieClubManager.Persistence.EF.Rents
{
    public class EFRentRepository : RentRepository
    {
        private DbSet<Rent> _rent;
        

        public EFRentRepository(EFDataContext Context)
        {
            _rent = Context.Rents;
        }

        public void Add(Rent rent)
        {
            _rent.Add(rent);
        }

        public async Task<Rent?> FindRentById(int id)
        {
            return await _rent.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<List<GetRentDto>> Get()
        {
            return await _rent.Select(_ => new GetRentDto
            {
                Id = _.Id,
                UserId = _.UserId,
                MovieId = _.MovieId,
                DailyPriceRent = _.DailyPriceRent,
                DelayPenalty = _.DelayPenalty,
                RentedAt = _.RentedAt,
                GiveBack = _.GiveBack,
                MovieRate = _.MovieRate,
                Cost = _.Cost
            }).ToListAsync();
        }

        public bool IsExistMovieForRent(int movieid)
        {
            return _rent.Any(_ => _.MovieId == movieid && _.GiveBack == null);

        }

    }
}
