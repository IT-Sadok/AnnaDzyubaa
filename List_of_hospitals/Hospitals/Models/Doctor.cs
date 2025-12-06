namespace Hospitals.Models
{
    public class Doctor
    {
        public int HospitalsId { get; set; }
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Specialization { get; set; }
    }
}