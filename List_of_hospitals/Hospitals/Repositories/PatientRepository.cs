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

        public void AddAppointment(Doctor doctor, Patient patientAccount)
        {
            var account = _patients.SingleOrDefault(a => a.Id == patientAccount.Id);
            account?.doctorsAppointment.Add(doctor);
        }
    }
}
