using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Migrations.Rents
{
    [Migration(202403091916)]
    public class _202403091916_AddedTableRents : Migration
    {

        public override void Up()
        {
            Create.Table("Rents")
                 .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                 .WithColumn("UserId").AsInt32().ForeignKey("FK_Rents_Users", "Users", "Id")
                 .WithColumn("MovieId").AsInt32().ForeignKey("FK_Rents_Movies", "Movies", "Id")
                 .WithColumn("RentedAt").AsDateTime().NotNullable()
                 .WithColumn("GiveBack").AsDateTime().Nullable()
                 .WithColumn("DailyPriceRent").AsDecimal().NotNullable()
                 .WithColumn("DelayPenalty").AsDecimal().NotNullable()
                 .WithColumn("Cost").AsDecimal().Nullable();
        }
        public override void Down()
        {
            Delete.Table("Rents");
        }
    }
}
