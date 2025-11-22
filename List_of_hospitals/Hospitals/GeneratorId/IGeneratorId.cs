namespace Hospitals.GeneratorId
{
    public interface IGeneratorId
    {
        public int Id { get; set; }
        public int GenerateId();
    }
}
