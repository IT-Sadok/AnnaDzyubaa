using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospitals.GeneratorId
{
    public interface IGeneratorId
    {
        public int Id { get; set; }
        public int GenerateId();
    }
}
