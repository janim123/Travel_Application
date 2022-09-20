using Microsoft.AspNetCore.Mvc.Rendering;
using Travel_Application.Models;
using System.ComponentModel.DataAnnotations;

namespace Travel_Application.ViewModels
{
    public class EnrollHotels
    {
        public City city { get; set; }
        public IEnumerable<int>? selectedHotels { get; set; }
        public IEnumerable<SelectListItem>? hotelsEnrolledList { get; set; }
    }
}
