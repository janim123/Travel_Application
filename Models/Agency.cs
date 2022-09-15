using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Travel_Application.Models
{
    public class Agency
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Location  { get; set; }
        [Display(Name="Phone Number")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Agency URL")]
        public string? AgencyUrl { get; set; }
        public ICollection<Hotel>? Hotels { get; set; }
    }
}
