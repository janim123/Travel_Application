namespace Travel_Application.Models
{
    public class HotelCity
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public City? City { get; set; }
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }

    }
}
