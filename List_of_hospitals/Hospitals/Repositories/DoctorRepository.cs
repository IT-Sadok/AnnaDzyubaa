using Hospitals.DateBases;
using Hospitals.Models;

//Repository(Сховище)
//Цей клас відповідає тільки за зберігання даних (CRUD).
public class DoctorRepository : IDoctorRepository
{
    private readonly List<Doctor> _doctors = new List<Doctor>
    {
        new Doctor { HospitalsId = 101, Id = 100, FullName = "Kovalchuk Oleh", Specialization = "therapist" },
        new Doctor { HospitalsId = 101, Id = 101, FullName = "Shevchenko Inna", Specialization = "cardiologist" },
        new Doctor { HospitalsId = 101, Id = 102, FullName = "Bondar Andrii", Specialization = "surgeon" },
        new Doctor { HospitalsId = 234, Id = 103, FullName = "Ivanov Dmytro", Specialization = "anesthesiologist" },
        new Doctor { HospitalsId = 234, Id = 104, FullName = "Petrenko Kateryna", Specialization = "gastroenterologist" },
        new Doctor { HospitalsId = 234, Id = 105, FullName = "Lysenko Mykhailo", Specialization = "pulmonologist" },
        new Doctor { HospitalsId = 342, Id = 106, FullName = "Horbach Liliia", Specialization = "rheumatologist" },
        new Doctor { HospitalsId = 342, Id = 107, FullName = "Stepanenko Yurii", Specialization = "ophthalmologist" },
        new Doctor { HospitalsId = 342, Id = 108, FullName = "Kravets Iryna", Specialization = "gynecologist" },
        new Doctor { HospitalsId = 456, Id = 109, FullName = "Soroka Nina", Specialization = "infectious disease specialist" },
        new Doctor { HospitalsId = 456, Id = 110, FullName = "Melnychuk Oleh", Specialization = "traumatologist" },
        new Doctor { HospitalsId = 456, Id = 111, FullName = "Vovk Anastasiia", Specialization = "family doctor" },
        new Doctor { HospitalsId = 509, Id = 112, FullName = "Rybalka Vadym", Specialization = "neurologist" },
        new Doctor { HospitalsId = 509, Id = 113, FullName = "Zadorozhna Lesia", Specialization = "endocrinologist" },
        new Doctor { HospitalsId = 509, Id = 114, FullName = "Tkachenko Bohdan", Specialization = "cardiothoracic surgeon" },
    };

    public IEnumerable<Doctor> ShowDoctorsList() => _doctors;
}