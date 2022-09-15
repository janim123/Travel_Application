using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations
;

namespace Travel_Application.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Stars { get; set; }
        [Display(Name = "Pet Friendly")]
        public string? PetFriendly { get; set; }
        public string? Spa { get; set; }
        public int? AgencyId { get; set; }
        public Agency? Agency { get; set; }
        public ICollection<HotelCity>? Cities { get; set; }
    }
}
