using Hospitals.GeneratorId;
using Hospitals.Logging;
using Hospitals.Repositories;
using Hospitals.Services;

DoctorRepository doctorRepository = new DoctorRepository();
HospitalRepository hospitalRepository = new HospitalRepository();
PatientRepository patientRepository = new PatientRepository();
ConsoleLogger logger = new ConsoleLogger();
GeneratorId generatorId = new GeneratorId();

PatientService patientService = new PatientService(doctorRepository, hospitalRepository, patientRepository, logger, generatorId);
Menu menu = new Menu(patientService);

while (true)
{
    menu.ShowMenu();
}