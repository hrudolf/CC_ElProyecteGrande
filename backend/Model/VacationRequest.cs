using System.Text.Json.Serialization;

namespace backend.Model;

public class VacationRequest
{
    private static int _counter;
    public int RequestId { get; }
    public int EmployeeId { get; }
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    public bool IsApproved { get; private set; }

    public VacationRequest(int employeeId, DateOnly startDate, DateOnly endDate)
    {
        RequestId = _counter++;
        EmployeeId = employeeId;
        StartDate = startDate;
        EndDate = endDate;
        IsApproved = false;
    }
    
    [JsonConstructor]
    public VacationRequest(int requestId, int employeeId, DateOnly startDate, DateOnly endDate, bool isApproved = false)
    {
        RequestId = requestId;
        EmployeeId = employeeId;
        StartDate = startDate;
        EndDate = endDate;
        IsApproved = isApproved;
    }
    
    public void UpdateVacationRequest(VacationRequest updatedData)
    {
        StartDate = updatedData.StartDate;
        EndDate = updatedData.EndDate;
        IsApproved = updatedData.IsApproved;
    }
}