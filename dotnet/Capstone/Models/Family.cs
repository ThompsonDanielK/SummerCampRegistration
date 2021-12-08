using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Family
    {
        public int FamilyId { get; set; }

        public string FullName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int Zip { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public Family()
        {

        }
    }
}
