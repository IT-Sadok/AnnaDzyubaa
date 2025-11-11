using Hospitals.Models;

public class DoctorsBase
{
    public List<Doctor> doctors = new List<Doctor>
    {
        new Doctor { HospitalsId = 101, OwnId = 100, FullName = "Kovalchuk Oleh", Specialization = "therapist" },
        new Doctor { HospitalsId = 101, OwnId = 101, FullName = "Shevchenko Inna", Specialization = "cardiologist" },
        new Doctor { HospitalsId = 101, OwnId = 102, FullName = "Bondar Andrii", Specialization = "surgeon" },
        new Doctor { HospitalsId = 234, OwnId = 103, FullName = "Ivanov Dmytro", Specialization = "anesthesiologist" },
        new Doctor { HospitalsId = 234, OwnId = 104, FullName = "Petrenko Kateryna", Specialization = "gastroenterologist" },
        new Doctor { HospitalsId = 234, OwnId = 105, FullName = "Lysenko Mykhailo", Specialization = "pulmonologist" },
        new Doctor { HospitalsId = 342, OwnId = 106, FullName = "Horbach Liliia", Specialization = "rheumatologist" },
        new Doctor { HospitalsId = 342, OwnId = 107, FullName = "Stepanenko Yurii", Specialization = "ophthalmologist" },
        new Doctor { HospitalsId = 342, OwnId = 108, FullName = "Kravets Iryna", Specialization = "gynecologist" },
        new Doctor { HospitalsId = 456, OwnId = 109, FullName = "Soroka Nina", Specialization = "infectious disease specialist" },
        new Doctor { HospitalsId = 456, OwnId = 110, FullName = "Melnychuk Oleh", Specialization = "traumatologist" },
        new Doctor { HospitalsId = 456, OwnId = 111, FullName = "Vovk Anastasiia", Specialization = "family doctor" },
        new Doctor { HospitalsId = 509, OwnId = 112, FullName = "Rybalka Vadym", Specialization = "neurologist" },
        new Doctor { HospitalsId = 509, OwnId = 113, FullName = "Zadorozhna Lesia", Specialization = "endocrinologist" },
        new Doctor { HospitalsId = 509, OwnId = 114, FullName = "Tkachenko Bohdan", Specialization = "cardiothoracic surgeon" },
    };
}