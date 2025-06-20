using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "Marks",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CyrillicName = table.Column<string>(type: "text", nullable: false),
                    Popular = table.Column<int>(type: "integer", nullable: false),
                    Country = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CyrillicName = table.Column<string>(type: "text", nullable: false),
                    Class = table.Column<int>(type: "integer", nullable: false),
                    YearFrom = table.Column<int>(type: "integer", nullable: false),
                    YearTo = table.Column<int>(type: "integer", nullable: false),
                    MarkId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Models_Marks_MarkId",
                        column: x => x.MarkId,
                        principalSchema: "public",
                        principalTable: "Marks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Generations",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ModelId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    YearStart = table.Column<int>(type: "integer", nullable: false),
                    YearStop = table.Column<int>(type: "integer", nullable: false),
                    IsRestyle = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Generations_Models_ModelId",
                        column: x => x.ModelId,
                        principalSchema: "public",
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarConfigurations",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    DoorsCount = table.Column<int>(type: "integer", nullable: false),
                    BodyType = table.Column<int>(type: "integer", nullable: false),
                    ConfigurationName = table.Column<string>(type: "text", nullable: true),
                    GenerationId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarConfigurations_Generations_GenerationId",
                        column: x => x.GenerationId,
                        principalSchema: "public",
                        principalTable: "Generations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Modifications",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    OffersPriceFrom = table.Column<float>(type: "real", nullable: true),
                    OffersPriceTo = table.Column<float>(type: "real", nullable: true),
                    GroupName = table.Column<string>(type: "text", nullable: true),
                    CarConfigurationId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modifications_CarConfigurations_CarConfigurationId",
                        column: x => x.CarConfigurationId,
                        principalSchema: "public",
                        principalTable: "CarConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comforts",
                schema: "public",
                columns: table => new
                {
                    ModificationId = table.Column<string>(type: "text", nullable: false),
                    Camera360 = table.Column<bool>(type: "boolean", nullable: false),
                    AshtrayAndCigaretteLighter = table.Column<bool>(type: "boolean", nullable: false),
                    AutoCruise = table.Column<bool>(type: "boolean", nullable: false),
                    AutoMirrors = table.Column<bool>(type: "boolean", nullable: false),
                    AutoPark = table.Column<bool>(type: "boolean", nullable: false),
                    ClimateControl = table.Column<int>(type: "integer", nullable: false),
                    Computer = table.Column<bool>(type: "boolean", nullable: false),
                    Condition = table.Column<bool>(type: "boolean", nullable: false),
                    CoolingBox = table.Column<bool>(type: "boolean", nullable: false),
                    CruiseControl = table.Column<bool>(type: "boolean", nullable: false),
                    DriveModeSystem = table.Column<bool>(type: "boolean", nullable: false),
                    EasyTrunkOpening = table.Column<bool>(type: "boolean", nullable: false),
                    ElectroMirrors = table.Column<bool>(type: "boolean", nullable: false),
                    ElectroTrunk = table.Column<bool>(type: "boolean", nullable: false),
                    ElectroWindowBack = table.Column<bool>(type: "boolean", nullable: false),
                    ElectroWindowFront = table.Column<bool>(type: "boolean", nullable: false),
                    ElectronicGagePanel = table.Column<bool>(type: "boolean", nullable: false),
                    FrontCamera = table.Column<bool>(type: "boolean", nullable: false),
                    KeylessEntry = table.Column<bool>(type: "boolean", nullable: false),
                    MultiFunctionSteeringWheel = table.Column<bool>(type: "boolean", nullable: false),
                    ParkAssistFront = table.Column<bool>(type: "boolean", nullable: false),
                    ParkAssistRear = table.Column<bool>(type: "boolean", nullable: false),
                    PowerLatchingDoors = table.Column<bool>(type: "boolean", nullable: false),
                    ProgrammedBlockHeater = table.Column<bool>(type: "boolean", nullable: false),
                    ProjectionDisplay = table.Column<bool>(type: "boolean", nullable: false),
                    RearCamera = table.Column<bool>(type: "boolean", nullable: false),
                    RemoteEngineStart = table.Column<bool>(type: "boolean", nullable: false),
                    Servo = table.Column<bool>(type: "boolean", nullable: false),
                    StartButton = table.Column<bool>(type: "boolean", nullable: false),
                    StartStopFunction = table.Column<bool>(type: "boolean", nullable: false),
                    SteeringWheelGearShiftPaddles = table.Column<bool>(type: "boolean", nullable: false),
                    WheelHightConfiguration = table.Column<string>(type: "text", nullable: false),
                    WheelDistanceConfiguration = table.Column<string>(type: "text", nullable: false),
                    WheelMemory = table.Column<bool>(type: "boolean", nullable: false),
                    WheelPower = table.Column<bool>(type: "boolean", nullable: false),
                    Socket12V = table.Column<bool>(type: "boolean", nullable: false),
                    Socket220V = table.Column<bool>(type: "boolean", nullable: false),
                    AndroidAuto = table.Column<bool>(type: "boolean", nullable: false),
                    AudioPreparation = table.Column<bool>(type: "boolean", nullable: false),
                    CdAudioSystem = table.Column<bool>(type: "boolean", nullable: false),
                    TVAudioSystem = table.Column<bool>(type: "boolean", nullable: false),
                    PremiumAudio = table.Column<bool>(type: "boolean", nullable: false),
                    Multimedia = table.Column<bool>(type: "boolean", nullable: false),
                    AUX = table.Column<bool>(type: "boolean", nullable: false),
                    AppleCarplay = table.Column<bool>(type: "boolean", nullable: false),
                    Bluetooth = table.Column<bool>(type: "boolean", nullable: false),
                    Navigation = table.Column<bool>(type: "boolean", nullable: false),
                    USB = table.Column<bool>(type: "boolean", nullable: false),
                    VoiceRecognition = table.Column<bool>(type: "boolean", nullable: false),
                    WirelessCharger = table.Column<bool>(type: "boolean", nullable: false),
                    YaAuto = table.Column<bool>(type: "boolean", nullable: false),
                    HeatedSteeringWheel = table.Column<bool>(type: "boolean", nullable: false),
                    DriverSeatSupport = table.Column<bool>(type: "boolean", nullable: false),
                    SeatUpDown = table.Column<bool>(type: "boolean", nullable: false),
                    ElectroRegulatingSeat = table.Column<bool>(type: "boolean", nullable: false),
                    SeatSupport = table.Column<bool>(type: "boolean", nullable: false),
                    SeatsHeat = table.Column<bool>(type: "boolean", nullable: false),
                    SeatsHeatVent = table.Column<bool>(type: "boolean", nullable: false),
                    MassageSeats = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comforts", x => x.ModificationId);
                    table.ForeignKey(
                        name: "FK_Comforts_Modifications_ModificationId",
                        column: x => x.ModificationId,
                        principalSchema: "public",
                        principalTable: "Modifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dimensions",
                schema: "public",
                columns: table => new
                {
                    ModificationId = table.Column<string>(type: "text", nullable: false),
                    Height = table.Column<float>(type: "real", nullable: false),
                    Width = table.Column<float>(type: "real", nullable: false),
                    Length = table.Column<float>(type: "real", nullable: false),
                    WheelBase = table.Column<float>(type: "real", nullable: false),
                    FrontWheelBase = table.Column<float>(type: "real", nullable: true),
                    BackWheelBase = table.Column<float>(type: "real", nullable: true),
                    Clearance = table.Column<string>(type: "text", nullable: true),
                    WheelSize = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimensions", x => x.ModificationId);
                    table.ForeignKey(
                        name: "FK_Dimensions_Modifications_ModificationId",
                        column: x => x.ModificationId,
                        principalSchema: "public",
                        principalTable: "Modifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Emissions",
                schema: "public",
                columns: table => new
                {
                    ModificationId = table.Column<string>(type: "text", nullable: false),
                    FuelEmission = table.Column<int>(type: "integer", nullable: true),
                    EmissionEuroClass = table.Column<string>(type: "text", nullable: true),
                    ElectricRange = table.Column<int>(type: "integer", nullable: true),
                    ChargeTime = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emissions", x => x.ModificationId);
                    table.ForeignKey(
                        name: "FK_Emissions_Modifications_ModificationId",
                        column: x => x.ModificationId,
                        principalSchema: "public",
                        principalTable: "Modifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Engines",
                schema: "public",
                columns: table => new
                {
                    ModificationId = table.Column<string>(type: "text", nullable: false),
                    HorsePower = table.Column<float>(type: "real", nullable: false),
                    KvtPower = table.Column<float>(type: "real", nullable: false),
                    RpmPower = table.Column<string>(type: "text", nullable: true),
                    EngineType = table.Column<int>(type: "integer", nullable: false),
                    EngineFeeding = table.Column<string>(type: "text", nullable: true),
                    EngineOrder = table.Column<string>(type: "text", nullable: true),
                    CylindersOrder = table.Column<string>(type: "text", nullable: true),
                    CylindersValue = table.Column<int>(type: "integer", nullable: true),
                    Compression = table.Column<float>(type: "real", nullable: true),
                    Volume = table.Column<float>(type: "real", nullable: true),
                    VolumeLitres = table.Column<float>(type: "real", nullable: true),
                    PetrolType = table.Column<string>(type: "text", nullable: true),
                    Valves = table.Column<int>(type: "integer", nullable: true),
                    Moment = table.Column<int>(type: "integer", nullable: true),
                    MomentRpm = table.Column<string>(type: "text", nullable: true),
                    GearValue = table.Column<int>(type: "integer", nullable: true),
                    PistonStroke = table.Column<float>(type: "real", nullable: true),
                    Diametr = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engines", x => x.ModificationId);
                    table.ForeignKey(
                        name: "FK_Engines_Modifications_ModificationId",
                        column: x => x.ModificationId,
                        principalSchema: "public",
                        principalTable: "Modifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exteriors",
                schema: "public",
                columns: table => new
                {
                    ModificationId = table.Column<string>(type: "text", nullable: false),
                    AdaptiveLight = table.Column<bool>(type: "boolean", nullable: false),
                    AutomaticLightingControl = table.Column<bool>(type: "boolean", nullable: false),
                    DaytimeRunningLights = table.Column<bool>(type: "boolean", nullable: false),
                    HeatedWashSystem = table.Column<bool>(type: "boolean", nullable: false),
                    HighBeamAssist = table.Column<bool>(type: "boolean", nullable: false),
                    LaserLights = table.Column<bool>(type: "boolean", nullable: false),
                    LEDLights = table.Column<bool>(type: "boolean", nullable: false),
                    LightCleaner = table.Column<bool>(type: "boolean", nullable: false),
                    LightSensor = table.Column<bool>(type: "boolean", nullable: false),
                    HeatedMirrors = table.Column<bool>(type: "boolean", nullable: false),
                    FrontFogLights = table.Column<bool>(type: "boolean", nullable: false),
                    RainSensor = table.Column<bool>(type: "boolean", nullable: false),
                    HeatedWindshieldCleaner = table.Column<bool>(type: "boolean", nullable: false),
                    HeatedWindscreen = table.Column<bool>(type: "boolean", nullable: false),
                    XenonLights = table.Column<bool>(type: "boolean", nullable: false),
                    DoubleColoredBody = table.Column<bool>(type: "boolean", nullable: false),
                    MetallicColored = table.Column<bool>(type: "boolean", nullable: false),
                    RoofRails = table.Column<bool>(type: "boolean", nullable: false),
                    SteelWheels = table.Column<bool>(type: "boolean", nullable: false),
                    DoorSillPanel = table.Column<bool>(type: "boolean", nullable: false),
                    BodyKit = table.Column<bool>(type: "boolean", nullable: false),
                    Mouldings = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exteriors", x => x.ModificationId);
                    table.ForeignKey(
                        name: "FK_Exteriors_Modifications_ModificationId",
                        column: x => x.ModificationId,
                        principalSchema: "public",
                        principalTable: "Modifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interiors",
                schema: "public",
                columns: table => new
                {
                    ModificationId = table.Column<string>(type: "text", nullable: false),
                    Seats = table.Column<string>(type: "text", nullable: true),
                    TrunksMinCapacity = table.Column<int>(type: "integer", nullable: true),
                    TrunksMaxCapacity = table.Column<int>(type: "integer", nullable: true),
                    InteriorMaterial = table.Column<int>(type: "integer", nullable: false),
                    BlackRoof = table.Column<bool>(type: "boolean", nullable: false),
                    DecorativeInteriorLighting = table.Column<bool>(type: "boolean", nullable: false),
                    FoldingFrontPassengerSeat = table.Column<bool>(type: "boolean", nullable: false),
                    FoldingTablesRear = table.Column<bool>(type: "boolean", nullable: false),
                    FrontCentreArmrest = table.Column<bool>(type: "boolean", nullable: false),
                    Hatch = table.Column<bool>(type: "boolean", nullable: false),
                    LeatherGearStick = table.Column<bool>(type: "boolean", nullable: false),
                    PanoramaRoof = table.Column<bool>(type: "boolean", nullable: false),
                    RollerBlindForRearWindow = table.Column<bool>(type: "boolean", nullable: false),
                    RollerBlindsForRearSideWindows = table.Column<bool>(type: "boolean", nullable: false),
                    SeatMemory = table.Column<bool>(type: "boolean", nullable: false),
                    SeatTransformation = table.Column<bool>(type: "boolean", nullable: false),
                    SportPedals = table.Column<bool>(type: "boolean", nullable: false),
                    SportSeats = table.Column<bool>(type: "boolean", nullable: false),
                    ThirdRearHeadrest = table.Column<bool>(type: "boolean", nullable: false),
                    ThirdRowSeats = table.Column<bool>(type: "boolean", nullable: false),
                    TintedGlass = table.Column<bool>(type: "boolean", nullable: false),
                    LeatherSteeringWheel = table.Column<bool>(type: "boolean", nullable: false),
                    AdjustablePedals = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interiors", x => x.ModificationId);
                    table.ForeignKey(
                        name: "FK_Interiors_Modifications_ModificationId",
                        column: x => x.ModificationId,
                        principalSchema: "public",
                        principalTable: "Modifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mobilities",
                schema: "public",
                columns: table => new
                {
                    ModificationId = table.Column<string>(type: "text", nullable: false),
                    FrontBrake = table.Column<string>(type: "text", nullable: true),
                    BackBrake = table.Column<string>(type: "text", nullable: true),
                    FrontSuspension = table.Column<string>(type: "text", nullable: true),
                    BackSuspension = table.Column<string>(type: "text", nullable: true),
                    Transmission = table.Column<int>(type: "integer", nullable: true),
                    Drive = table.Column<int>(type: "integer", nullable: false),
                    ActiveSuspension = table.Column<bool>(type: "boolean", nullable: false),
                    AirSuspension = table.Column<bool>(type: "boolean", nullable: false),
                    ReducedSpareWheel = table.Column<bool>(type: "boolean", nullable: false),
                    SpareWheel = table.Column<bool>(type: "boolean", nullable: false),
                    SportSuspension = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mobilities", x => x.ModificationId);
                    table.ForeignKey(
                        name: "FK_Mobilities_Modifications_ModificationId",
                        column: x => x.ModificationId,
                        principalSchema: "public",
                        principalTable: "Modifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Performances",
                schema: "public",
                columns: table => new
                {
                    ModificationId = table.Column<string>(type: "text", nullable: false),
                    TimeTo100 = table.Column<float>(type: "real", nullable: true),
                    MaxSpeed = table.Column<float>(type: "real", nullable: true),
                    ConsumptionMixed = table.Column<decimal>(type: "numeric", nullable: true),
                    ConsumptionHiway = table.Column<decimal>(type: "numeric", nullable: true),
                    ConsumptionCity = table.Column<decimal>(type: "numeric", nullable: true),
                    RangeDistance = table.Column<decimal>(type: "numeric", nullable: true),
                    ElectricRange = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performances", x => x.ModificationId);
                    table.ForeignKey(
                        name: "FK_Performances_Modifications_ModificationId",
                        column: x => x.ModificationId,
                        principalSchema: "public",
                        principalTable: "Modifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Safeties",
                schema: "public",
                columns: table => new
                {
                    ModificationId = table.Column<string>(type: "text", nullable: false),
                    SafetyRating = table.Column<string>(type: "text", nullable: true),
                    SafetyGrade = table.Column<int>(type: "integer", nullable: true),
                    ABS = table.Column<bool>(type: "boolean", nullable: false),
                    CurtainAirbags = table.Column<bool>(type: "boolean", nullable: false),
                    DriverAirbag = table.Column<bool>(type: "boolean", nullable: false),
                    PassengerAirbag = table.Column<bool>(type: "boolean", nullable: false),
                    RearSideAirbag = table.Column<bool>(type: "boolean", nullable: false),
                    SideAirbag = table.Column<bool>(type: "boolean", nullable: false),
                    ASR = table.Column<bool>(type: "boolean", nullable: false),
                    BAS = table.Column<bool>(type: "boolean", nullable: false),
                    BlindSpotMonitor = table.Column<bool>(type: "boolean", nullable: false),
                    CollisionPreventionAssist = table.Column<bool>(type: "boolean", nullable: false),
                    DownhillAssist = table.Column<bool>(type: "boolean", nullable: false),
                    DrowsyDriverAlertSystem = table.Column<bool>(type: "boolean", nullable: false),
                    ESP = table.Column<bool>(type: "boolean", nullable: false),
                    FeedbackAlarm = table.Column<bool>(type: "boolean", nullable: false),
                    GLONASS = table.Column<bool>(type: "boolean", nullable: false),
                    HillControl = table.Column<bool>(type: "boolean", nullable: false),
                    ISOFIX = table.Column<bool>(type: "boolean", nullable: false),
                    FrontISOFIX = table.Column<bool>(type: "boolean", nullable: false),
                    KneeAirbag = table.Column<bool>(type: "boolean", nullable: false),
                    LaminatedSafetyGlass = table.Column<bool>(type: "boolean", nullable: false),
                    LaneKeepingAssist = table.Column<bool>(type: "boolean", nullable: false),
                    NightVision = table.Column<bool>(type: "boolean", nullable: false),
                    RearDoorPowerChildLocks = table.Column<bool>(type: "boolean", nullable: false),
                    TrafficSignRecognition = table.Column<bool>(type: "boolean", nullable: false),
                    TyrePressureMonitoring = table.Column<bool>(type: "boolean", nullable: false),
                    VSM = table.Column<bool>(type: "boolean", nullable: false),
                    Alarm = table.Column<bool>(type: "boolean", nullable: false),
                    Immobiliser = table.Column<bool>(type: "boolean", nullable: false),
                    Lock = table.Column<bool>(type: "boolean", nullable: false),
                    VolumeSensor = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Safeties", x => x.ModificationId);
                    table.ForeignKey(
                        name: "FK_Safeties_Modifications_ModificationId",
                        column: x => x.ModificationId,
                        principalSchema: "public",
                        principalTable: "Modifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Weights",
                schema: "public",
                columns: table => new
                {
                    ModificationId = table.Column<string>(type: "text", nullable: false),
                    BaseWeight = table.Column<int>(type: "integer", nullable: true),
                    FullWeight = table.Column<int>(type: "integer", nullable: true),
                    FuelTankCapacity = table.Column<int>(type: "integer", nullable: true),
                    BatteryCapacity = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weights", x => x.ModificationId);
                    table.ForeignKey(
                        name: "FK_Weights_Modifications_ModificationId",
                        column: x => x.ModificationId,
                        principalSchema: "public",
                        principalTable: "Modifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarConfigurations_GenerationId",
                schema: "public",
                table: "CarConfigurations",
                column: "GenerationId");

            migrationBuilder.CreateIndex(
                name: "IX_Generations_ModelId",
                schema: "public",
                table: "Generations",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_MarkId",
                schema: "public",
                table: "Models",
                column: "MarkId");

            migrationBuilder.CreateIndex(
                name: "IX_Modifications_CarConfigurationId",
                schema: "public",
                table: "Modifications",
                column: "CarConfigurationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comforts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Dimensions",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Emissions",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Engines",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Exteriors",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Interiors",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Mobilities",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Performances",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Safeties",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Weights",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Modifications",
                schema: "public");

            migrationBuilder.DropTable(
                name: "CarConfigurations",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Generations",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Models",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Marks",
                schema: "public");
        }
    }
}
