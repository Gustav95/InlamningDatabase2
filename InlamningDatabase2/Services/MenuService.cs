

using InlamningDatabase2.Models.Entities;

namespace InlamningDatabase2.Services
{
    internal class MenuService
    {
        private readonly UserService _userService = new();
        private readonly CaseService _caseService = new();

        public async Task<UserEntity> CreateUserAsync()
        {
            var _entity = new UserEntity();
            Console.Clear();
            Console.WriteLine("------Create user------");

            Console.WriteLine("FirstName");
            _entity.FirstName = Console.ReadLine() ?? "";
            Console.WriteLine("LastName");
            _entity.LastName = Console.ReadLine() ?? "";
            Console.WriteLine("Email");
            _entity.Email = Console.ReadLine() ?? "";

           return await _userService.CreateAsync(_entity);
        }

        public async Task MainMenu(int userId)
        {
            Console.Clear();
            Console.WriteLine("------Meny------");
            Console.WriteLine("1. Visa alla Ärenden");
            Console.WriteLine("2. Visa handläggare");
            Console.WriteLine("3. Skapa ett ärende");
            Console.WriteLine("4. Se ett specifikt case");
            Console.WriteLine("Välj ett alternativ");
            var option = Console.ReadLine();

            switch(option)
            {
                case "1":
                    await ActiveCasesAsync();
                    break;

                case "2":
                    await HandlersAsync();
                    break;

                case "3":
                    await NewCaseAsync(userId);
                    break;

                case "4":
                    await ShowCase();
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Inget giltligt alternativ! Välj om");
                    break;
            }
        }

        private async Task ActiveCasesAsync()
        {
            Console.Clear();
            Console.WriteLine("------Meny------");
            foreach(var _case in await _caseService.GetAllActiveAsync())
            {
                Console.WriteLine($" Case ID: {_case.Id}");
                Console.WriteLine($" Created: {_case.Created}");
                Console.WriteLine($" Modified: {_case.Modified}");
                Console.WriteLine($" Status: {_case.Status.StatusName}");
                Console.WriteLine("");
            }
        }
        private async Task HandlersAsync()
        {
            Console.Clear();
            Console.WriteLine("------Handlers------");
            foreach (var _user in await _userService.GetAllAsync())
            {
                Console.WriteLine($" Handler Id: {_user.Id}");
                Console.WriteLine($" Name: {_user.FirstName} {_user.LastName}");
                Console.WriteLine($" Email: {_user.Email}");
                Console.WriteLine("");
            }
        }
        private async Task NewCaseAsync(int userId)
        {
            var _entity = new CaseEntity { UserId = userId };
            Console.Clear();
            Console.WriteLine("------New Case------");

            Console.WriteLine("CustomerName");
            _entity.CustomerName = Console.ReadLine() ?? "";
            Console.WriteLine("Customer Email");
            _entity.CustomerEmail = Console.ReadLine() ?? "";
            Console.WriteLine("Description");
            _entity.Description = Console.ReadLine() ?? "";

            await _caseService.CreateAsync( _entity );
            await ActiveCasesAsync();
        }

        private async Task ShowCase()
        {
  
            Console.WriteLine("------ChooseCase------");
            
            string caseId = Console.ReadLine() ?? "";
            int value = int.Parse(caseId);

            var check = await _caseService.GetById(value);
            Console.WriteLine($" Case ID: {check.Id}");
            Console.WriteLine($" Created: {check.Created}");
            Console.WriteLine($" Modified: {check.Modified}");
            Console.WriteLine($" Status: {check.Status?.StatusName}");
            Console.WriteLine("");

            Console.WriteLine("Do you Whant to change staus?(yes/no)");
            string answer = Console.ReadLine() ?? "";
            if (answer == "yes")
            {
                Console.WriteLine("pick status: 1.Ej Börjad,2.Pågår,3.Avslutad");
                string statusId = Console.ReadLine() ?? "";
                int val = int.Parse(statusId);
                await _caseService.UpdatCaseStatusAsync(check.Id, val);
                Console.WriteLine("You have change status");
            }
            else if (answer == "no")
            {
                Console.Clear();
            }
        }

    }
}
