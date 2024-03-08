using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieClubManager.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Persistence.EF.Users
{
    public class UserEntityMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id).ValueGeneratedOnAdd();
            builder.Property(_ => _.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(_ => _.LastName).HasMaxLength(50).IsRequired();
            builder.Property(_ => _.Adress).HasMaxLength(200).IsRequired();
            builder.Property(_ => _.MobileNumber).HasMaxLength(20).IsRequired();
            builder.Property(_ => _.Age).IsRequired();
            builder.Property(_ => _.Gender).IsRequired();
            builder.Property(_ => _.CreatedAt).IsRequired();
            builder.Property(_ => _.Rate).HasDefaultValue(0);

        }
    }
}
