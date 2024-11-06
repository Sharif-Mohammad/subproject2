using Business;
using Persistence;
using Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Auth;
using Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using API.Middleware;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Logging;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContextPool<ApplicationDbContext>(opt => 
            opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();



        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Movie API", Version = "v1" });

            // Add JWT Authentication to Swagger
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter 'Bearer' [space] and then your valid token."
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
        });
        builder.Services.AddBusinessServices();
        builder.Services.AddRepositories();
        builder.Services.AddAuthServices(builder.Configuration);
        IdentityModelEventSource.ShowPII = true;


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseHttpsRedirection();

        app.UseAuthentication(); // This enables the authentication middleware
        app.UseAuthorization();  // This enables the authorization middleware
        app.MapControllers();

        app.Run();
    }
}