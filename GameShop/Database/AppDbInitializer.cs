using GameShop.Enums;
using GameShop.Models;
using GameShop.Static;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GameShop.Database;

public class AppDbInitializer
{
    public static void Seed(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var data = serviceScope.ServiceProvider.GetService<AppDbContext>();
            data.Database.EnsureCreated();
            
            if (!data.Games.Any())
            {
                data.Games.AddRange(new List<Game>()
                {
                    new Game()
                    {
                        Image =
                            "https://image.ceneostatic.pl/data/products/27654568/i-grand-theft-auto-san-andreas-digital.jpg",
                        Title = "GTA San Andreas",
                        Description =
                            "Grand Theft Auto San Andreas to kolejna część kultowego cyklu GTA – jednej z najpopularniejszych serii wszechczasów, która pozwoliła graczom wcielić się w rolę drobnego rzezimieszka i rozsławiła w świecie gier firmę Rockstar Games.",
                        GameType = GameType.Sandbox,
                        Platform = Platform.PC,
                        ReleaseDate = new DateTime(2000, 1, 13),
                        Price = 9.99,
                        Language = "English"
                    },
                    new Game()
                    {
                        Image =
                            "https://image.ceneostatic.pl/data/products/95063/i-wladca-pierscieni-bitwa-o-srodziemie-classics-gra-pc.jpg",
                        Title = "Władca pierścieni Bitwa o śródziemie ",
                        Description =
                            "Opracowana przez zespół studia EA Los Angeles strategia czasu rzeczywistego, osadzona w fantastycznym uniwersum tolkienowskiego Śródziemia.",
                        GameType = GameType.Strategy,
                        Platform = Platform.XboxOne,
                        ReleaseDate = new DateTime(2007, 4, 21),
                        Price = 12.99,
                        Language = "Polish"
                    },
                    new Game()
                    {
                        Image = "https://image.ceneostatic.pl/data/products/48393919/i-call-of-duty-1-gra-pc.jpg",
                        Title = "Call of Duty",
                        Description =
                            "Call of Duty dostarcza nam dobrze wyważonej dawki realistycznej walki i intensywnej akcji znanej z filmów wojennych. Dzięki tej grze poznasz pole walki z zupełnie innej perspektywy - perspektywy bezimiennych bohaterów, szeregowych żołnierzy sojuszniczych państw, próbujących razem przywrócić właściwy bieg współczesnej historii.",
                        GameType = GameType.Action,
                        Platform = Platform.PC,
                        ReleaseDate = new DateTime(2004, 12, 8),
                        Price = 4.99,
                        Language = "Polish"
                    },
                    new Game()
                    {
                        Image =
                            "https://ecsmedia.pl/c/wiedzmin-3-dziki-gon-w-iext52299886.jpg",
                        Title = "Wiedźmin 3",
                        Description =
                            "Gra action RPG, stanowiąca trzecią część przygód Geralta z Rivii. Podobnie jak we wcześniejszych odsłonach cyklu, Wiedźmin 3: Dziki Gon bazuje na motywach twórczości literackiej Andrzeja Sapkowskiego, jednak nie jest bezpośrednią adaptacją żadnej z jego książek.",
                        GameType = GameType.RPG,
                        Platform = Platform.Playstation3,
                        ReleaseDate = new DateTime(2000, 1, 13),
                        Price = 19.99,
                        Language = "English"
                    },
                    
                    new Game()
                    {
                        Image =
                            "https://kupsiup.pl/images/ARTUR/Playstation/GRY/mini/x377px_wpx_ec4d3f0a3dbd9f1f07966affb514a511.jpg.pagespeed.ic.Grv8qgiqXj.webp",
                        Title = "THE LAST OF US II",
                        Description =
                            "Przeżyj druzgocące fizyczne i emocjonalne konsekwencje zemsty, jaką Ellie dokonuje w trakcie bezwzględnego pościgu za tymi, ktorzy ją skrzywdzili.",
                        GameType = GameType.RPG,
                        Platform = Platform.Playstation4,
                        ReleaseDate = new DateTime(2000, 1, 13),
                        Price = 89.99,
                        Language = "English"
                    },
                });
                data.SaveChanges();
            }
        }
    }

    public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                string adminUserEmail = "admin@admin.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if(adminUser == null)
                {
                    var newAdminUser = new IdentityUser()
                    {
                        UserName = "admin@admin.com",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Admin!2345");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                string appUserEmail = "user@user.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new IdentityUser()
                    {
                        UserName = "user@user.com",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "User!2345");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    
}
            
