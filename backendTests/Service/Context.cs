using backend.Database;
using backend.Database.Seed;
using Microsoft.EntityFrameworkCore;

namespace backendTests.Service;

public class Context
{
    public DataContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var databaseContext = new DataContext(options);
        databaseContext.Database.EnsureCreated();
        DataSeed dataSeed = new DataSeed(databaseContext);
        dataSeed.CreateAll(1);
        return databaseContext;
    }
}