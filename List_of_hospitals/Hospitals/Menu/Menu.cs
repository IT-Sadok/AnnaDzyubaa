using Hospitals;

public partial class Menu
{
    private AppFunctions _appFunctions;
    private int _number;
    public Menu(AppFunctions appFunctions)
    {
        this._appFunctions = appFunctions;
    }

    public void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("Hospital's menu:");
        Console.WriteLine();

        foreach (MenuList menuItem in Enum.GetValues(typeof(MenuList)))
        {
            Console.WriteLine($"{(int)menuItem}) {menuItem}");
        }

        ReadConsole();
    }

    public void ReadConsole()
    {
        Console.WriteLine();
        Console.WriteLine("Make your choise and type number");

        while (true)
        {
            var console = int.TryParse(Console.ReadLine(), out _number);
            var menuArray = Enum.GetNames(typeof(Menu.MenuList));

            if (console == false)
            {
                Console.WriteLine("Please, type only one number");
            }
            else if (_number < 1 || _number > menuArray.Length)
            {
                Console.WriteLine($"You can choose a number only from 1 to {menuArray.Length}");
            }
            else
            {
                break;
            }
        }

        ChoosingFunction();
    }

    public void ChoosingFunction()
    {
        switch ((MenuList)_number)
        {
            case MenuList.PacientRegistration:
                _appFunctions.PacientRegistration();
                break;
            case MenuList.ShowPacientAccountInfo:
                _appFunctions.ShowPacientAccountInfo();
                break;
            case MenuList.ShowHospitalsList:
                _appFunctions.ShowHospitalsList();
                break;
            case MenuList.ShowDoctorsList:
                _appFunctions.ShowDoctorsList();
                break;
            case MenuList.ProcessMakingAppointmentWithDoctor:
                _appFunctions.ProcessMakingAppointmentWithDoctor();
                break;
            case MenuList.ShowDoctorsAppointment:
                _appFunctions.ShowDoctorsAppointment();
                break;
            case MenuList.CancelDoctorsAppointment:
                _appFunctions.CancelDoctorsAppointment();
                break;
            case MenuList.Exit:
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine($"Invalid input, type a number from 1 to 6");
                break;
        }

        //Console.WriteLine("Press <Enter> to exit.");
        //var console = Console.ReadKey();

        //if (Console.ReadKey().Key != ConsoleKey.Enter)
        //{

        //}
    }
}
