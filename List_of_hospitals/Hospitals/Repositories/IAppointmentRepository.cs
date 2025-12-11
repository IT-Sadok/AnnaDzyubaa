using Hospitals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospitals.Repositories
{
    public interface IAppointmentRepository
    {
        public IEnumerable<Appointment> ShowAppointmentsList();
        public bool AddPatientAppointment(int apointmentId, int patientId);
        public bool RemoveAppointment(int appointmentId);
        public IEnumerable<Appointment>? ShowPatientAppointments(int patientAccount);
    }
}
