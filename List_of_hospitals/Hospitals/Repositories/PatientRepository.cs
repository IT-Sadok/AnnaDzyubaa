using Hospitals.Models;
namespace Hospitals.Repositories
{
    internal class PatientRepository : IPatientRepository
    {
        private readonly List<Patient> _patients = new List<Patient>();

        public void AddPatient(Patient patient)
        {
            _patients.Add(patient);
        }

        public IEnumerable<Patient> ShowPatientInfo() => _patients;
    }
}
