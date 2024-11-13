using AzureProject.Business.Concrete;
using AzureProject.DataAccess.Abstract;
using AzureProject.DataAccess.DAL;
using AzureProject.DataAccess.EF;
using AzureProject.DataAccess.UnitOfWork.Interface;
using AzureProject.DataAccess.UnitOfWork;
using AzureProject.Entity.Concrete;
using AzureProject.Entity.Profiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public class Program
{
    public static void Main(string[] args) // Static olaraq düz?ltdik
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.d
        builder.Services.AddHttpClient();
        builder.Services.AddControllers();
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
        });
        // DbContext konfiqurasiya
        builder.Services.AddDbContext<AppDbContext>(option =>
        {
            option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
        });
        // Repository Dependency Injection
        builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();
        builder.Services.AddScoped<ISliderDal, EfSliderDal>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<CategoryManager>();
        builder.Services.AddScoped<SliderManager>();
        builder.Services.AddScoped<IJwtService, JwtService>();


        // Swagger konfiqurasiya
        builder.Services.AddSwaggerGen(opt =>
        {
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    new string[] {}
                }
            });
        });

        // Identity konfiqurasiya
        builder.Services.AddIdentity<AppUser, IdentityRole>(op =>
        {
            op.User.RequireUniqueEmail = false;
            op.Password.RequiredLength = 4;
            op.Password.RequireNonAlphanumeric = false;
            op.Password.RequireDigit = false;
            op.Password.RequireLowercase = false;
            op.Password.RequireUppercase = false;


            op.Lockout.AllowedForNewUsers = true;
            op.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(20);
            op.Lockout.MaxFailedAccessAttempts = 3;
        })
        .AddDefaultTokenProviders()
        .AddEntityFrameworkStores<AppDbContext>();

        // AutoMapper konfiqurasiya
        builder.Services.AddAutoMapper(opt =>
        {
            opt.AddProfile<MapperProfile>();
        });

        // Authentication & JWT
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(cnfg =>
            {
                cnfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

        builder.Services.AddEndpointsApiExplorer();

        var app = builder.Build();

        // Configure the HTTP request pipeline. 
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "swagger v1 api test");
                //options.RoutePrefix = string.Empty;
            });
        }
        app.UseCors();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}