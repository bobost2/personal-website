using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PWBackend.Models.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "bobost.net",
        ValidAudience = "bobost.net",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSalt"]!))
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PWBackend", Version = "v1" });
    var securityScheme = new OpenApiSecurityScheme()
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearerAuth"
                }
            },
            []
        }
    };

    c.AddSecurityDefinition("bearerAuth", securityScheme);
    c.AddSecurityRequirement(securityRequirement);
});

builder.Services.AddDbContext<PWDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options => options.AddPolicy("AllowOrigin",
    corsBuilder => corsBuilder.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    // Ensure the database is created and apply any pending migrations
    var context = scope.ServiceProvider.GetRequiredService<PWDbContext>();
    context.Database.Migrate();

    // Add admin user if no users exist
    var users = context.Users.ToList();
    if (users.Count == 0)
    {
        var adminUser = new User
        {
            Username = builder.Configuration["AdminUserDefaults:Username"]!,
            Password = BCrypt.Net.BCrypt.HashPassword(builder.Configuration["AdminUserDefaults:Password"]!),
            RequiresReset = true,
            HasOTP = false
        };
        context.Users.Add(adminUser);
        context.SaveChanges();
    }

    // Add project types if none exist
    var projectTypes = context.ProjectTypes.ToList();
    if (projectTypes.Count == 0)
    {
        var types = new List<ProjectType>
        {
            new ProjectType { Name = "Game" },
            new ProjectType { Name = "Application" },
            new ProjectType { Name = "Animation" },
            new ProjectType { Name = "Other" }
        };
        context.ProjectTypes.AddRange(types);
        context.SaveChanges();
    }

    // Add project statuses if none exist
    var projectStatuses = context.ProjectStatuses.ToList();
    if (projectStatuses.Count == 0)
    {
        var statuses = new List<ProjectStatus>
        {
            new ProjectStatus { Name = "In Progress" },
            new ProjectStatus { Name = "Completed" },
            new ProjectStatus { Name = "Frozen" },
            new ProjectStatus { Name = "Abandoned" }
        };
        context.ProjectStatuses.AddRange(statuses);
        context.SaveChanges();
    }

    // Add project technologies if none exist
    var projectTechnologies = context.ProjectTechnologies.ToList();
    if (projectTechnologies.Count == 0)
    {
        var technologies = new List<ProjectTechnology>
        {
            new ProjectTechnology { Name = "Unity" },
            new ProjectTechnology { Name = "Unreal Engine" },
            new ProjectTechnology { Name = "Blender" },
            new ProjectTechnology { Name = "C#" },
            new ProjectTechnology { Name = "C++" }
        };

        context.ProjectTechnologies.AddRange(technologies);
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => options.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();
