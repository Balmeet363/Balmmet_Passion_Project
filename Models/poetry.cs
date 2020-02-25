using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BalmeetPassion_Project.Models
{
    public class poetry
    {

        
            public int poetryID { get; set; }
            public string poetryName { get; set; }
            public string poetryDescription { get; set; }
            public string poetryDate { get; set; }
            public ICollection<Artist> Artists { get; set; }

    }
}
