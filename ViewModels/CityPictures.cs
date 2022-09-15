 using System.ComponentModel.DataAnnotations;
using Travel_Application.Models;
namespace Travel_Application.ViewModels
   
{
    public class CityPictures
    {
        public City? City { get; set; }

        [Display(Name = "Upload")]
        public IFormFile? CityPictureFile { get; set; }

        [Display(Name = "Picture")]
        public string? CityPictureName { get; set; }
    }
}
