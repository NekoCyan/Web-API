using ControllerAPI_1721030861.Startup;

var builder = WebApplication.CreateBuilder(args);

// Builder startup container.
builder.Startup();

var app = builder.Build();

// App startup container.
app.Startup();

app.Run();
