using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospitals.Models
{
    public class Appointment
    {
        public int AppointmentId;
        public int DoctorsId;
        public DateTime DateAndTime;
        public int? PatientId;
    }
}
