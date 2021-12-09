using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Update
    {
        [Required]
        public int RequestId { get; set; }

        [Required]
        public string FieldToBeChanged { get; set; }

        [Required]
        public int CamperCode { get; set; }

        [Required]
        public int FamilyId { get; set; }

        [Required]
        public string Action { get; set; }

        [Required]
        public string NewData { get; set; }

        public string OldData { get; set; }

        [Required]
        public string Requestor { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime RequestDate { get; set; }

        public DateTime FinalizeDate { get; set; }


        public Update()
        {

        }
    }
}