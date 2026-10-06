using LibraryManegment.Api.Data;
using LibraryManegment.Api.Service.Implementation;
using LibraryManegment.Api.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LibraryManagementDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("LMConnection")));

builder.Services.AddScoped<IMemberService, MemberService>();


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