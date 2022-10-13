using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using ProductOwner.Microservice.Database;
using ProductOwner.Microservice.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
////builder.Services.AddScoped<IProductOwnerService,ProductOwnerService>();

////builder.Services.AddScoped<IProductOwnerService, ProductOwnerService>();

builder.Services.AddDbContext<DbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
///builder.Services.AddDbContext<DbContextClass>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
