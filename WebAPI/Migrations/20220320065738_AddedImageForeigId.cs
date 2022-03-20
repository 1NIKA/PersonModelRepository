using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedImageForeigId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Persons_ImageId",
                table: "Persons",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Images_ImageId",
                table: "Persons",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Images_ImageId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_ImageId",
                table: "Persons");
        }
    }
}
