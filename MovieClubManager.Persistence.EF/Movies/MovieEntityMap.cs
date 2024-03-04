using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieClubManager.Entities.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Persistence.EF.Movies
{
    public class MovieEntityMap : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasOne(_ => _.Genre)
                .WithMany(_ => _.Movies);
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id).ValueGeneratedOnAdd();
            builder.Property(_ => _.Name).IsRequired();
            builder.Property(_ => _.Describtion);
            builder.Property(_ => _.PublishYear).IsRequired();
            builder.Property(_ => _.DailyPriceRent).IsRequired();
            builder.Property(_ => _.AgeLimit).IsRequired();
            builder.Property(_ => _.DelayPenalty).IsRequired();
            builder.Property(_ => _.Duration).IsRequired();
            builder.Property(_ => _.Director).IsRequired();


        }
    }
}
