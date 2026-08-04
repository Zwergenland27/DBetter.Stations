using DBetter.Stations.Core;
using DBetter.Stations.Core.StationAggregates.Features.Search;
using DBetter.Stations.Infrastructure;
using DBetter.Stations.WebApi.StationFeatures;
using DBetter.Stations.WebApi.StationFeatures.Search;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCore();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.AddStationSearch();

app.Run();