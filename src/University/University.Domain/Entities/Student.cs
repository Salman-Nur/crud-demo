

namespace University.Domain.Entities
{
    public class Student : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Fees { get; set; }
    }
}
