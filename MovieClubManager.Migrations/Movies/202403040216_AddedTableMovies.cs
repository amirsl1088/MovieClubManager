using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Migrations.Movies
{
    [Migration(202403040216)]
    public class _202403040216_AddedTableMovies : Migration
    {

        public override void Up()
        {
            Create.Table("Movies")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(50).NotNullable()
                .WithColumn("Describtion").AsString(250).Nullable()
                .WithColumn("PublishYear").AsString(30).NotNullable()
                .WithColumn("DailyPriceRent").AsInt32().NotNullable()
                .WithColumn("AgeLimit").AsInt32().NotNullable()
                .WithColumn("DelayPenalty").AsInt32().NotNullable()
                .WithColumn("Duration").AsDouble().NotNullable()
                .WithColumn("Count").AsInt32().WithDefaultValue(1)
                .WithColumn("Director").AsString(50).NotNullable()
                .WithColumn("GenreId").AsInt32().ForeignKey("FK_Movies_Genres", "Genres", "Id");
        }
        public override void Down()
        {
            Delete.Table("Movies");
        }
    }
}
