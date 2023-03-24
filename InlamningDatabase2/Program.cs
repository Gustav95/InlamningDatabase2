using InlamningDatabase2.Services;

StatusService statusService = new();
MenuService menuService = new();
UserService userService = new();

await statusService.CreateStatusTypesAsync();
var currentUser = await userService.GetAsync(x => x.Email == "Gustav_e95@live.com");
if (currentUser == null)
    currentUser = await menuService.CreateUserAsync();

while (true)
{
    await menuService.MainMenu(currentUser.Id);
    Console.ReadKey();
}
