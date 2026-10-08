using LibraryManegment.Api.Data;
using LibraryManegment.Api.Models;
using LibraryManegment.Api.Service.Implementations;
using LibraryManegment.Api.Service.Interface;
using LibraryManegment.Api.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LibraryManagementDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("LMConnection")));

// Add services to the container.
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IUserInfoService, UserInfoService>();
//builder.Services.AddScoped<IBookIssueService, BookIssueService>();


builder.Services.AddControllers();


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Library Management API",
        Version = "v1",
        Description = "Library Management System API"
    });
});

var app = builder.Build();

// Swagger
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

app.UseAuthorization();

app.MapControllers();

app.Run();