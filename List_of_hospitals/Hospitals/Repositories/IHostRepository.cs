using Hospitals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospitals.Repositories
{
    public interface IHostRepository
    {
        public void AddHost(Host host);
        public IEnumerable<Host> ShowHostInfo();
        public void AddAppointment(Doctor doctor, Host patientAccount);
    }
}