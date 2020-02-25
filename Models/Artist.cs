using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BalmeetPassion_Project.Models
{
    public class Artist
    {
        public int ArtistID { get; set; }
        public string Name { get; set; }
        public string  DOB { get; set; }
        public string  Contact{ get; set; }

        public string Email { get; set; }

        public ICollection<poetry> Poetries { get; set; }
    }
}