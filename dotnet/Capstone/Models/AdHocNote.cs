using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class AdHocNote
    {
        public int NoteId { get; set; }
        public int CamperCode { get; set; }
        public string Parameter { get; set; }
        public string Value { get; set; }
        public string InputType { get; set; }
        public string PossibleValues { get; set; }

    }
}
