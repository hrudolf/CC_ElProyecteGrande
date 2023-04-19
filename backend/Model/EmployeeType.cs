using System.Text.Json.Serialization;

namespace backend.Model;

public class EmployeeType
{
    private static int Counter;
    public int Id { get; set; }
    public string Type { get; set; }
    private bool _isActive = true;

    public EmployeeType(string type)
    {
        Id = Counter++;
        Type = type;
    }

    [JsonConstructor]
    public EmployeeType(int id, string type)
    {
        Id = id;
        Type = type;
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