using Hospitals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospitals.Services
{
    public interface IHostService
    {
        public bool NameValidation(string? name);
        public bool PhoneNumberValidation(string? phoneNumber);
        public void AddNewPatient();
        public Host? SearchPatientById(string? id);
        public IEnumerable<Hospital> ShowHospitalsList();
        public IEnumerable<Doctor>? ShowDoctorsList(string? console);
        public Doctor? SearchSpecificDoctor(string? console);
        public void ProcessMakingAppointmentWithDoctor(Doctor doctor, Host patientAccount);
        public IEnumerable<Doctor>? ShowDoctorsAppointment(Host patientAccount);
        public bool CancelDoctorsAppointment(string? console, Host patientAccount);
    }
}
