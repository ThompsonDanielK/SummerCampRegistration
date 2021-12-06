using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Update
    {
        public int RequestId { get; set; }

        public string FieldToBeChanged { get; set; }

        public int CamperCode { get; set; }

        public Update()
        {

        }
    }
}
