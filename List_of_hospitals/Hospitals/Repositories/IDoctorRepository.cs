using Hospitals.Models;
namespace Hospitals.DateBases
{
    public interface IDoctorRepository
    {
        public IEnumerable<Doctor> ShowDoctorsList();
    }
}