using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Email
    {

        public string Body { get; set; } = "Email Body";

        public string Subject { get; set; } = "Subject";

        public int[] Recipients { get; set; }

        public Email()
        {

        }


    }
}
