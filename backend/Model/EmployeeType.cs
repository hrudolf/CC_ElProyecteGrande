using System.Text.Json.Serialization;

namespace backend.Model;

public class EmployeeType
{
    private static int Counter;
    public int Id { get; }
    public string Type { get; set; }
    private bool IsDeleted { get; set; }

    public EmployeeType(string type)
    {
        Id = Counter++;
        Type = type;
        IsDeleted = false;
    }

    [JsonConstructor]
    public EmployeeType(int id, string type)
    {
        Id = id;
        Type = type;
        IsDeleted = false;
    }

    public void ChangeIsDeleted()
    {
        IsDeleted = !IsDeleted;
    }

    public bool IsActive => !IsDeleted;
}