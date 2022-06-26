using System.Text;
using API.Data;
using API.Interface;
using API.Middleware;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

/* ---------------------------------- SQLite Connection ----------------------------------- */
builder.Services.AddDbContext<AppContextDb>(options =>
{
    //INSERT CONNECTION STRING AS AN ARGUMENT
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
/* --------------------------------------------------------------------------------------- */



/* -------------------------- Generate JWT Tokens ----------------------------*/
builder.Services.AddScoped<ITokenService, TokenService>();
/* -------------------------------------------------------------------------- */


/* -------------------------- Add Authentication -----------------------------*/
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])
                ),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
/* -------------------------------------------------------------------------- */


/* ----- Adding a CORS Policy to be used in app.UseCors as an argument ----- */
builder.Services.AddCors();
/* ------------------------------------------------------------------------- */

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(policy =>
{
    policy.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
