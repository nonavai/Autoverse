using Domain.Enums;

namespace Application.Dto_s.Models;

public class Interior
{
    public string ModificationId { get; set; }
    public string Seats { get; set; }
    public int? TrunksMinCapacity { get; set; }
    public int? TrunksMaxCapacity { get; set; }
    public InteriorMaterial InteriorMaterial { get; set; }
    public bool BlackRoof { get; set; }
    public bool DecorativeInteriorLighting { get; set; }
    public bool FoldingFrontPassengerSeat { get; set; }
    public bool FoldingTablesRear { get; set; }
    public bool FrontCentreArmrest { get; set; }
    public bool Hatch { get; set; }
    public bool LeatherGearStick { get; set; }
    public bool PanoramaRoof { get; set; }
    public bool RollerBlindForRearWindow { get; set; }
    public bool RollerBlindsForRearSideWindows { get; set; }
    public bool SeatMemory { get; set; }
    public bool SeatTransformation { get; set; }
    public bool SportPedals { get; set; }
    public bool SportSeats { get; set; }
    public bool ThirdRearHeadrest { get; set; }
    public bool ThirdRowSeats { get; set; }
    public bool TintedGlass { get; set; }
    public bool LeatherSteeringWheel { get; set; }
    public bool AdjustablePedals { get; set; }
}
