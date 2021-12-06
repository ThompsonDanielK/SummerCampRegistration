using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Camper
    {
        public int CamperCode { get; set; }

        public int FamilyId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        public List<string> Medications { get; set; }

        public List<string> Allergies { get; set; }

        public List<string> SpecialNeeds { get; set; }

        public Camper()
        {

        }
    }
}
