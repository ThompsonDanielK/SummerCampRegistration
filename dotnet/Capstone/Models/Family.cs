using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Family
    {
        public int FamilyId { get; set; }

        public string ParentGuardianName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int ZIP { get; set; }

        public int PhoneNumber { get; set; }
        public Family()
        {

        }
    }
}
