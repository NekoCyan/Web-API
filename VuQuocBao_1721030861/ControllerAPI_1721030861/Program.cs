using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Services;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services
    .AddDbContext<EFCoreDemoContext>(opt =>
        opt.UseSqlServer(builder.Configuration.GetConnectionString("EFcoreDemo"))
    )
    .AddDbContext<EcommerceAddressContext>(opt =>
        opt.UseSqlServer(builder.Configuration.GetConnectionString("EcommerceAddress"))
    );

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

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
