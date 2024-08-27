using Application;
using Infrastructure;
using Infrastructure.Common.Extensions;
using Presentation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

builder.Logging.ClearProviders();
builder.Host.UseSerilog(Log.Logger);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();
app.UseInfrastructure();

app.UseAuthentication();
app.UseAuthorization();
app.MapHealthCheckz();

app.MapControllers();

app.Run();