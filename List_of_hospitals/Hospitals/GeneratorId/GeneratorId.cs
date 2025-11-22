using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospitals.GeneratorId
{
    public class GeneratorId : IGeneratorId
    {
        public int Id { get; set; } = 100;

        public int GenerateId()
        {
            return ++Id;
        }
    }
}
