using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using School.Interfaces;
using School.Middlewares;
using School.Models;
using School.Repositories;
using School.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication("default")
    .AddCookie("default", o =>
    {
        o.Cookie.Name = "mycookie";
        o.ExpireTimeSpan = new TimeSpan(0, 5, 0);
    });

builder.Services.AddAuthorization(b =>
{
    b.AddPolicy("myPolicy", pb => pb.RequireAuthenticatedUser());
});

var connectionString = "server=localhost;port=3306;uid=root;pwd=bhavana;database=School";

builder.Services.AddDbContext<SchoolContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddScoped<IStudent, StudentRepository>();
builder.Services.AddScoped<StudentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();
// app.UseMiddleware<CustomAuthenticationMiddleware>();

app.MapControllers();

// Added Minimal API's for Login and Logout.

app.MapPost("/login", async (HttpContext ctx) =>
{
    await ctx.SignInAsync("default",
        new ClaimsPrincipal(
            new ClaimsIdentity(new Claim[] { }, "default")),
        new AuthenticationProperties() { IsPersistent = true });



    return "OK";
});

app.MapPost("/logout", async (HttpContext ctx) =>
{

    await ctx.SignOutAsync();

    return "OK";
});



app.Run();
