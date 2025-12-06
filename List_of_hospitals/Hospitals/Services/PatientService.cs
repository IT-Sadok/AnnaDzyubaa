using Hospitals.DateBases;
using Hospitals.GeneratorId;
using Hospitals.Logging;
using Hospitals.Models;
using Hospitals.Repositories;

namespace Hospitals.Services
{
    public class PatientService(IDoctorRepository doctorRepository, IHospitalRepository hospitalRepository, IPatientRepository hostRepository, ILogger logger, IGeneratorId generatorId) : IPatientService
    {
        private readonly IDoctorRepository _doctorRepository = doctorRepository;
        private readonly IHospitalRepository _hospitalRepository = hospitalRepository;
        private readonly IPatientRepository _hostRepository = hostRepository;
        private readonly ILogger _logger = logger;
        private readonly IGeneratorId _generatorId = generatorId;

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
            var host = new Patient
            {
                Id = _generatorId.GenerateId(),
                Name = _hostName,
                PhoneNumber = _phoneNumber
            };

            _hostRepository.AddPatient(host);
            _logger.LogInfo($"Patient account has created successfully. Your ID: {_generatorId.Id}");
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
                Patient? host = _hostRepository.ShowPatientInfo().FirstOrDefault(h => h.Id == number);
                if (host != null)
                {
                    return host;
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

            var appointment = patientAccount.doctorsAppointment.FirstOrDefault(i => i.Id == number);

            if (appointment != null)
            {
                patientAccount.doctorsAppointment.Remove(appointment);
                _logger.LogInfo($"Your appointment with {appointment.FullName} has just canceled");
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

        public void ProcessMakingAppointmentWithDoctor(Doctor doctor, Patient patientAccount)
        {
            _hostRepository.AddAppointment(doctor, patientAccount);
            _logger.LogInfo($"You have an appointment with the doctor {doctor.FullName}");
        }

        public IEnumerable<Doctor>? ShowDoctorsAppointment(Patient patientAccount)
        {
            if (patientAccount.doctorsAppointment.Count == 0)
            {
                _logger.LogInfo("You have no appointment with doctor");
                return null;
            }
            else
            {
                return patientAccount.doctorsAppointment;
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
    }
}
