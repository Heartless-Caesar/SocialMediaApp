using System.Text;
using API.Data;
using API.Helpers;
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
/*------*/ builder.Services.AddScoped<ITokenService, TokenService>(); /*------*/
/* -------------------------------------------------------------------------- */


/* --------------------- User repository added to scope ---------------------- */
/*-----*/ builder.Services.AddScoped<IUserRepository, UserRepository>(); /*---*/
/* -------------------------------------------------------------------------- */


/* ------------------------ AutoMapper Service ------------------------------ */
/*-*/ builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly); /*-*/
/* -------------------------------------------------------------------------- */

/* ------------------------ Cloudinary Settings ------------------------------ */
builder.Services.Configure<CloudinarySettings>(
    builder.Configuration.GetSection("CloudinarySettings"));
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
/* ---------------- */   builder.Services.AddCors();   /* -------------------*/
/* ------------------------------------------------------------------------- */


/* !!!!----------- BUILDING APP -------------!!!! */
var app = builder.Build();
/* !!!!-------------------------------------!!!! */


/* ---------- For data seed generation ------------ */
// using var scope = app.Services.CreateScope();
// var services = scope.ServiceProvider;
// try
// {
//     var context = services.GetRequiredService<AppContextDb>();
//     await context.Database.MigrateAsync();
//     await DataSeed.SeedUsers(context);
// }
// catch (Exception e)
// {
//     var logger = services.GetRequiredService<ILogger<Program>>();
//     logger.LogError(e, "Error occured during migration");
//     throw;
// }
/* ----------------------------------------------- */

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
