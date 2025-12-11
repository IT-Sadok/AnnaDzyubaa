using Hospitals.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospitals.DateBases;
using Hospitals.GeneratorId;

namespace Hospitals.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly List<Appointment> _appointments = new List<Appointment>
        {
            new Appointment { AppointmentId = 100, DoctorsId = 100, DateAndTime = DateTime.Today.AddDays(1).AddHours(13), PatientId = null },
            new Appointment { AppointmentId = 101, DoctorsId = 100, DateAndTime = DateTime.Today.AddDays(2).AddHours(14), PatientId = null },

            new Appointment { AppointmentId = 102, DoctorsId = 101, DateAndTime = DateTime.Today.AddDays(1).AddHours(13), PatientId = null },
            new Appointment { AppointmentId = 103, DoctorsId = 101, DateAndTime = DateTime.Today.AddDays(2).AddHours(14), PatientId = null },

            new Appointment { AppointmentId = 104, DoctorsId = 102, DateAndTime = DateTime.Today.AddDays(1).AddHours(13), PatientId = null },
            new Appointment { AppointmentId = 105, DoctorsId = 102, DateAndTime = DateTime.Today.AddDays(2).AddHours(14), PatientId = null },

            new Appointment { AppointmentId = 106, DoctorsId = 103, DateAndTime = DateTime.Today.AddDays(1).AddHours(13), PatientId = null },
            new Appointment { AppointmentId = 107, DoctorsId = 103, DateAndTime = DateTime.Today.AddDays(2).AddHours(14), PatientId = null },

            new Appointment { AppointmentId = 108, DoctorsId = 104, DateAndTime = DateTime.Today.AddDays(1).AddHours(13), PatientId = null },
            new Appointment { AppointmentId = 109, DoctorsId = 104, DateAndTime = DateTime.Today.AddDays(2).AddHours(14), PatientId = null },

            new Appointment { AppointmentId = 110, DoctorsId = 105, DateAndTime = DateTime.Today.AddDays(1).AddHours(13), PatientId = null },
            new Appointment { AppointmentId = 111, DoctorsId = 105, DateAndTime = DateTime.Today.AddDays(2).AddHours(14), PatientId = null },

            new Appointment { AppointmentId = 112, DoctorsId = 106, DateAndTime = DateTime.Today.AddDays(1).AddHours(13), PatientId = null },
            new Appointment { AppointmentId = 113, DoctorsId = 106, DateAndTime = DateTime.Today.AddDays(2).AddHours(14), PatientId = null },

            new Appointment { AppointmentId = 114, DoctorsId = 107, DateAndTime = DateTime.Today.AddDays(1).AddHours(13), PatientId = null },
            new Appointment { AppointmentId = 115, DoctorsId = 107, DateAndTime = DateTime.Today.AddDays(2).AddHours(14), PatientId = null },

            new Appointment { AppointmentId = 116, DoctorsId = 108, DateAndTime = DateTime.Today.AddDays(1).AddHours(13), PatientId = null },
            new Appointment { AppointmentId = 117, DoctorsId = 108, DateAndTime = DateTime.Today.AddDays(2).AddHours(14), PatientId = null },

            new Appointment { AppointmentId = 118, DoctorsId = 109, DateAndTime = DateTime.Today.AddDays(1).AddHours(13), PatientId = null },
            new Appointment { AppointmentId = 119, DoctorsId = 109, DateAndTime = DateTime.Today.AddDays(2).AddHours(14), PatientId = null },

            new Appointment { AppointmentId = 120, DoctorsId = 110, DateAndTime = DateTime.Today.AddDays(1).AddHours(13), PatientId = null },
            new Appointment { AppointmentId = 121, DoctorsId = 110, DateAndTime = DateTime.Today.AddDays(2).AddHours(14), PatientId = null },

            new Appointment { AppointmentId = 122, DoctorsId = 111, DateAndTime = DateTime.Today.AddDays(1).AddHours(13), PatientId = null },
            new Appointment { AppointmentId = 123, DoctorsId = 111, DateAndTime = DateTime.Today.AddDays(2).AddHours(14), PatientId = null },

            new Appointment { AppointmentId = 124, DoctorsId = 112, DateAndTime = DateTime.Today.AddDays(1).AddHours(13), PatientId = null },
            new Appointment { AppointmentId = 125, DoctorsId = 112, DateAndTime = DateTime.Today.AddDays(2).AddHours(14), PatientId = null },

            new Appointment { AppointmentId = 126, DoctorsId = 113, DateAndTime = DateTime.Today.AddDays(1).AddHours(13), PatientId = null },
            new Appointment { AppointmentId = 127, DoctorsId = 113, DateAndTime = DateTime.Today.AddDays(2).AddHours(14), PatientId = null },

            new Appointment { AppointmentId = 128, DoctorsId = 114, DateAndTime = DateTime.Today.AddDays(1).AddHours(13), PatientId = null },
            new Appointment { AppointmentId = 129, DoctorsId = 114, DateAndTime = DateTime.Today.AddDays(2).AddHours(14), PatientId = null },
        };

        public IEnumerable<Appointment> ShowAppointmentsList() => _appointments;

        public IEnumerable<Appointment>? ShowPatientAppointments(int patientAccount)
        {
            List<Appointment> existingAppointments = new();

            foreach(var appointment in _appointments.Where(a => a.PatientId == patientAccount))
            {
                existingAppointments.Add(appointment);
            }
            if (existingAppointments.Count == 0)
            {
                return null;
            }
            else
            {
                return existingAppointments;
            }
        } 

        public bool AddPatientAppointment(int apointmentId, int patientId)
        {
            var patientAppointment = _appointments.SingleOrDefault(a => a.AppointmentId == apointmentId);
            if (patientAppointment != null)
            {
                if (patientAppointment.PatientId == null)
                {
                    Thread.Sleep(20);

                    patientAppointment.PatientId = patientId;
                    return true;
                }
            }

            return false;
        }

        public bool RemoveAppointment(int appointmentId)
        {
            var canceledAppointment = _appointments.SingleOrDefault(a => a.AppointmentId == appointmentId);
            if (canceledAppointment != null)
            {
                canceledAppointment.PatientId = null;
                return true;
            }

            return false;
        }
    };
}
