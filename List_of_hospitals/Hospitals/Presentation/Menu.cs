using Hospitals.Models;
using Hospitals.Services;

public class Menu
{
    private int _userChoiceNumber;
    private IPatientService _patientService;

    public Menu(IPatientService patientService)
    {
        _patientService = patientService;
    }

    public void PatientRegistration()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("Enter your name");
            Console.WriteLine();

            string? name = Console.ReadLine();

            var validation = _patientService.NameValidation(name);

            Console.WriteLine();
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();

            if (validation)
            {
                break;
            }
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Enter your phone number +380 ... (9 numbers)");
            Console.WriteLine();

            var phoneNumber = Console.ReadLine();

            bool validation = _patientService.PhoneNumberValidation(phoneNumber);

            Console.WriteLine();
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();

            if (validation)
            {
                break;
            }
        }

        _patientService.AddNewPatient();

        Console.WriteLine();
        Console.WriteLine("Press any key to continue ...");
        Console.ReadKey();
    }

    public Patient ShowPacientAccountInfo()
    {
        Patient? patientInfo;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Type patient ID to find its information");
            Console.WriteLine();

            var console = Console.ReadLine();
            patientInfo = _patientService.SearchPatientById(console);

            Console.WriteLine();
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();

            if (patientInfo != null)
            {
                break;
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Personal information");
        Console.WriteLine();
        Console.WriteLine($"ID: {patientInfo.Id} \nName: {patientInfo.Name} \nPhone number: +380{patientInfo.PhoneNumber}");
        Console.WriteLine();
        Console.WriteLine("Press any key to continue ...");
        Console.ReadKey();

        return patientInfo;
    }

    public void ShowHospitalsList()
    {
        Console.Clear();
        Console.WriteLine("List of hospitals:");
        Console.WriteLine();

        foreach (var hospital in _patientService.ShowHospitalsList())
        {
            Console.WriteLine($"ID: {hospital.Id} | Name: {hospital.Name}");
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to continue ...");
        Console.ReadKey();
    }

    public void ShowDoctorsList()
    {
        Console.WriteLine("Please, choose a hospital");
        ShowHospitalsList();
        Console.WriteLine();

        IEnumerable<Doctor>? doctors;

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Type hospital ID");
            Console.WriteLine();
            var console = Console.ReadLine();
            doctors = _patientService.ShowDoctorsList(console);

            if (doctors != null)
            {
                break;
            }
        }
        
        Console.Clear();

        Console.WriteLine("List of doctors:");
        Console.WriteLine();

        foreach (var doctor in doctors)
        {
            Console.WriteLine($"Doctor's ID: {doctor.Id} | Specialization: {doctor.Specialization} | Doctor`s name: {doctor.FullName}");
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to continue ...");
        Console.ReadKey();
    }
    public void ProcessMakingAppointmentWithDoctor()
    {
        var patientAccount = ShowPacientAccountInfo();

        Console.WriteLine();
        Console.WriteLine("Second, you need to choose a hospital");
        Console.WriteLine();
        Console.WriteLine("Press any key to continue ...");
        Console.ReadKey();

        Console.Clear();

        ShowDoctorsList();

        Doctor? doctor;

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Choose your doctor and type its ID");
            Console.WriteLine();

            var console = Console.ReadLine();
            doctor = _patientService.SearchSpecificDoctor(console);
            
            if (doctor != null)
            {
                break;
            }
        }

        Console.WriteLine();
        Console.WriteLine($"You chose {doctor.FullName} | Specialization {doctor.Specialization}");
        Console.WriteLine();
        Console.WriteLine($"Press <Enter> to make an appointment with this doctor or <Escape> to quit the selection and return to main menu");
        Console.WriteLine();

        while (true)
        {
            var console = Console.ReadKey().Key;

            if (console == ConsoleKey.Enter)
            {
                _patientService.ProcessMakingAppointmentWithDoctor(doctor, patientAccount);
                break;
            }
            else if (console == ConsoleKey.Escape)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nThe appointment has been canсeled");
                Console.ResetColor();
                break;
            }
            else
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYou can press only <Enter> or <Escape>");
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to continue ...");
        Console.ReadKey();
    }

    public Patient ShowDoctorsAppointment()
    {
        Console.Clear();

        var patientAccount = ShowPacientAccountInfo();
        var appointments = _patientService.ShowDoctorsAppointment(patientAccount);

        if (appointments != null)
        {
            foreach (var appointment in appointments)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"You have an appointment with {appointment.FullName}, {appointment.Specialization}, personal ID: {appointment.Id}");
                Console.ResetColor();
            }
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to continue ...");
        Console.ReadKey();

        return patientAccount;
    }

    public void CancelDoctorsAppointment()
    {
        Console.Clear();

        var patientAccount = ShowPacientAccountInfo();
        var appointments = _patientService.ShowDoctorsAppointment(patientAccount);

        if (appointments != null)
        {
            bool canceling = false;

            while (true)
            {
                Console.WriteLine();

                foreach (var appointment in appointments)

                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"You have an appointment with {appointment.FullName}, {appointment.Specialization}, personal ID: {appointment.Id}");
                    Console.ResetColor();
                }

                Console.WriteLine();
                Console.WriteLine("Type doctor`s ID to cancel your appointment");
                Console.WriteLine();

                var console = Console.ReadLine();

                if (!canceling)
                {
                    canceling = _patientService.CancelDoctorsAppointment(console, patientAccount);

                    if (canceling)
                    {
                        break;
                    }
                }
            }
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to continue ...");
        Console.ReadKey();
    }

    public void ChangePatientPhoneNumber()
    {
        Console.Clear();
        var patientAccount = ShowPacientAccountInfo();

        while (true)
        {
            string? newPhoneNumber;

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Please, enter your new phone number");
                Console.WriteLine();

                newPhoneNumber = Console.ReadLine();

                if (_patientService.PhoneNumberValidation(newPhoneNumber))
                {
                    break;
                }
            }
            
            Console.WriteLine();
            Console.WriteLine("Press <Enter> to confirm the new phone number, or <Escape> to cancel the changes");

            var consoleKey = Console.ReadKey().Key;

            if (consoleKey == ConsoleKey.Enter)
            {
                _patientService.UpdatePatientPhoneNumber(patientAccount, newPhoneNumber);
                break;
            }
            else if (consoleKey == ConsoleKey.Escape)
            {
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYou can press only <Enter> or <Escape>");
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to continue ...");
        Console.ReadKey();
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

        ReadUserChoiceFromConsole();
    }

    public void ReadUserChoiceFromConsole()
    {
        Console.WriteLine();
        Console.WriteLine("Make your choise and type number");

        while (true)
        {
            Console.WriteLine();
            var console = int.TryParse(Console.ReadLine(), out _userChoiceNumber);
            var menuArray = Enum.GetNames(typeof(MenuList));

            Console.WriteLine();

            if (console == false)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Please, type only one number");
                Console.ResetColor();
            }
            else if (_userChoiceNumber < 1 || _userChoiceNumber > menuArray.Length)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"You can choose a number only from 1 to {menuArray.Length}");
                Console.ResetColor();
            }
            else
            {
                break;
            }
        }

        ChooseAction();
    }

    public void ChooseAction()
    {
        switch ((MenuList)_userChoiceNumber)
        {
            case MenuList.PatientRegistration:
                PatientRegistration();
                break;
            case MenuList.ShowPacientAccountInfo:
                ShowPacientAccountInfo();
                break;
            case MenuList.ShowHospitalsList:
                ShowHospitalsList();
                break;
            case MenuList.ShowDoctorsList:
                ShowDoctorsList();
                break;
            case MenuList.ProcessMakingAppointmentWithDoctor:
                ProcessMakingAppointmentWithDoctor();
                break;
            case MenuList.ShowDoctorsAppointment:
                ShowDoctorsAppointment();
                break;
            case MenuList.CancelDoctorsAppointment:
                CancelDoctorsAppointment();
                break;
            case MenuList.ChangePatientPhoneNumber:
                ChangePatientPhoneNumber();
                break;
            case MenuList.Exit:
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine($"Invalid input, type a number from 1 to 6");
                break;
        }
    }
}
