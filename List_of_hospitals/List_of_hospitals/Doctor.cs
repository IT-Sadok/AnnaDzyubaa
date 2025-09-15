using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List_of_hospitals
{
    public class Doctor
    {
        private readonly int _id;
        private readonly string _name;

        public Doctor(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public int Id { get { return _id; } }
        public string Name { get { return _name; } }
    }
}
