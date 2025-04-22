using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MES.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListItems = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isDeleted = table.Column<int>(type: "int", nullable: false),
                    PageNames = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSalesUser = table.Column<bool>(type: "bit", nullable: false),
                    SelectedWorkCenter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Routes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncomingImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomingImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncomingInspections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Received = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Inspected = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RotorsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Initials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Len = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Materials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Others = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RotorsDia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RotorStyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BearingRemoved = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bearing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BearingSeals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CeramicSeals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Right = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    yRight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Left = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    yLeft = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasicSharpening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IfYBasicSharpening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WedgelockAlignmentMarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CenterGrinding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IfYCenterGrinding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aligned = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlasticSleaves = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Welding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeldingNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BedKnife = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoxReceivedWithSaddles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SandBlasting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManualLabor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bottom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Top = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddQty = table.Column<int>(type: "int", nullable: true),
                    TirLeftJournal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TirRightJournal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaddlePartNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RotorCategorization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComponentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Users = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomingInspections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logindetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logindetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginUserDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoginDateAndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginUserDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MESWorkcenters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Workcenters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MESWorkcenters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewRotorData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Workcenters = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewRotorData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Params",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Params", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Receivings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelectedOption = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receivings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RotorDamageGrindingDataFromGrinding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalesOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Received = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Inspected = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RotorsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Initials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Len = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Materials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Others = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RotorsDia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RotorStyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BearingRemoved = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bearing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BearingSeals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CeramicSeals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Right = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    yRight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Left = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    yLeft = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasicSharpening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IfYBasicSharpening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WedgelockAlignmentMarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CenterGrinding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IfYCenterGrinding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aligned = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlasticSleaves = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Welding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeldingNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BedKnife = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoxReceivedWithSaddles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SandBlasting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManualLabor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bottom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Top = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddQty = table.Column<int>(type: "int", nullable: true),
                    TirLeftJournal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TirRightJournal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaddlePartNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RotorCategorization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComponentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Users = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerInstructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerImportance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubmitedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdvancedSharpingStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Workcenters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductionSubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductionSubmitBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RotorsDiaLeft = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RotorsDiaRight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReliefLand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToothFaceLeft = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToothFaceRight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CentersLeft = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CentersRight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisualChecks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InspectedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrindingStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DelayReasonTracking = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMoveoutsideoperation = table.Column<bool>(type: "bit", nullable: false),
                    GrindingdataSubmiteddBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrindingdataSubmitedByDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RotorDamageGrindingDataFromGrinding", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RotorDamageGrindingSaveData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalesOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Received = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Inspected = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RotorsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Initials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Len = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Materials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Others = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RotorsDia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RotorStyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BearingRemoved = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bearing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BearingSeals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CeramicSeals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Right = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    yRight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Left = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    yLeft = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasicSharpening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IfYBasicSharpening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WedgelockAlignmentMarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CenterGrinding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IfYCenterGrinding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aligned = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlasticSleaves = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Welding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeldingNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BedKnife = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoxReceivedWithSaddles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SandBlasting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManualLabor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bottom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Top = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddQty = table.Column<int>(type: "int", nullable: true),
                    TirLeftJournal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TirRightJournal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaddlePartNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RotorCategorization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComponentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Users = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerInstructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerImportance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubmitedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdvancedSharpingStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Workcenters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DamageGrindingSavedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DamageGrindingSavedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RotorDamageGrindingSaveData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RotorDamageGrindingSubmitedData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalesOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Received = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Inspected = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RotorsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Initials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Len = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Materials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Others = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RotorsDia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RotorStyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BearingRemoved = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bearing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BearingSeals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CeramicSeals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Right = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    yRight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Left = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    yLeft = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasicSharpening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IfYBasicSharpening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WedgelockAlignmentMarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CenterGrinding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IfYCenterGrinding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aligned = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlasticSleaves = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Welding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeldingNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BedKnife = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoxReceivedWithSaddles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SandBlasting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManualLabor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bottom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Top = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddQty = table.Column<int>(type: "int", nullable: true),
                    TirLeftJournal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TirRightJournal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaddlePartNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RotorCategorization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComponentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Users = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerInstructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerImportance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubmitedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdvancedSharpingStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Workcenters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DamageGrindingSubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DamageGrindingSubmitBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RotorDamageGrindingSubmitedData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RotorGrindingData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalesOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Received = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Inspected = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RotorsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Initials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Len = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Materials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Others = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RotorsDia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RotorStyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BearingRemoved = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bearing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BearingSeals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CeramicSeals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Right = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    yRight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Left = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    yLeft = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasicSharpening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IfYBasicSharpening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WedgelockAlignmentMarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CenterGrinding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IfYCenterGrinding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aligned = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlasticSleaves = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Welding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeldingNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BedKnife = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoxReceivedWithSaddles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SandBlasting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManualLabor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bottom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Top = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddQty = table.Column<int>(type: "int", nullable: true),
                    TirLeftJournal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TirRightJournal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaddlePartNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RotorCategorization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComponentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Users = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerInstructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerImportance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubmitedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdvancedSharpingStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Workcenters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductionSubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductionSubmitBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RotorsDiaLeft = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RotorsDiaRight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReliefLand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToothFaceLeft = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToothFaceRight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CentersLeft = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CentersRight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisualChecks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InspectedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrindingStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DelayReasonTracking = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMoveoutsideoperation = table.Column<bool>(type: "bit", nullable: false),
                    GrindingdataSubmiteddBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrindingdataSubmitedByDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RotorGrindingData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RotorGrindingSavedData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalesOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Received = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Inspected = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RotorsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Initials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Len = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Materials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Others = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RotorsDia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RotorStyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BearingRemoved = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bearing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BearingSeals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CeramicSeals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Right = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    yRight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Left = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    yLeft = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasicSharpening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IfYBasicSharpening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WedgelockAlignmentMarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CenterGrinding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IfYCenterGrinding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aligned = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlasticSleaves = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Welding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeldingNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BedKnife = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoxReceivedWithSaddles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SandBlasting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManualLabor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bottom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Top = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddQty = table.Column<int>(type: "int", nullable: true),
                    TirLeftJournal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TirRightJournal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaddlePartNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RotorCategorization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComponentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Users = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerInstructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerImportance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubmitedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdvancedSharpingStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Workcenters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductionSubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductionSubmitBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RotorsDiaLeft = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RotorsDiaRight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReliefLand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToothFaceLeft = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToothFaceRight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CentersLeft = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CentersRight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisualChecks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InspectedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrindingStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DelayReasonTracking = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMoveoutsideoperation = table.Column<bool>(type: "bit", nullable: false),
                    GrindingdataSavedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrindingdataSavedByDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RotorGrindingSavedData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RotorProductionData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalesOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Received = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Inspected = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RotorsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Initials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Len = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Materials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Others = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RotorsDia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RotorStyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BearingRemoved = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bearing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BearingSeals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CeramicSeals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Right = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    yRight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Left = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    yLeft = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasicSharpening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IfYBasicSharpening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WedgelockAlignmentMarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CenterGrinding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IfYCenterGrinding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aligned = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlasticSleaves = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Welding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeldingNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BedKnife = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoxReceivedWithSaddles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SandBlasting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManualLabor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bottom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Top = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddQty = table.Column<int>(type: "int", nullable: true),
                    TirLeftJournal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TirRightJournal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaddlePartNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RotorCategorization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComponentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Users = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerInstructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerImportance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubmitedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdvancedSharpingStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Workcenters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductionSubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductionSubmitBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RotorProductionData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RotorProductionSavedData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalesOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Received = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Inspected = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RotorsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Initials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Len = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Materials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Others = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RotorsDia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RotorStyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BearingRemoved = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bearing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BearingSeals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CeramicSeals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Right = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    yRight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Left = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    yLeft = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasicSharpening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IfYBasicSharpening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WedgelockAlignmentMarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CenterGrinding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IfYCenterGrinding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aligned = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlasticSleaves = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Welding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeldingNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BedKnife = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoxReceivedWithSaddles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SandBlasting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManualLabor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bottom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Top = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddQty = table.Column<int>(type: "int", nullable: true),
                    TirLeftJournal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TirRightJournal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaddlePartNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RotorCategorization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComponentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Users = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerInstructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerImportance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubmitedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdvancedSharpingStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Workcenters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductionSavedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductionSavedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RotorProductionSavedData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RotorSalesData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalesOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Received = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Inspected = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RotorsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Initials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Len = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Materials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Others = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RotorsDia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RotorStyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BearingRemoved = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bearing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BearingSeals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CeramicSeals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Right = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    yRight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Left = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    yLeft = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasicSharpening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IfYBasicSharpening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WedgelockAlignmentMarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CenterGrinding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IfYCenterGrinding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aligned = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlasticSleaves = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Welding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeldingNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BedKnife = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoxReceivedWithSaddles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SandBlasting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManualLabor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bottom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Top = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddQty = table.Column<int>(type: "int", nullable: true),
                    TirLeftJournal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TirRightJournal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaddlePartNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RotorCategorization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComponentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Users = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerInstructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerImportance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubmitedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RotorSalesData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RotorSalesSavedData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalesOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Received = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Inspected = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RotorsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Initials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Len = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Materials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Others = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RotorsDia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RotorStyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BearingRemoved = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bearing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BearingSeals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CeramicSeals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Right = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    yRight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Left = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    yLeft = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasicSharpening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IfYBasicSharpening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WedgelockAlignmentMarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CenterGrinding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IfYCenterGrinding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aligned = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlasticSleaves = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Welding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeldingNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BedKnife = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoxReceivedWithSaddles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SandBlasting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManualLabor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bottom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Top = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddQty = table.Column<int>(type: "int", nullable: true),
                    TirLeftJournal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TirRightJournal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaddlePartNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RotorCategorization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComponentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Users = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerInstructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerImportance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SavedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SavedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RotorSalesSavedData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesAttachedFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesAttachedFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<int>(type: "int", nullable: false),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentImage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkCenters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tla = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkCenters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Imagedatas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ImageFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IncomingImageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagedatas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Imagedatas_IncomingImages_IncomingImageId",
                        column: x => x.IncomingImageId,
                        principalTable: "IncomingImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Filedata",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalesAttachedFileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filedata", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Filedata_SalesAttachedFile_SalesAttachedFileId",
                        column: x => x.SalesAttachedFileId,
                        principalTable: "SalesAttachedFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ImageFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipmentImageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Images_ShipmentImage_ShipmentImageId",
                        column: x => x.ShipmentImageId,
                        principalTable: "ShipmentImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "ListItems", "Name", "NormalizedName", "ReadOnly" },
                values: new object[,]
                {
                    { 1, null, "None", "Admin", "ADMIN", false },
                    { 2, null, "None", "User", "USER", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Filedata_SalesAttachedFileId",
                table: "Filedata",
                column: "SalesAttachedFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Imagedatas_IncomingImageId",
                table: "Imagedatas",
                column: "IncomingImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ShipmentImageId",
                table: "Images",
                column: "ShipmentImageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Filedata");

            migrationBuilder.DropTable(
                name: "Imagedatas");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "IncomingInspections");

            migrationBuilder.DropTable(
                name: "Logindetails");

            migrationBuilder.DropTable(
                name: "LoginUserDetails");

            migrationBuilder.DropTable(
                name: "MESWorkcenters");

            migrationBuilder.DropTable(
                name: "NewRotorData");

            migrationBuilder.DropTable(
                name: "Params");

            migrationBuilder.DropTable(
                name: "Receivings");

            migrationBuilder.DropTable(
                name: "RotorDamageGrindingDataFromGrinding");

            migrationBuilder.DropTable(
                name: "RotorDamageGrindingSaveData");

            migrationBuilder.DropTable(
                name: "RotorDamageGrindingSubmitedData");

            migrationBuilder.DropTable(
                name: "RotorGrindingData");

            migrationBuilder.DropTable(
                name: "RotorGrindingSavedData");

            migrationBuilder.DropTable(
                name: "RotorProductionData");

            migrationBuilder.DropTable(
                name: "RotorProductionSavedData");

            migrationBuilder.DropTable(
                name: "RotorSalesData");

            migrationBuilder.DropTable(
                name: "RotorSalesSavedData");

            migrationBuilder.DropTable(
                name: "WorkCenters");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "SalesAttachedFile");

            migrationBuilder.DropTable(
                name: "IncomingImages");

            migrationBuilder.DropTable(
                name: "ShipmentImage");
        }
    }
}
