using Hospitals.Models;
namespace Hospitals.Repositories
{
    public interface IPatientRepository
    {
        public void AddPatient(Patient patient);
        public IEnumerable<Patient> ShowPatientInfo();
    }
}