using Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class VisitHistory : Entity
    {
        public DateTime VisitDate { get; set; } = DateTime.Now;
        public Guid DoctorId { get; set; }
    }
}
