using Microsoft.EntityFrameworkCore;

namespace backend.Model;

public enum UserRole : byte
{
    Admin,
    Basic,
    ShiftLead,
    Supervisor,
    Accountant
}