using FluentValidation.AspNetCore;
using System;
using System.Reflection;
using WebAPiDemo.Data;
using WebAPiDemo.Models;
namespace WebAPiDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddFluentValidation(c=>c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddScoped<CityRepository>();
            builder.Services.AddScoped<StateRepository>();
            builder.Services.AddScoped<CountryRepository>();
            //builder.Services.AddScoped<IValidator<CityModel>, CityValidator>();
            //builder.Services.AddScoped<IValidator<CountryModel>, CountryValidator>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
