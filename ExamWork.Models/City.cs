using System;
using System.Collections.Generic;

namespace ExamWork.Models
{
    public class City: Entity
    {
        public string Name { get; set; }
        
        public Guid CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<Street> Streets { get; set; }
    }
}
