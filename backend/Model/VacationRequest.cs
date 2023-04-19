namespace backend.Model;

public class VacationRequest
{
    private static int Counter;
    public int RequestId { get; }
    public int EmployeeId { get; }
    public DateOnly StartDate { get; }
    public DateOnly EndDate { get; }
    private bool _isApproved;

    public VacationRequest(int employeeId, DateOnly startDate, DateOnly endDate)
    {
        RequestId = Counter++;
        EmployeeId = employeeId;
        StartDate = startDate;
        EndDate = endDate;
    }
    public bool GetIsApproved()
    {
        return _isApproved;
    }
    
    public void ChangeIsApproved(bool isApproved)
    {
        _isApproved = isApproved;
    }
}