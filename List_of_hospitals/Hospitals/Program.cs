using Hospitals;

var listOfHospitals = new List<Hospital>()
{
    new Hospital {Id = 101, Name = "Pechersk Hospital"},
    new Hospital {Id = 234, Name = "Holosiivskyi Hospital"},
    new Hospital {Id = 342, Name = "Shevchenko Hospital"},
    new Hospital {Id = 456, Name = "Podolsk Hospital"},
    new Hospital {Id = 509, Name = "Darnytskyi Hospital"}
};

ShowHospitalsList();

void ShowHospitalsList()
{
    foreach (var hospital in listOfHospitals)
    {
        Console.WriteLine($"ID: {hospital.Id}, Name: {hospital.Name}");
    }
}

var doctors = new List<Doctor>()
{
    new Doctor {Id = 101, Name = "Kovalchuk Oleh, therapist"},
    new Doctor {Id = 101, Name = "Shevchenko Inna, cardiologist"},
    new Doctor {Id = 101, Name = "Bondar Andrii, surgeon"},
    new Doctor {Id = 234, Name = "Ivanov Dmytro, anesthesiologist"},
    new Doctor {Id = 234, Name = "Petrenko Kateryna, gastroenterologist"},
    new Doctor {Id = 234, Name = "Lysenko Mykhailo, pulmonologist"},
    new Doctor {Id = 342, Name = "Horbach Liliia, rheumatologist"},
    new Doctor {Id = 342, Name = "Stepanenko Yurii, ophthalmologist"},
    new Doctor {Id = 342, Name = "Kravets Iryna, gynecologist"},
    new Doctor {Id = 456, Name = "Soroka Nina, infectious disease specialist"},
    new Doctor {Id = 456, Name = "Melnychuk Oleh, traumatologist"},
    new Doctor {Id = 456, Name = "Vovk Anastasiia, family doctor"},
    new Doctor {Id = 509, Name = "Rybalka Vadym, neurologist"},
    new Doctor {Id = 509, Name = "Zadorozhna Lesia, endocrinologist"},
    new Doctor {Id = 509, Name = "Tkachenko Bohdan, cardiothoracic surgeon"},
}; ;

Console.WriteLine();
Console.WriteLine("Type hospital ID\n");

var console = Console.ReadLine();
ArgumentException.ThrowIfNullOrWhiteSpace(console);

var success = (int.TryParse(console, out var number));

Console.WriteLine();

if (!success)
{
    Console.WriteLine("Invalid data entry. Please use numbers only\n");
    Environment.Exit(0);
}

if (!doctors.Any(i => i.Id == number))
{
    Console.WriteLine("ID is not found");
}

foreach (var doctor in doctors.Where(p => p.Id == number))
{
    Console.WriteLine("Doctor`s name: " + doctor.Name);
}
