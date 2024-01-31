using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
                Create the database table name <Activities> which is the name in the <Domain> project folder which also known as the Model.
                Notice the columns were generated. The column name are actually the properties of the <Activities> class.

                Take note of the first column name whic is the "Id" which is of "Guid". This will be recognise as the primary key for this <Activities> table.

                Notice all the "string" column are "nullable" as we have indicated in the <<Nullable>disable</Nullable>> in the <Domain> project
            */
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Category = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    Venue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    /*
                        Noted that the "Id" of Guid is automatically assign to be the Primary Key of this "Activities" table.

                        Take note: The <Activity> class must declared variable name to be "Id" and of "Guid" datatype before the dotnet ef Migrations will assign it to be the Primary Key. If using any random variable name will cause many error while creating the table while assigning the Primary Key
                    */
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*
                Basically this <Down> method only function is to drop this <Activities> table
            */
            migrationBuilder.DropTable(
                name: "Activities");
        }
    }
}
