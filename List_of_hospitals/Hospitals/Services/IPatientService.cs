using Hospitals.Models;
namespace Hospitals.Services
{
    public interface IPatientService
    {
        public bool NameValidation(string? name);
        public bool PhoneNumberValidation(string? phoneNumber);
        public void AddNewPatient();
        public Patient? SearchPatientById(string? id);
        public IEnumerable<Hospital> ShowHospitalsList();
        public IEnumerable<Doctor>? ShowDoctorsList(string? console);
        public Doctor? SearchSpecificDoctor(string? console);
        public void ProcessMakingAppointmentWithDoctor(Doctor doctor, Patient patientAccount);
        public IEnumerable<Doctor>? ShowDoctorsAppointment(Patient patientAccount);
        public bool CancelDoctorsAppointment(string? console, Patient patientAccount);
        public void UpdatePatientPhoneNumber(Patient patientAccount, string? newPhoneNumber);
    }
}
