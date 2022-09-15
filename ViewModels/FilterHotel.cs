using global::Travel_Application.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Travel_Application.ViewModels
{
    public class FilterHotel
    {
        public IList<Hotel> hotels { get; set; }
        public SelectList stars { get; set; }
        public string Name { get; set; }    
        public int star { get; set; }
    }
}