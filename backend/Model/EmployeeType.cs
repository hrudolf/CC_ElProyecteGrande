using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Model;

public class EmployeeType
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Type { get; set; }
    private bool _isActive = true;

    [JsonIgnore] public List<Employee> EmployeeList { get; set; } = new();
    public EmployeeType(string type)
    {
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