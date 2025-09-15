using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List_of_hospitals
{
    public class Hospital
    {
        private readonly int _id;
        private readonly string _hospitalName;

        public Hospital(int id, string hospitalName)
        {
            _id = id;
            _hospitalName = hospitalName;
        }

        public int Id 
        { 
            get { return _id; } 
        }

        public string HospitalName
        {
            get { return _hospitalName; }
        }

    }
}

