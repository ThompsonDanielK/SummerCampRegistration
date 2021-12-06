using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Camper
    {
        public string MedicationsCSV { get; set; }

        public string AllergiesCSV { get; set; }

        public string SpecialNeedsCSV { get; set; }

        public int CamperCode { get; set; }

        public int FamilyId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        public List<string> Medications 
        {
            get
            {
                string[] medications = MedicationsCSV.Split(',');
                List<string> medList = new List<string>();

                foreach (var med in medications)
                {
                    medList.Add(med);
                }

                return medList;
            } 
            set
            {

            }
        }

        public List<string> Allergies
        {
            get
            {
                string[] allergies = AllergiesCSV.Split(',');
                List<string> allergyList = new List<string>();

                foreach (var allergy in allergies)
                {
                    allergyList.Add(allergy);
                }

                return allergyList;
            }
            set
            {

            }
        }

        public List<string> SpecialNeeds
        {
            get
            {
                string[] needs = SpecialNeedsCSV.Split(',');
                List<string> needsList = new List<string>();

                foreach (var need in needs)
                {
                    needsList.Add(need);
                }

                return needsList;
            }
            set
            {

            }
        }

        public Camper()
        {

        }
    }
}
