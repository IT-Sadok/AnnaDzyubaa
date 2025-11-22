using Hospitals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hospitals.Repositories
{
    internal class HostRepository : IHostRepository
    {
        private readonly List<Host> _hosts = new List<Host>();

        public void AddHost(Host host)
        {
            _hosts.Add(host);
        }

        public IEnumerable<Host> ShowHostInfo() => _hosts;

        public void AddAppointment(Doctor doctor, Host patientAccount)
        {
            var account = _hosts.SingleOrDefault(a => a.Id == patientAccount.Id);
            account?.doctorsAppointment.Add(doctor);
        }
    }
}
