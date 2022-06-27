using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataSeed
{
    //VOID TYPE THAT DOESN'T RETURN ANYTHING
    public static async Task SeedUsers(AppContextDb context)
    {
        if (await context.Users.AnyAsync()) return;

        var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");

        var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

        foreach (var user in users)
        {
            using var hmac = new HMACSHA512();

            user.Username = user.Username.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("string2"));
            user.PasswordSalt = hmac.Key;
            context.Users.Add(user);
        }
        
        await context.SaveChangesAsync();
        
    }
}