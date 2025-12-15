using Hospitals.GeneratorId;
using Hospitals.Logging;
using Hospitals.Models;
using Hospitals.Repositories;
using Hospitals.Services;

DoctorRepository doctorRepository = new ();
HospitalRepository hospitalRepository = new ();
PatientRepository patientRepository = new ();
ConsoleLogger logger = new ();
GeneratorId generatorId = new ();
AppointmentRepository appointmentRepository = new ();

PatientService patientService = new (doctorRepository, hospitalRepository, patientRepository, logger, generatorId, appointmentRepository);
Menu menu = new (patientService);

while (true)
{
    await menu.ShowMenuAsync();
}