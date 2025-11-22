using Hospitals;
using Hospitals.GeneratorId;
using Hospitals.Logging;
using Hospitals.Models;
using Hospitals.Repositories;
using Hospitals.Services;
using static Menu;

DoctorRepository doctorRepository = new DoctorRepository();
HospitalRepository hospitalRepository = new HospitalRepository();
HostRepository hostRepository = new HostRepository();
ConsoleLogger logger = new ConsoleLogger();
GeneratorId generatorId = new GeneratorId();

HostService hostService = new HostService(doctorRepository, hospitalRepository, hostRepository, logger, generatorId);
Menu menu = new Menu(hostService);

while (true)
{
    menu.ShowMenu();
}