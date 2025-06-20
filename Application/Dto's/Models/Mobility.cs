using Domain.Entities;
using Domain.Enums;
using DriveType = Domain.Enums.DriveType;

namespace Application.Dto_s.Models;

public class Mobility
{
    public string ModificationId { get; set; }
    public string FrontBrake { get; set; }
    public string BackBrake { get; set; }
    public string FrontSuspension { get; set; }
    public string BackSuspension { get; set; }
    public TransmissionType Transmission { get; set; }
    public DriveType Drive { get; set; }
    
    public bool ActiveSuspension { get; set; }
    public bool AirSuspension { get; set; }
    public bool ReducedSpareWheel { get; set; }
    public bool SpareWheel { get; set; }
    public bool SportSuspension { get; set; }
}