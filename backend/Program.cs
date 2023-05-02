using System.Configuration;
using backend.Database;
using backend.Model;
using backend.Repositories;
using backend.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IRepository<EmployeeType>, EmployeeTypeRepo>();
builder.Services.AddSingleton<IRepository<Employee>, EmployeeRepo>();
builder.Services.AddSingleton<IRepository<Shift>, ShiftRepo>();
builder.Services.AddSingleton<IRepository<Roster>, RosterRepo>();
builder.Services.AddTransient<IEmployeeTypeService, EmployeeTypeService>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IShiftService, ShiftService>();
builder.Services.AddTransient<IRosterService, RosterService>();

builder.Services.AddCors();
builder.Services.AddSingleton<IRepository<VacationRequest>, VacationRequestRepo>();
builder.Services.AddTransient<IVacationRequestService, VacationRequestService>();
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
    //Seed data
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();