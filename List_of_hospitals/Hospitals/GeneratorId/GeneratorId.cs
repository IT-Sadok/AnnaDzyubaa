namespace Hospitals.GeneratorId
{
    public class GeneratorId : IGeneratorId
    {
        public int PatientId { get; set; } = 100;

        public int GeneratePatientId()
        {
            return ++PatientId;
        }
    }
}