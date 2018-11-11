using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SensorAPIWeb.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SerialNumber = table.Column<string>(maxLength: 24, nullable: false),
                    BatteryVoltage = table.Column<string>(nullable: true),
                    FirmwareVersion = table.Column<string>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceDetails",
                columns: table => new
                {
                    DeviceDetailsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SerialNumber = table.Column<string>(nullable: true),
                    Humidity = table.Column<string>(nullable: true),
                    Temperature = table.Column<string>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    DeviceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceDetails", x => x.DeviceDetailsId);
                    table.ForeignKey(
                        name: "FK_DeviceDetails_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeId = table.Column<int>(nullable: false),
                    TokenNumber = table.Column<string>(maxLength: 64, nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tokens_Devices_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceDetails_DeviceId",
                table: "DeviceDetails",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_TypeId",
                table: "Tokens",
                column: "TypeId",
                unique: true);
            var sp = @"IF (OBJECT_ID('InsertOrUpdateToken') IS NOT NULL)
                            DROP PROCEDURE InsertOrUpdateToken
                        GO
                        CREATE PROCEDURE InsertOrUpdateToken
                        (
                            @SerialNumber VARCHAR(24),
                            @Token VARCHAR(64)	
                        )
                        AS
                        BEGIN
                        Declare @TypeId int
                        SET NOCOUNT ON;
                        SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;   
                            BEGIN TRY
		                        BEGIN TRANSACTION
			                        select @TypeId=Id from Devices where SerialNumber = @SerialNumber 
			                        IF @@ROWCOUNT=0 
				                        BEGIN
					                        INSERT INTO Devices(SerialNumber,UpdateTime) values(@SerialNumber, CURRENT_TIMESTAMP)
					                        select @TypeId=Id from Devices where SerialNumber = @SerialNumber 
					                        INSERT INTO Tokens(TypeId,TokenNumber,UpdateTime) values(@TypeId, @Token, CURRENT_TIMESTAMP)
				                        END
			                        ELSE
			                        BEGIN
				                        select * from Tokens where TypeId=@TypeId 			
				                        IF @@ROWCOUNT=0 
					                        BEGIN
						                        INSERT INTO Tokens(TypeId,TokenNumber,UpdateTime) values(@TypeId, @Token, CURRENT_TIMESTAMP)
					                        END
				                        ELSE  
					                        BEGIN
						                        UPDATE Tokens set TokenNumber=@Token where TypeId=@TypeId
					                        END
			                        END
		                        COMMIT TRANSACTION
                            END TRY
                            BEGIN CATCH
		                        IF @@TRANCOUNT > 0 
		                        BEGIN
			                        ROLLBACK TRANSACTION
		                        END
                            END CATCH;
                        END
                        GO
                        ";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceDetails");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
