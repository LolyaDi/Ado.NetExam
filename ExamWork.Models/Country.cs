using System.Collections.Generic;

namespace ExamWork.Models
{
    public class Country: Entity
    {
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
