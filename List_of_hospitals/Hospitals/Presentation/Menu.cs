using Hospitals;
using Hospitals.DateBases;
using Hospitals.Models;
using Hospitals.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Xml.Linq;

public class Menu
{
    private int _UserChoiceNumber;
    private IHostService _hostService;

    public Menu(IHostService hostService)
    {
        _hostService = hostService;
    }

    public void PatientRegistration()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("Enter your name");
            Console.WriteLine();

            string? name = Console.ReadLine();

            var validation = _hostService.NameValidation(name);

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

            bool validation = _hostService.PhoneNumberValidation(phoneNumber);

            Console.WriteLine();
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();

            if (validation)
            {
                break;
            }
        }

        _hostService.AddNewPatient();

        Console.WriteLine();
        Console.WriteLine("Press any key to continue ...");
        Console.ReadKey();
    }

    public Host ShowPacientAccountInfo()
    {
        Host? hostInfo;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Type patient ID to find its information");
            Console.WriteLine();

            var console = Console.ReadLine();
            hostInfo = _hostService.SearchPatientById(console);

            Console.WriteLine();
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();

            if (hostInfo != null)
            {
                break;
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Personal information");
        Console.WriteLine();
        Console.WriteLine($"ID: {hostInfo.Id} \nName: {hostInfo.Name} \nPhone number: +380{hostInfo.PhoneNumber}");
        Console.WriteLine();
        Console.WriteLine("Press any key to continue ...");
        Console.ReadKey();

        return hostInfo;
    }

    public void ShowHospitalsList()
    {
        Console.Clear();
        Console.WriteLine("List of hospitals:");
        Console.WriteLine();

        foreach (var hospital in _hostService.ShowHospitalsList())
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
            doctors = _hostService.ShowDoctorsList(console);

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
            doctor = _hostService.SearchSpecificDoctor(console);
            
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
                _hostService.ProcessMakingAppointmentWithDoctor(doctor, patientAccount);
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

    public Host ShowDoctorsAppointment()
    {
        Console.Clear();

        var patientAccount = ShowPacientAccountInfo();
        var appointments = _hostService.ShowDoctorsAppointment(patientAccount);

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
        var appointments = _hostService.ShowDoctorsAppointment(patientAccount);

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
                    canceling = _hostService.CancelDoctorsAppointment(console, patientAccount);

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
            var console = int.TryParse(Console.ReadLine(), out _UserChoiceNumber);
            var menuArray = Enum.GetNames(typeof(MenuList));

            Console.WriteLine();

            if (console == false)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Please, type only one number");
                Console.ResetColor();
            }
            else if (_UserChoiceNumber < 1 || _UserChoiceNumber > menuArray.Length)
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
        switch ((MenuList)_UserChoiceNumber)
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
            case MenuList.Exit:
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine($"Invalid input, type a number from 1 to 6");
                break;
        }
    }
}
