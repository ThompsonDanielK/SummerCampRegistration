using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class RequestLineItem
    {
        public string FieldToBeChanged { get; set; }

        public string NewData { get; set; }

        public string OldData { get; set; }

        public RequestLineItem()
        {

        }
    }
}
