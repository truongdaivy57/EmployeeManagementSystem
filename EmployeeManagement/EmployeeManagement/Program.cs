using EmployeeManagement.Data;
using EmployeeManagement.Helper;
using EmployeeManagement.Model;
using EmployeeManagement.Repositories;
using EmployeeManagement.Repository;
using EmployeeManagement.Service;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Employee Management",
        Version = "v1",
        Description = "This is an employee management system"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header,
        BearerFormat = "JWT",
        Scheme = "Bearer",
        Description = "Please input your token"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] { }
        }
    });
});

//builder.Services.Configure<IdentityOptions>(config =>
//{
//    config.Password.RequireNonAlphanumeric = true;
//    config.Password.RequireDigit = true;
//    config.Password.RequireLowercase = true;
//    config.Password.RequireUppercase = true;
//});

builder.Services.AddIdentity<User, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IServiceProvider, ServiceProvider>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<RoleManager<IdentityRole<Guid>>>();
builder.Services.AddScoped<UserManager<User>>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<Department>, DepartmentRepository>();
builder.Services.AddScoped<ITokenHandler, EmployeeManagement.Helper.TokenHandler>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<SendMail>();
builder.Services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);

// Register Lazy services to avoid circular dependencies
builder.Services.AddScoped(provider => new Lazy<ITokenHandler>(() => provider.GetRequiredService<ITokenHandler>()));
builder.Services.AddScoped(provider => new Lazy<IUserService>(() => provider.GetRequiredService<IUserService>()));

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
