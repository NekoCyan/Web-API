using ControllerAPI_1721030861.Startup;

var builder = WebApplication.CreateBuilder(args);

// Builder startup container.
builder.Startup();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1607#issuecomment-607170559
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});

var app = builder.Build();

app.UseCors("Dev");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// App startup container.
app.Startup();

app.Run();
