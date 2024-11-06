using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class customfunctions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Path to your SQL file
            var sqlFilePath = Path.Combine("../Persistence/SqlScripts", "custom_functions.sql");
            var sqlCommands = File.ReadAllText(sqlFilePath);

            // Execute the SQL commands from the file
            migrationBuilder.Sql(sqlCommands);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
