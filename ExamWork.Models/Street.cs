using System;

namespace ExamWork.Models
{
    public class Street: Entity
    {
        public string Name { get; set; }

        public Guid CityId { get; set; }
        public City City { get; set; }
    }
}
