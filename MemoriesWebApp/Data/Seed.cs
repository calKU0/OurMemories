using Microsoft.AspNetCore.Identity;
using MemoriesWebApp.Models;
using System.Diagnostics;
using System.Net;
using System.Globalization;

namespace MemoriesWebApp.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                if (!context.Meetings.Any())
                {
                    context.Meetings.AddRange(new List<Meeting>()
                    {
                        new Meeting()
                        {
                            DateStart = DateTime.ParseExact("08-03-2024 09:00:00", "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                            DateEnd = DateTime.ParseExact("10-03-2024 17:00:00", "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                            MeetingCity = Enum.MeetingCity.Rumia,
                            Realized = true
                        },
                        new Meeting()
                        {
                            DateStart = DateTime.ParseExact("11-04-2024 23:00:00", "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                            DateEnd = DateTime.ParseExact("14-04-2024 17:00:00", "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                            MeetingCity = Enum.MeetingCity.Zawada,
                            Realized = true
                        },
                        new Meeting()
                        {
                            DateStart = DateTime.ParseExact("26-04-2024 09:00:00", "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                            DateEnd = DateTime.ParseExact("05-05-2024 17:00:00", "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                            MeetingCity = Enum.MeetingCity.Rumia,
                            Realized = false
                        }
                    });
                    context.SaveChanges();
                }
                //Images
                if (!context.Images.Any())
                {
                    context.Images.AddRange(new List<Image>()
                    {
                        new Image()
                        {
                            Name = "Zdjecie 1",
                            Url = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            MeetingId = 1
                        },
                        new Image()
                        {
                            Name = "Zdjecie 2",
                            Url = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "Opis zdjecia 2",
                            MeetingId = 2
                        }
                    });
                    context.SaveChanges();
                }
            }
        }

        /*public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "teddysmithdeveloper@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "teddysmithdev",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }*/
    }
}