using Hospitals.Models;
using Hospitals.Repositories;

public class HospitalRepository : IHospitalRepository
{
    private readonly List<Hospital> _hospitals = new List<Hospital>()
    {
        new Hospital {Id = 101, Name = "Pechersk Hospital",},
        new Hospital {Id = 234, Name = "Holosiivskyi Hospital"},
        new Hospital {Id = 342, Name = "Shevchenko Hospital"},
        new Hospital {Id = 456, Name = "Podolsk Hospital"},
        new Hospital {Id = 509, Name = "Darnytskyi Hospital"}
    };

    public IEnumerable<Hospital> ShowHospitalsList() => _hospitals;
}