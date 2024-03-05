using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Migrations.Users
{
    [Migration(202403051957)]
    public class _202403051957_AddedTableUsers : Migration
    {

        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("FirstName").AsString(50).NotNullable()
                .WithColumn("LastName").AsString(50).NotNullable()
                .WithColumn("Adress").AsString(200).NotNullable()
                .WithColumn("MobileNumber").AsString(20).NotNullable()
                .WithColumn("Age").AsInt32().NotNullable()
                .WithColumn("Gender").AsInt16().NotNullable()
                .WithColumn("Rate").AsInt32().Nullable().WithDefaultValue(0);
        }
        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}
