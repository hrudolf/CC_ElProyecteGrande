using System.Text.Json.Serialization;

namespace backend.Model;

public class Shift
{
    private static int _shiftIdCounter;
    public int ShiftId { get; }
    public string TimeOfShift { get; set; }
    public int NursesRequiredForShift { get; set; }
    public double BonusRate { get; set; }
    public bool _isActive = true;

    
    public Shift(string timeOfShift, int nursesRequiredForShift, double bonusRate)
    {
        ShiftId = _shiftIdCounter++;
        TimeOfShift = timeOfShift;
        NursesRequiredForShift = nursesRequiredForShift;
        BonusRate = bonusRate;  
    }

    [JsonConstructor]
    public Shift(int shiftId, string timeOfShift, int nursesRequiredForShift, double bonusRate)
    {
        ShiftId = shiftId;
        TimeOfShift = timeOfShift;
        NursesRequiredForShift = nursesRequiredForShift;
        BonusRate = bonusRate;
    }
    
    public bool GetIsActive()
    {
        return _isActive;
    }
    
    public void ChangeIsActive()
    {
        _isActive = !_isActive;
    }
}