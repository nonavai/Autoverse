namespace Application.Dto_s.Models;

public class Dimension
{
    public string ModificationId { get; set; }
    public float Height { get; set; }
    public float Width { get; set; }
    public float Length { get; set; }
    public float WheelBase { get; set; }
    public float? FrontWheelBase { get; set; }
    public float? BackWheelBase { get; set; }
    public string? Clearance { get; set; }
    public string? WheelSize { get; set; }
}
