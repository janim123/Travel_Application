using Microsoft.AspNetCore.Mvc.Rendering;
using Travel_Application.Models;

namespace Travel_Application.ViewModels
{
    public class FilterCity
    {
        public IList<City> cities { get; set; }
       
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
