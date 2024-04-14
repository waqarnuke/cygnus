
using Microsoft.AspNetCore.Identity;
using Models.Identity;
using Utility.Common;


namespace DataAccess.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if(!roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
            {
                roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();

                if(!userManager.Users.Any())
                {
                    var user= new AppUser
                    {
                        DisplayName = "bob",
                        Email = "bob@test.com",
                        UserName = "bob@test.com",
                        Address = new Address
                        {
                            FirstName = "bob",
                            LastName = "builder",
                            Street = "10101 S Memorial",
                            City = "Humble",
                            State = "Tx",
                            ZipCode = "79221"

                        }
                    };

                    await userManager.CreateAsync(user, "Pa$$w0rd");
                    userManager.AddToRoleAsync(user,SD.Role_Admin).GetAwaiter().GetResult();
                }
            }
        }
    }
}