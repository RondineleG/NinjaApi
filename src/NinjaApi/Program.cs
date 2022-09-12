
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using NinjaApi.Models;
using NinjaApi.Repositories;
using NinjaApi.Services;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

builder.Services.AddControllers();

builder.Services.TryAddSingleton<IClanService, ClanService>();
builder.Services.TryAddSingleton<IClanRepository, ClanRepository>();
builder.Services.TryAddSingleton<IEnumerable<Clan>>(new Clan[]{
                new Clan { Name = "Iga" },
                new Clan { Name = "Kōga" },
            });
builder.Services.AddMvc();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", config =>
         config.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .Build()
    );
});
builder.Services.AddSwaggerGen(options =>
{
    var titleBase = "Ninja Api";
    var description = "This project is an API using dotnet 6.0 and LiteDB 5.0";
    var contact = new OpenApiContact()
    {
        Email = "rondineleg@gmail.com",
        Name = "Rondinele Guimarães"
    };

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = titleBase,
        Description = description,
        Contact = contact
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowAllOrigins");
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ninja Api");
});
app.Run();