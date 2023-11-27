using Demoapi.Authentication;
using Demoapi.Data;
using Demoapi.Models;
using Microsoft.AspNetCore.Identity;

namespace Demoapi
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }


        public async Task SeedDataContext(DataContext dbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {

            // string[] roles = { "Admin", "User" };

            var roles = roleManager.Roles.ToList();
            if (!roles.Where(x => x.Name.ToUpper() == "Admin".ToUpper()).Any())
            {
                roleManager.CreateAsync(new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin"
                }).Wait();
            }
            if (!roles.Where(x => x.Name.ToUpper() == "Operator".ToUpper()).Any())
            {
                roleManager.CreateAsync(new IdentityRole
                {
                    Name = "Operator",
                    NormalizedName = "Operator"
                }).Wait();
            }

            List<(User, string, string)> usersToCreate = new();

            var appUser = new User
            {
                UserName = "Demo-Admin",
                FirstName = "shivam",
                LastName = "singh",
                Email = "shivamsingh@gmail.com",
                Role = "Admin",
                EmailConfirmed = true
            };


            usersToCreate.Add((appUser, "User$14123!", "Admin"));
            // if (!dataContext.Users.Any())
            // {
            // var user = new User
            // {
            //     UserName = "Demo-Admin",
            //     FirstName = "shivam",
            //     LastName = "singh",
            //     Email = "shivamsingh@gmail.com",
            //     Password = "12345678",
            //     Role = "Admin"
            // };
            //     };
            //     await dataContext.Users.AddRangeAsync(UsersList);
            //     await dataContext.SaveChangesAsync();
            // };

            foreach (var item in usersToCreate)
            {
                var newUser = item.Item1;
                var userName = newUser.UserName;
                var password = item.Item2;
                var role = item.Item3;

                var user = await userManager.FindByNameAsync(userName);
                if (user == null)
                {
                    var result = await userManager.CreateAsync(newUser, password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newUser, role);
                    }
                }
            }

            // var DbUsers = userManager.Users.ToList().Select(x => x.Email);
            // UsersListForLogin = UsersListForLogin.Where(x => !DbUsers.Contains(x.email)).ToList();
            // if (!dataContext.Users.Any())
            // {
            //     // logger.LogInformation("Uploading users seed data.");
            //     // foreach (var user in UsersListForLogin)
            //     // {
            //     userManager.CreateAsync(new User
            //     {
            //         UserName = user.UserName,
            //         Email = user.Email,
            //         Role = user.Role,
            //         Password = user.Password,
            //         // Preference = new Preference { Refresh = 10000 },
            //         CreatedOn = DateTime.UtcNow,
            //         UpdatedOn = DateTime.UtcNow,
            //         // IsUserEnabled = CommonStatus.Active,
            //         // Status = CommonStatus.Active
            //     }, user.Password).Wait();

            //     var u = await userManager.FindByEmailAsync(user.Email);
            //     await userManager.AddToRoleAsync(u, user.Role);
                // }
                // logger.LogInformation("Completed uploading users seed data.");
            // }



            //     if (dataContext.Pumps.Any()) return;

            //     var PumpsList = new List<Pump>()
            // {
            //     new Pump {
            //         PumpName = "Centrifugal Pump",
            //         PumpStatus = true,
            //         Type = "Pump",
            //         Description = "This is a pump"
            //     },
            //     new Pump {
            //         PumpName = "Jet Pump",
            //         PumpStatus = false,
            //         Type = "Pump",
            //         Description = "This is a pump"
            //     },
            //     new Pump {
            //         PumpName = "Piston Pump",
            //         PumpStatus = false,
            //         Type = "Pump",
            //         Description = "This is a pump"
            //     },
            //     new Pump {
            //         PumpName = "Centrifugal Pump",
            //         PumpStatus = false,
            //         Type = "Pump",
            //         Description = "This is a pump"
            //     },
            //     new Pump {
            //         PumpName = "Jet Pump",
            //         PumpStatus = true,
            //         Type = "Pump",
            //         Description = "This is a pump"
            //     }
            //   };

            //     if (dataContext.Pumps.Any()) return;

            //     var CameraList = new List<Camera>()
            //   {
            //     new Camera {
            //         CameraName = "Centrifugal Camera",
            //         CameraStatus = true,
            //         Type = "Camera",
            //         CameraDescription = "ok",
            //     },
            //     new Camera {
            //         CameraName = "Wildlife Camera",
            //         CameraStatus = true,
            //         Type = "Camera",
            //         CameraDescription = "ok",
            //     },
            //     new Camera {
            //         CameraName = "Centrifugal Camera",
            //         CameraStatus = true,
            //         Type = "Camera",
            //         CameraDescription = "ok",
            //     },
            //     new Camera {
            //         CameraName = "Security Camera",
            //         CameraStatus = true,
            //         Type = "Camera",
            //         CameraDescription = "ok",
            //     },
            //     new Camera {
            //         CameraName = "Wildlife Camera",
            //         CameraStatus = true,
            //         Type = "Camera",
            //         CameraDescription = "ok",
            //     }
            //   };

            //     if (dataContext.Pumps.Any()) return;

            //     var WheelList = new List<Wheel>()
            //   {
            //     new Wheel {
            //         WheelName = "Centrifugal Wheel",
            //         WheelStatus = true,
            //         Type = "Wheel",
            //         WheelDescription = "ok",
            //     },
            //     new Wheel {
            //         WheelName = "HotWheel",
            //         WheelStatus = true,
            //         Type = "Wheel",
            //         WheelDescription = "ok",
            //     },
            //     new Wheel {
            //         WheelName = "Centrifugal Wheel",
            //         WheelStatus = true,
            //         Type = "Wheel",
            //         WheelDescription = "ok",
            //     },
            //     new Wheel {
            //         WheelName = "Security Wheel",
            //         WheelStatus = true,
            //         Type = "Wheel",
            //         WheelDescription = "ok",
            //     },
            //     new Wheel {
            //         WheelName = "HotWheel",
            //         WheelStatus = true,
            //         Type = "Wheel",
            //         WheelDescription = "ok",
            //     }
            //   };

            // await dataContext.Users.AddRangeAsync(UsersList);
            // await dataContext.SaveChangesAsync();
            // await dataContext.Pumps.AddRangeAsync(PumpsList);
            // await dataContext.SaveChangesAsync();
            // await dataContext.Wheels.AddRangeAsync(WheelList);
            // await dataContext.SaveChangesAsync();
            // await dataContext.Cameras.AddRangeAsync(CameraList);
            // await dataContext.SaveChangesAsync();
            // };
        }
    }
}
