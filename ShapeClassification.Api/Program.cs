using ShapeClassification.Api;
using ShapeClassification.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseEndpoints<Program>();

app.Run();