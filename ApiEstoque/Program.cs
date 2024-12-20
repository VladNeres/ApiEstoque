﻿using ApiEstoque.DependenceInjecton;
using Microsoft.OpenApi.Models;
using ServiceBus;
using ServiceBus.Base;
using ServiceBus.Consumers;
using ServiceBus.Interfaces;
using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation(builder.Configuration)
    .AddServiceBus(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiEstoque", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



var consumerFactory = app.Services.GetRequiredService<RabbitConsumerFactory>();
consumerFactory.StartConsumers();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();