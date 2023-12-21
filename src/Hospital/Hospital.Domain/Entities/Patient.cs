using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Entities
{
    public class Patient : IEntity<Guid>
    {
        public Guid Id {  get; set; }
        public string Name { get; set; }
        public double Bill { get; set; }
    }
}
