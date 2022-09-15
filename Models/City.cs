using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Travel_Application.Models
{
    public class City
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
        [Display (Name="Popular Landmark")]
        public string? PopularLandMark { get; set; }
        public string? Picture   { get; set; }
        public ICollection<HotelCity>? Hotels { get; set; }
    }
}
