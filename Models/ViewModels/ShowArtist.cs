using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BalmeetPassion_Project.Models.ViewModels
{
    public class ShowArtist
    {
        public virtual Artist artist { get; set; }
        //a list for every pet they own
        public List<poetry> poetries { get; set; }

        //a SEPARATE list for representing the ADD of an owner to a pet,
        //i.e. show a dropdownlist of all pets, with cta "Add Pet" on Show Owner etc.
        public List<poetry> all_poetries { get; set; }
    }
}