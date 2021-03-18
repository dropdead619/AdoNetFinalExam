using Models.Abstract;
using System;

namespace Models
{
    public class Schedule : Entity
    {
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; } = DateTime.Now.AddHours(8);
    }
}