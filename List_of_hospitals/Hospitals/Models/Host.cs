using Hospitals.Models;

public class Host
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? PhoneNumber { get; set; }

    public List<Doctor> doctorsAppointment = new List<Doctor>();
}