using Api.Configurations;
using Application;
using FluentValidation.AspNetCore;
using Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddConfigurations();

builder.Services.AddControllers(opt =>
{
    opt.SuppressAsyncSuffixInActionNames = false;
});
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddInfra(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();
await app.Services.InitializeDatabasesAsync();

app.UseHttpsRedirection();

app.UseInfra(builder.Configuration);
app.MapEndpoints();
app.Run();

public partial class Program { }