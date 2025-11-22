using Hospitals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospitals.DateBases
{
    public interface IDoctorRepository
    {
        public IEnumerable<Doctor> ShowDoctorsList();
    }
}