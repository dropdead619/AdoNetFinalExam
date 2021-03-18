using Models.Abstract;

namespace Models
{
    public class Doctor : Entity
    {
        public string FullName { get; set; }
        public bool IsFree { get; set; } = false;
        public Schedule Schedule { get; set; }
        public Direction Direction { get; set; }
    }
}