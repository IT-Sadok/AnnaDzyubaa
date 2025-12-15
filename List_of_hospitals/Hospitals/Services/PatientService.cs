using Hospitals.DateBases;
using Hospitals.GeneratorId;
using Hospitals.Logging;
using Hospitals.Models;
using Hospitals.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hospitals.Services
{
    public class PatientService(IDoctorRepository doctorRepository, IHospitalRepository hospitalRepository, IPatientRepository patientRepository, ILogger logger, IGeneratorId generatorId, IAppointmentRepository appointmentRepository) : IPatientService
    {
        private readonly IDoctorRepository _doctorRepository = doctorRepository;
        private readonly IHospitalRepository _hospitalRepository = hospitalRepository;
        private readonly IPatientRepository _patientRepository = patientRepository;
        private readonly ILogger _logger = logger;
        private readonly IGeneratorId _generatorId = generatorId;
        private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;

        private string? _hostName;
        private int _phoneNumber;
        private const int CountOfNumber = 9;



        public bool NameValidation(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                _logger.LogWarning("Name shouldn`t be empty. Please, type again");
                return false;
            }

            _hostName = name;
            return true;
        }

        public bool PhoneNumberValidation(string? phoneNumber)
        {
            var count = phoneNumber?.Length;

            if (Int32.TryParse(phoneNumber, out int number) && count == CountOfNumber)
            {
                if (number != 0)
                {
                    _phoneNumber = number;
                    return true;
                }
            }

            _logger.LogError("You should enter 9 digits of your phone number, no more and no less");
            return false;
        }

        public void AddNewPatient()
        {
            var patient = new Patient
            {
                Id = _generatorId.GeneratePatientId(),
                Name = _hostName,
                PhoneNumber = _phoneNumber
            };

            _patientRepository.AddPatient(patient);
            _logger.LogInfo($"Patient account has created successfully. Your ID: {patient.Id}");
        }

        public Patient? SearchPatientById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogError("ID cannot be empty. Please, type again");
                return null;
            }

            if (Int32.TryParse(id, out int number))
            {
                Patient? patient = _patientRepository.ShowPatientInfo().FirstOrDefault(h => h.Id == number);
                if (patient != null)
                {
                    return patient;
                }
                else
                {
                    _logger.LogWarning("ID not found");
                    return null;
                }
            }
            else
            {
                _logger.LogError("Plese, use only numbers");
                return null;
            }
        }

        public bool CancelDoctorsAppointment(string? console, Patient patientAccount)
        {
            if (string.IsNullOrWhiteSpace(console))
            {
                _logger.LogError("ID not entered. Please, try again");
                return false;
            }

            if (!Int32.TryParse(console, out int number))
            {
                _logger.LogError("Plese, use only numbers");
                return false;
            }

            var appointment = _appointmentRepository.RemoveAppointment(number);

            if (appointment)
            {
                _logger.LogInfo($"Your appointment with ID {number} has just canceled");
                return true;
            }
            else
            {
                _logger.LogWarning($"You don`t have a doctor's appointment with this ID. Please, try enter ID again");
                return false;
            }
        }

        public Doctor? SearchSpecificDoctor(string? console)
        {
            if (string.IsNullOrWhiteSpace(console))
            {
                _logger.LogError("ID not entered. Please, try again");
                return null;
            }

            if (!Int32.TryParse(console, out int number))
            {
                _logger.LogError("Invalid data entry. Please, use numbers only");
                return null;
            }

            var doctor = _doctorRepository.ShowDoctorsList().SingleOrDefault(i => i.Id == number);

            if (doctor == null)
            {
                _logger.LogWarning($"ID {number} is not found");
            }

            return doctor;
        }

        public bool ProcessMakingAppointmentWithDoctor(string? console, int patientId)
        {
            if (string.IsNullOrWhiteSpace(console))
            {
                _logger.LogWarning("ID can`t be empty. Please, type again");
                return false;
            }

            if (Int32.TryParse(console, out int apointmentId))
            {
                if (_appointmentRepository.AddPatientAppointment(apointmentId, patientId))
                {
                    return true;
                }
                else
                {
                    _logger.LogWarning($"ID {console} is not found. Please, try again");
                    return false;
                }
            }
            else
            {
                _logger.LogError("Invalid data entry. Please, use numbers only");
                return false;
            }
        }

        public IEnumerable<Appointment>? ShowDoctorsAppointment(int patientId)
        {
            var appointments = _appointmentRepository.ShowPatientAppointments(patientId);

            if (appointments == null)
            {
                _logger.LogInfo("You have no appointment with doctor");
                return null;
            }
            else
            {
                return appointments;
            }
        }

        public IEnumerable<Doctor>? ShowDoctorsList(string? console)
        {
            if (string.IsNullOrWhiteSpace(console))
            {
                _logger.LogError("ID not entered. Please, try again");
                return null;
            }

            if (!Int32.TryParse(console, out int number))
            {
                _logger.LogError("Please, use only numbers");
                return null;
            }

            var doctors = _doctorRepository.ShowDoctorsList().Where(i => i.HospitalsId == number);

            if (doctors.Count() == 0)
            {
                _logger.LogWarning("No doctors found with this ID. Please enter a different ID");
                return null;
            }

            return doctors;
        }

        public void UpdatePatientPhoneNumber(Patient patientAccount, string? newPhoneNumber)
        {
            patientAccount.PhoneNumber = _phoneNumber;
            _logger.LogInfo("Your phone number has been successfully changed");
        }

        public IEnumerable<Hospital> ShowHospitalsList() => _hospitalRepository.ShowHospitalsList();

        public IEnumerable<Appointment> ShowAvailableAppointmentsOfSpecifiedDoctor(Doctor doctor)
        {
            List<Appointment>? appointmentsTime = new ();

            foreach (var appointment in _appointmentRepository.ShowAppointmentsList()
                .Where(a => a.DoctorsId == doctor.Id)
                .OrderBy(a => a.DateAndTime))
            {
                appointmentsTime.Add(appointment);
            }

            return appointmentsTime;
        }
    }
}
