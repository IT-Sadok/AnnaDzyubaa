using Hospitals.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

public partial class Menu
{
    public class AppFunctions
    {

        HospitalsBase hospitalBase = new HospitalsBase();
        DoctorsBase doctorsBase = new DoctorsBase();
        Host host = new Host();

        public void PacientRegistration()
        {
            Console.Clear();
            int countOfNumber = 9;

            Console.WriteLine("Enter your name");
            Console.WriteLine();

            host.Name = Console.ReadLine();
            Console.Clear();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Enter your phone number +380 ... (9 numbers)");
                Console.WriteLine();

                var console = Console.ReadLine();

                var count = console?.Length;

                if (Int32.TryParse(console, out int number) && count == countOfNumber)
                {
                    if (number != 0)
                    {
                        host.PhoneNumber = number;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("You should enter 9 digits of your phone number, no more and no less");
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue ...");
                    Console.ReadKey();
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();
        }

        public void ShowPacientAccountInfo()
        {
            Console.Clear();
            Console.WriteLine($"Personal information");
            Console.WriteLine();
            Console.WriteLine($"Name: {host.Name} \nPhone number: +380{host.PhoneNumber}");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();
        }

        public void ShowHospitalsList()
        {
            Console.Clear();
            Console.WriteLine("List of hospitals:");
            Console.WriteLine();

            foreach (var hospital in hospitalBase.listOfHospitals)
            {
                Console.WriteLine($"ID: {hospital.Id} | Name: {hospital.Name}");
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();
        }

        public void ShowDoctorsList()
        {
            Console.WriteLine("Please, choose a hospital");
            ShowHospitalsList();
            Console.WriteLine() ;
            Console.WriteLine("Type hospital ID");

            int number;

            while (true)
            {
                var console = Int32.TryParse(Console.ReadLine(), out number);
                if (!console)
                {
                    Console.WriteLine();
                }else if (!doctorsBase.doctors.Any(i => i.HospitalsId == number))
                {
                    Console.WriteLine();
                }
                else
                {
                    break;
                }
            }

            Console.Clear();

            Console.WriteLine("List of doctors:");
            Console.WriteLine();

            foreach (var doctor in doctorsBase.doctors.Where(p => p.HospitalsId == number))
            {
                Console.WriteLine($"Doctor's ID: {doctor.OwnId} | Specialization: {doctor.Specialization} | Doctor`s name: {doctor.FullName}");
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();
        }

        public void ProcessMakingAppointmentWithDoctor()
        {
            ShowDoctorsList();
            
            Doctor? doctor;
            int number;

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Choose your doctor and type its ID");
                var console = Int32.TryParse(Console.ReadLine(), out number);

                doctor = doctorsBase.doctors.SingleOrDefault(i => i.OwnId == number);

                if (!console)
                {
                    Console.WriteLine("Invalid data entry. Please use numbers only");
                }else if (doctor == null)
                {
                    Console.WriteLine($"ID {number} is not found");
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine();
            Console.WriteLine($"You chose {doctor.FullName} | Specialization {doctor.Specialization}");
            Console.WriteLine();
            Console.WriteLine($"Press <Enter> to make an appointment with this doctor or <Escape> to quit the selection and return to main menu");
            Console.WriteLine();

            while (true)
            {
                var console = Console.ReadKey().Key;

                if (console == ConsoleKey.Enter)
                {
                    host.doctorsAppointment.Add(doctor);
                    Console.WriteLine($"You have an appointment with the doctor {doctor.FullName}");
                    break;
                }
                else if (console == ConsoleKey.Escape)
                {
                    Console.WriteLine("The appointment has been canсeled");
                    break;
                }
                else
                {
                    Console.WriteLine("You can press only <Enter> or <Escape>");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();
        }

        public void ShowDoctorsAppointment()
        {
            Console.Clear();

            if (host.doctorsAppointment.Count == 0)
            {
                Console.WriteLine("You have no appointment with doctor");
            }
            else
            {
                foreach (var appointment in host.doctorsAppointment)
                {
                    Console.WriteLine($"You have an appointment with {appointment.FullName}, {appointment.Specialization}, personal ID: {appointment.OwnId}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();
        }

        public void CancelDoctorsAppointment()
        {
            Console.Clear();
            ShowDoctorsAppointment();

            while (host.doctorsAppointment.Count >= 1)
            {
                Console.WriteLine();
                Console.WriteLine("Type personal doctor`s ID to cancel your appointment");
                Console.WriteLine();

                var console = Int32.TryParse(Console.ReadLine(), out int number);
                Console.WriteLine();

                var appointment = host.doctorsAppointment.FirstOrDefault(i => i.OwnId == number);

                if (console && appointment != null)
                {
                    host.doctorsAppointment.Remove(appointment);
                    Console.WriteLine($"Your appointment with {appointment.FullName} has just canceled");
                }
                else
                {
                    Console.WriteLine($"You don`t have a doctor's appointment with this ID");
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to continue ...");
                Console.ReadKey();
            }
            
        }
    }
}
