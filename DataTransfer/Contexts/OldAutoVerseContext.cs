using DataTransfer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataTransfer.Contexts;

public partial class OldAutoVerseContext : DbContext
{
    public OldAutoVerseContext()
    {
    }

    public OldAutoVerseContext(DbContextOptions<OldAutoVerseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Configuration> Configurations { get; set; }

    public virtual DbSet<Generation> Generations { get; set; }

    public virtual DbSet<Mark> Marks { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<Modification> Modifications { get; set; }

    public virtual DbSet<Option> Options { get; set; }

    public virtual DbSet<Specification> Specifications { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("Server=mysql;Port=3306;Database=appdb;User=appuser;Password=StrongMyPassword!23;Pooling=true;Connection Lifetime=0;", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Configuration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("configuration")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.GenerationId, "generation_id");

            entity.HasIndex(e => e.Id, "id").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.BodyType)
                .HasMaxLength(50)
                .HasColumnName("body-type");
            entity.Property(e => e.ConfigurationName)
                .HasMaxLength(50)
                .HasColumnName("configuration-name");
            entity.Property(e => e.DoorsCount).HasColumnName("doors-count");
            entity.Property(e => e.GenerationId)
                .HasMaxLength(50)
                .HasColumnName("generation_id");

            entity.HasOne(d => d.Generation).WithMany(p => p.Configurations)
                .HasForeignKey(d => d.GenerationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_configuration_generation");
        });

        modelBuilder.Entity<Generation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("generation")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.Id, "id").IsUnique();

            entity.HasIndex(e => e.ModelId, "model_id");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.IsRestyle).HasColumnName("is-restyle");
            entity.Property(e => e.ModelId)
                .HasMaxLength(50)
                .HasColumnName("model_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.YearStart).HasColumnName("year-start");
            entity.Property(e => e.YearStop).HasColumnName("year-stop");

            entity.HasOne(d => d.Model).WithMany(p => p.Generations)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_generation_model");
        });

        modelBuilder.Entity<Mark>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("mark")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.Id, "id").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
            entity.Property(e => e.CyrillicName)
                .HasMaxLength(50)
                .HasColumnName("cyrillic-name");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Popular).HasColumnName("popular");
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("model")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.Id, "id").IsUnique();

            entity.HasIndex(e => e.MarkId, "mark_id");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Class)
                .HasMaxLength(5)
                .HasColumnName("class");
            entity.Property(e => e.CyrillicName)
                .HasMaxLength(50)
                .HasColumnName("cyrillic-name");
            entity.Property(e => e.MarkId)
                .HasMaxLength(50)
                .HasColumnName("mark_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.YearFrom).HasColumnName("year-from");
            entity.Property(e => e.YearTo).HasColumnName("year-to");

            entity.HasOne(d => d.Mark).WithMany(p => p.Models)
                .HasForeignKey(d => d.MarkId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_model_mark");
        });

        modelBuilder.Entity<Modification>(entity =>
        {
            entity.HasKey(e => e.ComplectationId).HasName("PRIMARY");

            entity
                .ToTable("modification")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.ComplectationId, "complectation-id").IsUnique();

            entity.HasIndex(e => e.ConfigurationId, "configuration_id");

            entity.Property(e => e.ComplectationId)
                .HasMaxLength(50)
                .HasColumnName("complectation-id");
            entity.Property(e => e.ConfigurationId)
                .HasMaxLength(50)
                .HasColumnName("configuration_id");
            entity.Property(e => e.GroupName)
                .HasMaxLength(100)
                .HasColumnName("group-name");
            entity.Property(e => e.OffersPriceFrom).HasColumnName("offers-price-from");
            entity.Property(e => e.OffersPriceTo).HasColumnName("offers-price-to");

            entity.HasOne(d => d.Configuration).WithMany(p => p.Modifications)
                .HasForeignKey(d => d.ConfigurationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_modification_configuration");
        });

        modelBuilder.Entity<Option>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("options")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.ComplectationId, "complectation_id").IsUnique();

            entity.Property(e => e.Abs)
                .HasMaxLength(100)
                .HasColumnName("abs");
            entity.Property(e => e.ActivSuspension)
                .HasMaxLength(100)
                .HasColumnName("activ-suspension");
            entity.Property(e => e.AdaptiveLight)
                .HasMaxLength(100)
                .HasColumnName("adaptive-light");
            entity.Property(e => e.AdjPedals)
                .HasMaxLength(100)
                .HasColumnName("adj-pedals");
            entity.Property(e => e.AirSuspension)
                .HasMaxLength(100)
                .HasColumnName("air-suspension");
            entity.Property(e => e.AirbagCurtain)
                .HasMaxLength(100)
                .HasColumnName("airbag-curtain");
            entity.Property(e => e.AirbagDriver)
                .HasMaxLength(100)
                .HasColumnName("airbag-driver");
            entity.Property(e => e.AirbagPassenger)
                .HasMaxLength(100)
                .HasColumnName("airbag-passenger");
            entity.Property(e => e.AirbagRearSide)
                .HasMaxLength(100)
                .HasColumnName("airbag-rear-side");
            entity.Property(e => e.AirbagSide)
                .HasMaxLength(100)
                .HasColumnName("airbag-side");
            entity.Property(e => e.Alarm)
                .HasMaxLength(100)
                .HasColumnName("alarm");
            entity.Property(e => e.Alcantara)
                .HasMaxLength(100)
                .HasColumnName("alcantara");
            entity.Property(e => e.AndroidAuto)
                .HasMaxLength(100)
                .HasColumnName("android-auto");
            entity.Property(e => e.AppleCarplay)
                .HasMaxLength(100)
                .HasColumnName("apple-carplay");
            entity.Property(e => e.AshtrayAndCigaretteLighter)
                .HasMaxLength(100)
                .HasColumnName("ashtray-and-cigarette-lighter");
            entity.Property(e => e.Asr)
                .HasMaxLength(100)
                .HasColumnName("asr");
            entity.Property(e => e.Audiopreparation)
                .HasMaxLength(100)
                .HasColumnName("audiopreparation");
            entity.Property(e => e.AudiosystemCd)
                .HasMaxLength(100)
                .HasColumnName("audiosystem-cd");
            entity.Property(e => e.AudiosystemTv)
                .HasMaxLength(100)
                .HasColumnName("audiosystem-tv");
            entity.Property(e => e.AutoCruise)
                .HasMaxLength(100)
                .HasColumnName("auto-cruise");
            entity.Property(e => e.AutoMirrors)
                .HasMaxLength(100)
                .HasColumnName("auto-mirrors");
            entity.Property(e => e.AutoPark)
                .HasMaxLength(100)
                .HasColumnName("auto-park");
            entity.Property(e => e.AutomaticLightingControl)
                .HasMaxLength(100)
                .HasColumnName("automatic-lighting-control");
            entity.Property(e => e.Aux)
                .HasMaxLength(100)
                .HasColumnName("aux");
            entity.Property(e => e.Bas)
                .HasMaxLength(100)
                .HasColumnName("bas");
            entity.Property(e => e.BlackRoof)
                .HasMaxLength(100)
                .HasColumnName("black-roof");
            entity.Property(e => e.BlindSpot)
                .HasMaxLength(100)
                .HasColumnName("blind-spot");
            entity.Property(e => e.Bluetooth)
                .HasMaxLength(100)
                .HasColumnName("bluetooth");
            entity.Property(e => e.BodyKit)
                .HasMaxLength(100)
                .HasColumnName("body-kit");
            entity.Property(e => e.BodyMouldings)
                .HasMaxLength(100)
                .HasColumnName("body-mouldings");
            entity.Property(e => e.ClimateControl1)
                .HasMaxLength(100)
                .HasColumnName("climate-control-1");
            entity.Property(e => e.ClimateControl2)
                .HasMaxLength(100)
                .HasColumnName("climate-control-2");
            entity.Property(e => e.CollisionPreventionAssist)
                .HasMaxLength(100)
                .HasColumnName("collision-prevention-assist");
            entity.Property(e => e.ComboInterior)
                .HasMaxLength(100)
                .HasColumnName("combo-interior");
            entity.Property(e => e.ComplectationId)
                .HasMaxLength(50)
                .HasColumnName("complectation_id");
            entity.Property(e => e.Computer)
                .HasMaxLength(100)
                .HasColumnName("computer");
            entity.Property(e => e.Condition)
                .HasMaxLength(100)
                .HasColumnName("condition");
            entity.Property(e => e.CoolingBox)
                .HasMaxLength(100)
                .HasColumnName("cooling-box");
            entity.Property(e => e.CruiseControl)
                .HasMaxLength(100)
                .HasColumnName("cruise-control");
            entity.Property(e => e.DecorativeInteriorLighting)
                .HasMaxLength(100)
                .HasColumnName("decorative-interior-lighting");
            entity.Property(e => e.Dha)
                .HasMaxLength(100)
                .HasColumnName("dha");
            entity.Property(e => e.DoorSillPanel)
                .HasMaxLength(100)
                .HasColumnName("door-sill-panel");
            entity.Property(e => e.DriveModeSys)
                .HasMaxLength(100)
                .HasColumnName("drive-mode-sys");
            entity.Property(e => e.DriverSeatElectric)
                .HasMaxLength(100)
                .HasColumnName("driver-seat-electric");
            entity.Property(e => e.DriverSeatMemory)
                .HasMaxLength(100)
                .HasColumnName("driver-seat-memory");
            entity.Property(e => e.DriverSeatSupport)
                .HasMaxLength(100)
                .HasColumnName("driver-seat-support");
            entity.Property(e => e.DriverSeatUpdown)
                .HasMaxLength(100)
                .HasColumnName("driver-seat-updown");
            entity.Property(e => e.Drl)
                .HasMaxLength(100)
                .HasColumnName("drl");
            entity.Property(e => e.DrowsyDriverAlertSystem)
                .HasMaxLength(100)
                .HasColumnName("drowsy-driver-alert-system");
            entity.Property(e => e.DuoBodyColor)
                .HasMaxLength(100)
                .HasColumnName("duo-body-color");
            entity.Property(e => e.EAdjustmentWheel)
                .HasMaxLength(100)
                .HasColumnName("e-adjustment-wheel");
            entity.Property(e => e.EasyTrunkOpening)
                .HasMaxLength(100)
                .HasColumnName("easy-trunk-opening");
            entity.Property(e => e.EcoLeather)
                .HasMaxLength(100)
                .HasColumnName("eco-leather");
            entity.Property(e => e.ElectroMirrors)
                .HasMaxLength(100)
                .HasColumnName("electro-mirrors");
            entity.Property(e => e.ElectroRearSeat)
                .HasMaxLength(100)
                .HasColumnName("electro-rear-seat");
            entity.Property(e => e.ElectroTrunk)
                .HasMaxLength(100)
                .HasColumnName("electro-trunk");
            entity.Property(e => e.ElectroWindowBack)
                .HasMaxLength(100)
                .HasColumnName("electro-window-back");
            entity.Property(e => e.ElectroWindowFront)
                .HasMaxLength(100)
                .HasColumnName("electro-window-front");
            entity.Property(e => e.ElectronicGagePanel)
                .HasMaxLength(100)
                .HasColumnName("electronic-gage-panel");
            entity.Property(e => e.EntertainmentSystemForRearSeatPassengers)
                .HasMaxLength(100)
                .HasColumnName("entertainment-system-for-rear-seat-passengers");
            entity.Property(e => e.Esp)
                .HasMaxLength(100)
                .HasColumnName("esp");
            entity.Property(e => e.FabricSeats)
                .HasMaxLength(100)
                .HasColumnName("fabric-seats");
            entity.Property(e => e.FeedbackAlarm)
                .HasMaxLength(100)
                .HasColumnName("feedback-alarm");
            entity.Property(e => e.FoldingFrontPassengerSeat)
                .HasMaxLength(100)
                .HasColumnName("folding-front-passenger-seat");
            entity.Property(e => e.FoldingTablesRear)
                .HasMaxLength(100)
                .HasColumnName("folding-tables-rear");
            entity.Property(e => e.FrontCamera)
                .HasMaxLength(100)
                .HasColumnName("front-camera");
            entity.Property(e => e.FrontCentreArmrest)
                .HasMaxLength(100)
                .HasColumnName("front-centre-armrest");
            entity.Property(e => e.FrontSeatSupport)
                .HasMaxLength(100)
                .HasColumnName("front-seat-support");
            entity.Property(e => e.FrontSeatsHeat)
                .HasMaxLength(100)
                .HasColumnName("front-seats-heat");
            entity.Property(e => e.FrontSeatsHeatVent)
                .HasMaxLength(100)
                .HasColumnName("front-seats-heat-vent");
            entity.Property(e => e.Glonass)
                .HasMaxLength(100)
                .HasColumnName("glonass");
            entity.Property(e => e.Hatch)
                .HasMaxLength(100)
                .HasColumnName("hatch");
            entity.Property(e => e.Hcc)
                .HasMaxLength(100)
                .HasColumnName("hcc");
            entity.Property(e => e.HeatedWashSystem)
                .HasMaxLength(100)
                .HasColumnName("heated-wash-system");
            entity.Property(e => e.HighBeamAssist)
                .HasMaxLength(100)
                .HasColumnName("high-beam-assist");
            entity.Property(e => e.Immo)
                .HasMaxLength(100)
                .HasColumnName("immo");
            entity.Property(e => e.Isofix)
                .HasMaxLength(100)
                .HasColumnName("isofix");
            entity.Property(e => e.IsofixFront)
                .HasMaxLength(100)
                .HasColumnName("isofix-front");
            entity.Property(e => e.KeylessEntry)
                .HasMaxLength(100)
                .HasColumnName("keyless-entry");
            entity.Property(e => e.KneeAirbag)
                .HasMaxLength(100)
                .HasColumnName("knee-airbag");
            entity.Property(e => e.LaminatedSafetyGlass)
                .HasMaxLength(100)
                .HasColumnName("laminated-safety-glass");
            entity.Property(e => e.LaneKeepingAssist)
                .HasMaxLength(100)
                .HasColumnName("lane-keeping-assist");
            entity.Property(e => e.LaserLights)
                .HasMaxLength(100)
                .HasColumnName("laser-lights");
            entity.Property(e => e.Leather)
                .HasMaxLength(100)
                .HasColumnName("leather");
            entity.Property(e => e.LeatherGearStick)
                .HasMaxLength(100)
                .HasColumnName("leather-gear-stick");
            entity.Property(e => e.LedLights)
                .HasMaxLength(100)
                .HasColumnName("led-lights");
            entity.Property(e => e.LightCleaner)
                .HasMaxLength(100)
                .HasColumnName("light-cleaner");
            entity.Property(e => e.LightSensor)
                .HasMaxLength(100)
                .HasColumnName("light-sensor");
            entity.Property(e => e.Lock)
                .HasMaxLength(100)
                .HasColumnName("lock");
            entity.Property(e => e.MassageSeats)
                .HasMaxLength(100)
                .HasColumnName("massage-seats");
            entity.Property(e => e.MirrorsHeat)
                .HasMaxLength(100)
                .HasColumnName("mirrors-heat");
            entity.Property(e => e.MultiWheel)
                .HasMaxLength(100)
                .HasColumnName("multi-wheel");
            entity.Property(e => e.MultizoneClimateControl)
                .HasMaxLength(100)
                .HasColumnName("multizone-climate-control");
            entity.Property(e => e.MusicSuper)
                .HasMaxLength(100)
                .HasColumnName("music-super");
            entity.Property(e => e.Navigation)
                .HasMaxLength(100)
                .HasColumnName("navigation");
            entity.Property(e => e.NightVision)
                .HasMaxLength(100)
                .HasColumnName("night-vision");
            entity.Property(e => e.PaintMetallic)
                .HasMaxLength(100)
                .HasColumnName("paint-metallic");
            entity.Property(e => e.PanoramaRoof)
                .HasMaxLength(100)
                .HasColumnName("panorama-roof");
            entity.Property(e => e.ParkAssistF)
                .HasMaxLength(100)
                .HasColumnName("park-assist-f");
            entity.Property(e => e.ParkAssistR)
                .HasMaxLength(100)
                .HasColumnName("park-assist-r");
            entity.Property(e => e.PassengerSeatElectric)
                .HasMaxLength(100)
                .HasColumnName("passenger-seat-electric");
            entity.Property(e => e.PassengerSeatUpdown)
                .HasMaxLength(100)
                .HasColumnName("passenger-seat-updown");
            entity.Property(e => e.PowerChildLocksRearDoors)
                .HasMaxLength(100)
                .HasColumnName("power-child-locks-rear-doors");
            entity.Property(e => e.PowerLatchingDoors)
                .HasMaxLength(100)
                .HasColumnName("power-latching-doors");
            entity.Property(e => e.ProgrammedBlockHeater)
                .HasMaxLength(100)
                .HasColumnName("programmed-block-heater");
            entity.Property(e => e.ProjectionDisplay)
                .HasMaxLength(100)
                .HasColumnName("projection-display");
            entity.Property(e => e.Ptf)
                .HasMaxLength(100)
                .HasColumnName("ptf");
            entity.Property(e => e.RainSensor)
                .HasMaxLength(100)
                .HasColumnName("rain-sensor");
            entity.Property(e => e.RearCamera)
                .HasMaxLength(100)
                .HasColumnName("rear-camera");
            entity.Property(e => e.RearSeatHeatVent)
                .HasMaxLength(100)
                .HasColumnName("rear-seat-heat-vent");
            entity.Property(e => e.RearSeatMemory)
                .HasMaxLength(100)
                .HasColumnName("rear-seat-memory");
            entity.Property(e => e.RearSeatsHeat)
                .HasMaxLength(100)
                .HasColumnName("rear-seats-heat");
            entity.Property(e => e.ReduceSpareWheel)
                .HasMaxLength(100)
                .HasColumnName("reduce-spare-wheel");
            entity.Property(e => e.RemoteEngineStart)
                .HasMaxLength(100)
                .HasColumnName("remote-engine-start");
            entity.Property(e => e.RollerBlindForRearWindow)
                .HasMaxLength(100)
                .HasColumnName("roller-blind-for-rear-window");
            entity.Property(e => e.RollerBlindsForRearSideWindows)
                .HasMaxLength(100)
                .HasColumnName("roller-blinds-for-rear-side-windows");
            entity.Property(e => e.RoofRails)
                .HasMaxLength(100)
                .HasColumnName("roof-rails");
            entity.Property(e => e.SeatMemory)
                .HasMaxLength(100)
                .HasColumnName("seat-memory");
            entity.Property(e => e.SeatTransformation)
                .HasMaxLength(100)
                .HasColumnName("seat-transformation");
            entity.Property(e => e.Servo)
                .HasMaxLength(100)
                .HasColumnName("servo");
            entity.Property(e => e.SpareWheel)
                .HasMaxLength(100)
                .HasColumnName("spare-wheel");
            entity.Property(e => e.SportPedals)
                .HasMaxLength(100)
                .HasColumnName("sport-pedals");
            entity.Property(e => e.SportSeats)
                .HasMaxLength(100)
                .HasColumnName("sport-seats");
            entity.Property(e => e.SportSuspension)
                .HasMaxLength(100)
                .HasColumnName("sport-suspension");
            entity.Property(e => e.StartButton)
                .HasMaxLength(100)
                .HasColumnName("start-button");
            entity.Property(e => e.StartStopFunction)
                .HasMaxLength(100)
                .HasColumnName("start-stop-function");
            entity.Property(e => e.SteelWheels)
                .HasMaxLength(100)
                .HasColumnName("steel-wheels");
            entity.Property(e => e.SteeringWheelGearShiftPaddles)
                .HasMaxLength(100)
                .HasColumnName("steering-wheel-gear-shift-paddles");
            entity.Property(e => e.ThirdRearHeadrest)
                .HasMaxLength(100)
                .HasColumnName("third-rear-headrest");
            entity.Property(e => e.ThirdRowSeats)
                .HasMaxLength(100)
                .HasColumnName("third-row-seats");
            entity.Property(e => e.TintedGlass)
                .HasMaxLength(100)
                .HasColumnName("tinted-glass");
            entity.Property(e => e.TrafficSignRecognition)
                .HasMaxLength(100)
                .HasColumnName("traffic-sign-recognition");
            entity.Property(e => e.TyrePressure)
                .HasMaxLength(100)
                .HasColumnName("tyre-pressure");
            entity.Property(e => e.Usb)
                .HasMaxLength(100)
                .HasColumnName("usb");
            entity.Property(e => e.VoiceRecognition)
                .HasMaxLength(100)
                .HasColumnName("voice-recognition");
            entity.Property(e => e.VolumeSensor)
                .HasMaxLength(100)
                .HasColumnName("volume-sensor");
            entity.Property(e => e.Vsm)
                .HasMaxLength(100)
                .HasColumnName("vsm");
            entity.Property(e => e.WheelConfiguration1)
                .HasMaxLength(100)
                .HasColumnName("wheel-configuration1");
            entity.Property(e => e.WheelConfiguration2)
                .HasMaxLength(100)
                .HasColumnName("wheel-configuration2");
            entity.Property(e => e.WheelHeat)
                .HasMaxLength(100)
                .HasColumnName("wheel-heat");
            entity.Property(e => e.WheelLeather)
                .HasMaxLength(100)
                .HasColumnName("wheel-leather");
            entity.Property(e => e.WheelMemory)
                .HasMaxLength(100)
                .HasColumnName("wheel-memory");
            entity.Property(e => e.WheelPower)
                .HasMaxLength(100)
                .HasColumnName("wheel-power");
            entity.Property(e => e.WindcleanerHeat)
                .HasMaxLength(100)
                .HasColumnName("windcleaner-heat");
            entity.Property(e => e.WindscreenHeat)
                .HasMaxLength(100)
                .HasColumnName("windscreen-heat");
            entity.Property(e => e.WirelessCharger)
                .HasMaxLength(100)
                .HasColumnName("wireless-charger");
            entity.Property(e => e.Xenon)
                .HasMaxLength(100)
                .HasColumnName("xenon");
            entity.Property(e => e.YaAuto)
                .HasMaxLength(100)
                .HasColumnName("ya-auto");
            entity.Property(e => e._12vSocket)
                .HasMaxLength(100)
                .HasColumnName("12v-socket");
            entity.Property(e => e._14InchWheels)
                .HasMaxLength(100)
                .HasColumnName("14-inch-wheels");
            entity.Property(e => e._15InchWheels)
                .HasMaxLength(100)
                .HasColumnName("15-inch-wheels");
            entity.Property(e => e._16InchWheels)
                .HasMaxLength(100)
                .HasColumnName("16-inch-wheels");
            entity.Property(e => e._17InchWheels)
                .HasMaxLength(100)
                .HasColumnName("17-inch-wheels");
            entity.Property(e => e._18InchWheels)
                .HasMaxLength(100)
                .HasColumnName("18-inch-wheels");
            entity.Property(e => e._19InchWheels)
                .HasMaxLength(100)
                .HasColumnName("19-inch-wheels");
            entity.Property(e => e._20InchWheels)
                .HasMaxLength(100)
                .HasColumnName("20-inch-wheels");
            entity.Property(e => e._21InchWheels)
                .HasMaxLength(100)
                .HasColumnName("21-inch-wheels");
            entity.Property(e => e._220vSocket)
                .HasMaxLength(100)
                .HasColumnName("220v-socket");
            entity.Property(e => e._22InchWheels)
                .HasMaxLength(100)
                .HasColumnName("22-inch-wheels");
            entity.Property(e => e._360Camera)
                .HasMaxLength(100)
                .HasColumnName("360-camera");

            entity.HasOne(d => d.Complectation).WithOne()
                .HasForeignKey<Option>(d => d.ComplectationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_options_modification");
        });

        modelBuilder.Entity<Specification>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("specifications")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.ComplectationId, "complectation_id").IsUnique();

            entity.Property(e => e.BackBrake)
                .HasMaxLength(100)
                .HasColumnName("back-brake");
            entity.Property(e => e.BackSuspension)
                .HasMaxLength(100)
                .HasColumnName("back-suspension");
            entity.Property(e => e.BackWheelBase)
                .HasMaxLength(100)
                .HasColumnName("back-wheel-base");
            entity.Property(e => e.BatteryCapacity)
                .HasMaxLength(100)
                .HasColumnName("battery-capacity");
            entity.Property(e => e.ChargeTime)
                .HasMaxLength(100)
                .HasColumnName("charge-time");
            entity.Property(e => e.Clearance)
                .HasMaxLength(100)
                .HasColumnName("clearance");
            entity.Property(e => e.ComplectationId)
                .HasMaxLength(50)
                .HasColumnName("complectation_id");
            entity.Property(e => e.Compression)
                .HasMaxLength(100)
                .HasColumnName("compression");
            entity.Property(e => e.ConsumptionCity)
                .HasMaxLength(100)
                .HasColumnName("consumption-city");
            entity.Property(e => e.ConsumptionHiway)
                .HasMaxLength(100)
                .HasColumnName("consumption-hiway");
            entity.Property(e => e.ConsumptionMixed)
                .HasMaxLength(100)
                .HasColumnName("consumption-mixed");
            entity.Property(e => e.CylindersOrder)
                .HasMaxLength(100)
                .HasColumnName("cylinders-order");
            entity.Property(e => e.CylindersValue)
                .HasMaxLength(100)
                .HasColumnName("cylinders-value");
            entity.Property(e => e.Diametr)
                .HasMaxLength(100)
                .HasColumnName("diametr");
            entity.Property(e => e.Drive)
                .HasMaxLength(100)
                .HasColumnName("drive");
            entity.Property(e => e.ElectricRange)
                .HasMaxLength(100)
                .HasColumnName("electric-range");
            entity.Property(e => e.EmissionEuroClass)
                .HasMaxLength(100)
                .HasColumnName("emission-euro-class");
            entity.Property(e => e.EngineFeeding)
                .HasMaxLength(100)
                .HasColumnName("engine-feeding");
            entity.Property(e => e.EngineOrder)
                .HasMaxLength(100)
                .HasColumnName("engine-order");
            entity.Property(e => e.EngineType)
                .HasMaxLength(100)
                .HasColumnName("engine-type");
            entity.Property(e => e.Feeding)
                .HasMaxLength(100)
                .HasColumnName("feeding");
            entity.Property(e => e.FrontBrake)
                .HasMaxLength(100)
                .HasColumnName("front-brake");
            entity.Property(e => e.FrontSuspension)
                .HasMaxLength(100)
                .HasColumnName("front-suspension");
            entity.Property(e => e.FrontWheelBase)
                .HasMaxLength(100)
                .HasColumnName("front-wheel-base");
            entity.Property(e => e.FuelEmission)
                .HasMaxLength(100)
                .HasColumnName("fuel-emission");
            entity.Property(e => e.FuelTankCapacity)
                .HasMaxLength(100)
                .HasColumnName("fuel-tank-capacity");
            entity.Property(e => e.FullWeight)
                .HasMaxLength(100)
                .HasColumnName("full-weight");
            entity.Property(e => e.GearValue)
                .HasMaxLength(100)
                .HasColumnName("gear-value");
            entity.Property(e => e.Height)
                .HasMaxLength(100)
                .HasColumnName("height");
            entity.Property(e => e.HorsePower)
                .HasMaxLength(100)
                .HasColumnName("horse-power");
            entity.Property(e => e.KvtPower)
                .HasMaxLength(100)
                .HasColumnName("kvt-power");
            entity.Property(e => e.Length)
                .HasMaxLength(100)
                .HasColumnName("length");
            entity.Property(e => e.MaxSpeed)
                .HasMaxLength(100)
                .HasColumnName("max-speed");
            entity.Property(e => e.Moment)
                .HasMaxLength(100)
                .HasColumnName("moment");
            entity.Property(e => e.MomentRpm)
                .HasMaxLength(100)
                .HasColumnName("moment-rpm");
            entity.Property(e => e.PetrolType)
                .HasMaxLength(100)
                .HasColumnName("petrol-type");
            entity.Property(e => e.PistonStroke)
                .HasMaxLength(100)
                .HasColumnName("piston-stroke");
            entity.Property(e => e.RangeDistance)
                .HasMaxLength(100)
                .HasColumnName("range-distance");
            entity.Property(e => e.RpmPower)
                .HasMaxLength(100)
                .HasColumnName("rpm-power");
            entity.Property(e => e.SafetyGrade)
                .HasMaxLength(100)
                .HasColumnName("safety-grade");
            entity.Property(e => e.SafetyRating)
                .HasMaxLength(100)
                .HasColumnName("safety-rating");
            entity.Property(e => e.Seats)
                .HasMaxLength(100)
                .HasColumnName("seats");
            entity.Property(e => e.TimeTo100)
                .HasMaxLength(100)
                .HasColumnName("time-to-100");
            entity.Property(e => e.Transmission)
                .HasMaxLength(100)
                .HasColumnName("transmission");
            entity.Property(e => e.TrunksMaxCapacity)
                .HasMaxLength(100)
                .HasColumnName("trunks-max-capacity");
            entity.Property(e => e.TrunksMinCapacity)
                .HasMaxLength(100)
                .HasColumnName("trunks-min-capacity");
            entity.Property(e => e.Valves)
                .HasMaxLength(100)
                .HasColumnName("valves");
            entity.Property(e => e.Volume)
                .HasMaxLength(100)
                .HasColumnName("volume");
            entity.Property(e => e.VolumeLitres)
                .HasMaxLength(100)
                .HasColumnName("volume-litres");
            entity.Property(e => e.Weight)
                .HasMaxLength(100)
                .HasColumnName("weight");
            entity.Property(e => e.WheelBase)
                .HasMaxLength(100)
                .HasColumnName("wheel-base");
            entity.Property(e => e.WheelSize)
                .HasMaxLength(100)
                .HasColumnName("wheel-size");
            entity.Property(e => e.Width)
                .HasMaxLength(100)
                .HasColumnName("width");

            entity.HasOne(d => d.Complectation).WithOne()
                .HasForeignKey<Specification>(d => d.ComplectationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_specifications_modification");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
