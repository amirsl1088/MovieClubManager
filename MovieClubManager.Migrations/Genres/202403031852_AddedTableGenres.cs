using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Migrations.Genres
{
    [Migration(202403031852)]
    public class _202403031852_AddedTableGenres : Migration
    {

        public override void Up()
        {
            Create.Table("Genres")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Title").AsString(50).NotNullable()
                .WithColumn("Rate").AsInt32().WithDefaultValue(0);
        }
        public override void Down()
        {
            Delete.Table("Genres");
        }
    }
}
