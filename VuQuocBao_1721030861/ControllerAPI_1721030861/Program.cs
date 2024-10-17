using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Services;
using static ControllerAPI_1721030861.Utils.WebBuilderUtils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Startup();

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1607#issuecomment-607170559
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
