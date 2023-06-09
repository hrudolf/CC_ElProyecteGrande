using backend.Database;
using backend.Database.Seed;
using backend.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
{
    options.Cookie.Name = "MyCookieAuth";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    options.Cookie.SameSite = SameSiteMode.None;
    options.Events.OnRedirectToAccessDenied =
        options.Events.OnRedirectToLogin = c =>
        {
            c.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
});

builder.Services.AddTransient<IEmployeeTypeService, EmployeeTypeService>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IShiftService, ShiftService>();
builder.Services.AddTransient<IRosterService, RosterService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddCors();

builder.Services.AddTransient<IVacationRequestService, VacationRequestService>();
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

/*var ourValue = builder.Configuration.GetValue<string>("Value");
var ourValue2 = builder.Configuration.GetValue<string>("Value2");*/

var app = builder.Build();

/*app.Logger.LogInformation("######################");
app.Logger.LogInformation(ourValue);
app.Logger.LogInformation(ourValue2);
app.Logger.LogInformation("######################");*/


using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();

    //context.Database.EnsureDeleted();

    //true if has to be created, false if already exists
    //always true if EnsureDeleted is allowed
    if (context.Database.EnsureCreated())
    {
        //if it is newly created, seed data:
        DataSeed dataSeed = new DataSeed(context);
        dataSeed.CreateAll(30);
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x =>
{
    x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000").AllowCredentials();
    //x.AllowAnyHeader().AllowAnyMethod().WithOrigins(builder.Configuration.GetValue<string>("endpoint")).AllowCredentials();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();