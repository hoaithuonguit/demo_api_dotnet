using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Room_Model
    {
        public int id { get; set; }
        public String Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime BookedDate { get; set; }
        public String BookedPerson { get; set; }
    }
}