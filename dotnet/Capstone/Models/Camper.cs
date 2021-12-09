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

        public string Medications { get; set; }

        public string Allergies { get; set; }

        public string SpecialNeeds { get; set; }

        public string Registrar { get; set; }

        public bool PaymentStatus { get; set; } = false;

        public DateTime DateAdded { get; set; }



        public List<string> MedicationsList 
        {
            get
            {
                string[] medications = Medications.Split(',');
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

        public List<string> AllergiesList
        {
            get
            {
                string[] allergies = Allergies.Split(',');
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

        public List<string> SpecialNeedsList
        {
            get
            {
                string[] needs = SpecialNeeds.Split(',');
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
