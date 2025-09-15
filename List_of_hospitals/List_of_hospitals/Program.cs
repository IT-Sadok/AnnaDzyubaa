using List_of_hospitals;

List<Hospital> listOfHospitals = new List<Hospital>()
{
    new Hospital(101, "Pechersk Hospital"),
    new Hospital(234, "Holosiivskyi Hospital"),
    new Hospital(342, "Shevchenko Hospital"),
    new Hospital(456, "Podolsk Hospital"),
    new Hospital(509, "Darnytskyi Hospital")
};

ShowHospitalsList();

void ShowHospitalsList()
{
    foreach (Hospital IdAndHospital in listOfHospitals)
    {
        Console.WriteLine($"ID: {IdAndHospital.Id}, Name: {IdAndHospital.HospitalName}");
    }
}

List<Doctor> doctors = new List<Doctor>() 
{
    new Doctor(101, "Kovalchuk Oleh, therapist"),
    new Doctor(101, "Shevchenko Inna, cardiologist"),
    new Doctor(101, "Bondar Andrii, surgeon"),
    new Doctor(234, "Ivanov Dmytro, anesthesiologist"),
    new Doctor(234, "Petrenko Kateryna, gastroenterologist"),
    new Doctor(234, "Lysenko Mykhailo, pulmonologist"),
    new Doctor(342, "Horbach Liliia, rheumatologist"),
    new Doctor(342, "Stepanenko Yurii, ophthalmologist"),
    new Doctor(342, "Kravets Iryna, gynecologist"),
    new Doctor(456, "Soroka Nina, infectious disease specialist"),
    new Doctor(456, "Melnychuk Oleh, traumatologist"),
    new Doctor(456, "Vovk Anastasiia, family doctor"),
    new Doctor(509, "Rybalka Vadym, neurologist"),
    new Doctor(509, "Zadorozhna Lesia, endocrinologist"),
    new Doctor(509, "Tkachenko Bohdan, cardiothoracic surgeon"),
};

Console.WriteLine();
Console.WriteLine("Type hospital ID\n");

var console = Console.ReadLine();
ArgumentException.ThrowIfNullOrWhiteSpace(console);

bool success = (int.TryParse(console, out var number));

Console.WriteLine();

if (success == false)
{
    Console.WriteLine("Invalid data entry. Please use numbers only\n");
    Environment.Exit(0);
}

if (!doctors.Any(i => i.Id == number))
{
    Console.WriteLine("ID is not found");
}

foreach (Doctor doctor in doctors.Where(p => p.Id == number))
{
    Console.WriteLine("Doctor`s name: " + doctor.Name);
}
