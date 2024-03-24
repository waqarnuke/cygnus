using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Models.Identity;

namespace DataAccess.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
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
                        State = "Tx",
                        ZipCode = "79221"

                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}