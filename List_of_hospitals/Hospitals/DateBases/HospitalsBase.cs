using Hospitals.Models;

public class HospitalsBase
{
    public List<Hospital> listOfHospitals = new List<Hospital>()
    {
        new Hospital {Id = 101, Name = "Pechersk Hospital",},
        new Hospital {Id = 234, Name = "Holosiivskyi Hospital"},
        new Hospital {Id = 342, Name = "Shevchenko Hospital"},
        new Hospital {Id = 456, Name = "Podolsk Hospital"},
        new Hospital {Id = 509, Name = "Darnytskyi Hospital"}
    };
}
