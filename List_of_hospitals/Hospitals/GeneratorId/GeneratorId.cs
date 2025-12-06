namespace Hospitals.GeneratorId
{
    public class GeneratorId : IGeneratorId
    {
        public int Id { get; set; } = 100;

        public int GenerateId()
        {
            return ++Id;
        }
    }
}
