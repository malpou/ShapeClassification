using TypeOfShape.Api;
using TypeOfShape.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseEndpoints<Program>();

app.Run();