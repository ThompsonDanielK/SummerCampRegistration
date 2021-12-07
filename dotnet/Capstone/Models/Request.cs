using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Request
    {
        public int RequestId { get; set; }

        public int CamperCode { get; set; }

        public List<RequestLineItem> Items { get; set; }

        public int RequestedBy { get; set; }
        public Request()
        {

        }
              
    }

}
