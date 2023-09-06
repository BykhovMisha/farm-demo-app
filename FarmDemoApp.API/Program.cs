using FarmDemoApp.BL.Handlers.Animal;
using FarmDemoApp.DataAccess;
using FarmDemoApp.DataAccess.Abstracts.Repository;
using FarmDemoApp.DataAccess.Entities;
using FarmDemoApp.DataAccess.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers(options =>
        {
            options.Filters.Add(new ProducesAttribute("application/json"));
        });


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<FarmContext>(options =>
        {
            options.UseInMemoryDatabase("FarmDb");
        });
        builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();

        builder.Services.AddMediatR(option =>
        {
            option.RegisterServicesFromAssemblyContaining<GetAnimalPageQuery>();
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            FillFakeData(app);
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }

    private static void FillFakeData(IHost host)
    {
        using (var scope = host.Services.CreateScope())
        using (var dbContext = scope.ServiceProvider.GetRequiredService<FarmContext>())
        {
            dbContext.Animals.AddRange(new List<Animal> 
            {
                new() { Name = "Cow" },
                new() { Name = "Pig" },
            });
            dbContext.SaveChanges();
        }

    }
}
