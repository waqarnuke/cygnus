using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Identity;
using Utility.Common;

namespace DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AppIdentityDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, AppIdentityDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            
        }
        public void Initialize()
        {
            // Migrations if they are not applied 
            try
            {
                if(_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception ex){

            }

             if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult()) {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();


                //if roles are not created, then we will create admin user as well
                _userManager.CreateAsync(new AppUser {
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
                }, "Admin123*").GetAwaiter().GetResult();


                AppUser user = _context.AppUsers.FirstOrDefault(u => u.Email == "admin@dotnetmastery.com");
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();

            }

            return;
        }
    }
}