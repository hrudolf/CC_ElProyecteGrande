using backend.Database;
using backend.DTOs;
using backend.Model;
using backend.Model.Records;
using Microsoft.EntityFrameworkCore;

namespace backend.Service;

public class RosterService : IRosterService
{
    private readonly DataContext _context;

    public RosterService(DataContext context)
    {
        _context = context;
    }

    public Roster? ConvertFromDto(RosterDto rosterData)
    {
        var employeeInDb = _context.Employees.FirstOrDefault(employee => employee.Id == rosterData.EmployeeId);
        if (employeeInDb == null) return null;
        var shiftInDb = _context.Shifts.FirstOrDefault(shift => shift.Id == rosterData.ShiftId);
        if (shiftInDb == null) return null;

        return new Roster
        {
            Date = rosterData.Date,
            Shift = shiftInDb,
            Employee = employeeInDb
        };
    }

    public Roster Create(Roster item)
    {
        _context.Rosters.Add(item);
        _context.SaveChanges();
        return item;
    }

    public IEnumerable<Roster> GetAll() =>
        _context.Rosters
            .Include(roster => roster.Employee)
            .Include(roster => roster.Shift)
            .Include(roster => roster.Employee.EmployeeType)
            .ToList()
            .Where(roster => roster.GetIsActive() == true)
            .OrderBy(roster => roster.Date)
        ;

    public Roster? GetById(int id) => GetAll().FirstOrDefault(roster => roster.Id == id);

    public IEnumerable<Roster> GetRostersByEmployeeId(int id)
    {
        return GetAll().Where(roster => roster.Employee != null && roster.Employee.Id == id);
    }

    public Roster? Delete(int id)
    {
        Roster? rosterInDb = GetById(id);
        if (rosterInDb != null && rosterInDb.GetIsActive())
        {
            _context.Rosters.Remove(rosterInDb);
            _context.SaveChanges();
            return rosterInDb;
        }

        return null;
    }

    public Roster? Update(Roster updatedData)
    {
        Roster? rosterInDb = GetById(updatedData.Id);
        if (rosterInDb != null && rosterInDb.GetIsActive())
        {
            rosterInDb.Date = updatedData.Date;
            rosterInDb.Shift = updatedData.Shift;
            rosterInDb.Employee = updatedData.Employee;
            rosterInDb.Attendance = updatedData.Attendance;
            if (rosterInDb.GetIsActive() != updatedData.GetIsActive())
            {
                rosterInDb.ChangeIsActive();
            }

            _context.SaveChanges();
            return rosterInDb;
        }

        return null;
    }

    public Roster? ChangeAttendance(int id)
    {
        Roster? requestInDb = GetById(id);
        if (requestInDb != null)
        {
            requestInDb.Attendance = !requestInDb.Attendance;
            _context.SaveChanges();
        }

        return requestInDb;
    }

    public bool GenerateWeeklyRoster(DateTime firstDayOfWeek)
    {
        IEnumerable<DateTime> listOfDates = _context.Rosters.ToList().Select(r => r.Date).Distinct();
        if (listOfDates.Contains(firstDayOfWeek)) return false;

        DateTime currentDay = firstDayOfWeek;
        while (currentDay.DayOfWeek != DayOfWeek.Monday)
        {
            currentDay = currentDay.AddDays(-1);
        }

        int numberOfDays = 7;
        int dayCounter = 1;
        
        List<Shift> shifts = _context.Shifts.ToList();
        // hard coded
        IEnumerable<Employee> employees = _context.Employees
            .Where(employee => employee.EmployeeType != null && employee.EmployeeType.Type != "Accountant" && employee.IsActive == true);
        IEnumerable<VacationRequest> vacationRequests = _context.VacationRequests
            .Where(request => request.IsApproved == true);
        List<EmployeeType> employeeTypes = _context.EmployeeTypes.ToList();

        while (dayCounter <= numberOfDays)
        {
            List<Employee> employeesNotOnHoliday = EmployeesNotOnHolidayToday(vacationRequests, currentDay, employees);

            foreach (var shift in shifts)
            {
               List<Employee> availableForShift = employeesNotOnHoliday
                    .Where(employee => employee.PreferredShift != null && employee.PreferredShift.Id == shift.Id)
                    .ToList();

                ChooseShiftLeader(availableForShift, currentDay, shift, employeeTypes);

                AddNursesToRoster(availableForShift, currentDay, shift, employeeTypes);
            }

            currentDay = currentDay.AddDays(1);
            dayCounter++;
        }


        return true;
    }

    public List<Forecast> WeeklyForeCast()
    {
        List<Forecast> forecast = new List<Forecast>();
        
        List<Roster> rosters = _context.Rosters
            .Include(roster => roster.Employee)
            .ToList();
        DateTime firstDay = rosters.Select(r => r.Date).Min();
        DateTime lastDay = rosters.Select(r => r.Date).Max();

        while (firstDay < lastDay)
        {
            int weeklyTotal = rosters
                .Where(r => r.Date >= firstDay && r.Date < firstDay.AddDays(7))
                .Select(r =>
                {
                    if (r.Employee != null) return r.Employee.SalaryPerShift;
                    return 0;
                })
                .ToList()
                .Sum();

            Forecast forecastItem = new Forecast(firstDay, firstDay.AddDays(7), weeklyTotal);
            forecast.Add(forecastItem);

            firstDay = firstDay.AddDays(7);
        }
        
        return forecast;
    }

    public void ChooseShiftLeader(List<Employee> availableForShift, DateTime currentDay, Shift shift,
        List<EmployeeType> employeeTypes)
    {
        Employee? shiftLeader = availableForShift
            .Where(employee => employee.EmployeeType.Type == "Shift lead nurse").MinBy(x => Random.Shared.Next());

        if (shiftLeader == null)
        {
            Create(new Roster
            {
                Date = currentDay, Shift = shift, Attendance = false, Warning = "No shift leader scheduled"
            });
        }
        else
        {
            Create(new Roster { Date = currentDay, Shift = shift, Employee = shiftLeader, Attendance = false });
            availableForShift.Remove(shiftLeader);
        }
    }

    public void AddNursesToRoster(List<Employee> availableForShift, DateTime currentDay, Shift shift,
        List<EmployeeType> employeeTypes)
    {
        var nursesRequiredForShift = shift.NursesRequiredForShift - 1;
        var counter = 1;
        while (counter <= nursesRequiredForShift)
        {
            Employee? employee =
                availableForShift.Where(employee => employee.EmployeeType != employeeTypes[2])
                    .MinBy(x => Random.Shared.Next());

            if (employee == null)
            {
                Create(new Roster
                    { Date = currentDay, Shift = shift, Attendance = false, Warning = "No nurse scheduled" });
            }
            else
            {
                Create(new Roster
                    { Date = currentDay, Shift = shift, Employee = employee, Attendance = false });
                availableForShift.Remove(employee);
            }


            counter++;
        }
    }

    public List<Employee> EmployeesNotOnHolidayToday(IEnumerable<VacationRequest> vacationRequests,
        DateTime currentDay, IEnumerable<Employee> employees)
    {
        var todaysVacationRequests = vacationRequests
            .Where(request => request.StartDate <= currentDay && request.EndDate >= currentDay);

        return employees
            .Where(employee => todaysVacationRequests.All(request => request.Employee.Id != employee.Id)).ToList();
    }
    /*
     * Conditions for generating a weekly roster
     *  + only employees who are not on a vacation can be taken into consideration
     *  + employees can only be added after set rest time TODO add int RequiredRestTime property to Shift model
     *  + there has to be one shift leader in each shift
     *    - if there are now shift leaders available the program issues a warning 
     *  + 
     *
     * Process of roster generation
     *  + ask for input: first day of the week
     *    - verify if that day is a monday
     *    - is not a monday, send error message
     *  + start loop for creating roster items
     *    - filter available employees
     *        - who are not on a holiday
     *        - who's preferred shift matches current shift
     *        - TODO who are after required rest time
     *        - TODO order them by their last shift ascending
     *        - TODO check working days
     *    - choose shift leader,
     *        - if no shift leader is available issue a warning
     *        - if a shift leader is available
     *             - save employee to Roster
     *             - remove employee from available employees
     *    - choose nurses for the rest of the required slots
     *         - if no suitable employee is found issue a warning
     *         - if a suitable employee is found save employee to Roster
     *             - save employee to Roster
     *             - remove employee from available employees
     *    - once complete move on to next shift
     * 
     */
}