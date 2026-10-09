
using LibraryManegment.Api.Data;
using LibraryManegment.Api.Models;
using LibraryManegment.Api.Service.Implementations;
using LibraryManegment.Api.Service.Interface;
using LibraryManegment.Api.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LibraryManagementDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("LMConnection")));

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IIssueBookService, IssueBookService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtSigner, JwtSigner>();

var jwtSettings = builder.Configuration.GetSection("Jwt");

var secretKey = jwtSettings["SecretKey"]
    ?? throw new InvalidOperationException("JWT SecretKey is missing.");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secretKey)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("User.Read", policy =>
       policy.RequireClaim("permission", "User.Read"));

    options.AddPolicy("User.Create", policy =>
        policy.RequireClaim("permission", "User.Create"));

    options.AddPolicy("User.Update", policy =>
        policy.RequireClaim("permission", "User.Update"));

    options.AddPolicy("User.Delete", policy =>
        policy.RequireClaim("permission", "User.Delete"));

    options.AddPolicy("User.RoleUpdate", policy =>
        policy.RequireClaim("permission", "User.RoleUpdate"));

    options.AddPolicy("BookIssue.Read", policy =>
        policy.RequireClaim("permission", "BookIssue.Read"));

    options.AddPolicy("BookIssue.Create", policy =>
        policy.RequireClaim("permission", "BookIssue.Create"));

    options.AddPolicy("BookIssue.Return", policy =>
        policy.RequireClaim("permission", "BookIssue.Return"));

    options.AddPolicy("ExtensionRequest.Read", policy =>
        policy.RequireClaim("permission", "ExtensionRequest.Read"));

    options.AddPolicy("ExtensionRequest.Create", policy =>
        policy.RequireClaim("permission", "ExtensionRequest.Create"));

    options.AddPolicy("ExtensionRequest.Approve", policy =>
        policy.RequireClaim("permission", "ExtensionRequest.Approve"));

    options.AddPolicy("ExtensionRequest.Reject", policy =>
        policy.RequireClaim("permission", "ExtensionRequest.Reject"));

    options.AddPolicy("Book.Read", policy =>
        policy.RequireClaim("permission", "Book.Read"));

    options.AddPolicy("Book.Create", policy =>
        policy.RequireClaim("permission", "Book.Create"));

    options.AddPolicy("Book.Update", policy =>
        policy.RequireClaim("permission", "Book.Update"));

    options.AddPolicy("Book.Delete", policy =>
        policy.RequireClaim("permission", "Book.Delete"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Library Management API",
        Version = "v1",
        Description = "Library Management System API"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT access token."
    });

    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)] =
                new List<string>()
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/swagger/v1/swagger.json",
            "Library Management API v1");

        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

