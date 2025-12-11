namespace Hospitals.GeneratorId
{
    public interface IGeneratorId
    {
        public int PatientId { get; set; }
        public int GeneratePatientId();
    }
}
