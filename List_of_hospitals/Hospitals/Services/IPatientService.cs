using Hospitals.Models;
using System.Numerics;
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
        public bool ProcessMakingAppointmentWithDoctor(string? console, int patientId);
        public IEnumerable<Appointment>? ShowDoctorsAppointment(int patientId);
        public bool CancelDoctorsAppointment(string? console, Patient patientAccount);
        public void UpdatePatientPhoneNumber(Patient patientAccount, string? newPhoneNumber);
        public IEnumerable<Appointment> ShowAvailableAppointmentsOfSpecifiedDoctor(Doctor doctor);
    }
}
