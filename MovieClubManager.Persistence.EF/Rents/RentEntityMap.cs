using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieClubManager.Entities.Rents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Persistence.EF.Rents
{
    public class RentEntityMap : IEntityTypeConfiguration<Rent>
    {
        public void Configure(EntityTypeBuilder<Rent> builder)
        {
            builder.HasOne(_ => _.User)
                .WithMany(_ => _.Rents);
            builder.HasOne(_ => _.Movie)
                .WithMany(_ => _.Rents);
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id).ValueGeneratedOnAdd();
            builder.Property(_ => _.UserId).IsRequired();
            builder.Property(_ => _.MovieId).IsRequired();
            builder.Property(_ => _.RentedAt).IsRequired();
            builder.Property(_ => _.GiveBack);
            builder.Property(_ => _.DailyPriceRent).IsRequired();
            builder.Property(_ => _.DelayPenalty).IsRequired();
            builder.Property(_ => _.Cost);
        }
    }
}
