using Newtonsoft.Json;
using SimulacrumSharp.SimulationAPI.Helpers;
using SimulacrumSharp.SimulationAPI.Helpers.Interfaces;
using SimulacrumSharp.SimulationAPI.Services.Interfaces.Simulation;
using SimulacrumSharp.SimulationAPI.Services.Interfaces.Simulation.Survivor;
using SimulacrumSharp.SimulationAPI.Services.Simulation.DragRace;
using SimulacrumSharp.SimulationAPI.Services.Simulation.Survivor;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISurvivorSimulationService, SurvivorSimulationService>();
builder.Services.AddScoped<ITribalCouncilService, TribalCouncilService>();
builder.Services.AddScoped<IDragRaceSimulationService, DragRaceSimulationService>();

builder.Services.AddScoped<ICommonHelper, CommonHelper>();

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
