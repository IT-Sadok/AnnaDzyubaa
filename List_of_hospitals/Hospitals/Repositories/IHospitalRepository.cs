using Hospitals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospitals.Repositories
{
    public interface IHospitalRepository
    {
        public IEnumerable<Hospital> ShowHospitalsList();
    }
}
