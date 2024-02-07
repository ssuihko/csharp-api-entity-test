using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using workshop.wwwapi.Data;
using workshop.wwwapi.Endpoints;
using workshop.wwwapi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(

    opt => {
        opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString"));
   }
    );
builder.Services.AddScoped<IRepository,Repository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyProjectMigrations();
}

app.UseHttpsRedirection();
app.ConfigurePatientEndpoint();
app.ApplyProjectMigrations();

app.Run();

public partial class Program { } // needed for testing - please ignore