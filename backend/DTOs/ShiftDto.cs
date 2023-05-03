namespace backend.DTOs;

public class ShiftDto
{
    public string NameOfShift { get; set; } = String.Empty;
    public int NursesRequiredForShift { get; set; }
    public double BonusRate { get; set; }
}