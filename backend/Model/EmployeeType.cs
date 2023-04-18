namespace backend.Model;

public class EmployeeType
{
    private static int Counter = 0;
    public int Id { get; }
    public string Type { get; set; }
    public bool IsDeleted { get; set; }

    public EmployeeType(string type)
    {
        Id = Counter++;
        Type = type;
        IsDeleted = false;
    }
}