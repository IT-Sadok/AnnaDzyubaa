using Hospitals.Models;
namespace Hospitals.Repositories
{
    public interface IHospitalRepository
    {
        public IEnumerable<Hospital> ShowHospitalsList();
    }
}
