using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auer.Models
{
    public class NHTSAMake
    {
        public int Make_ID { get; set; }
        public string Make_Name { get; set; }
    }

    public class AllMakes
    {
        public int Count { get; set; }
        public string Message { get; set; }
        public object SearchCriteria { get; set; }
        public IList<NHTSAMake> Results { get; set; }
    }
}
