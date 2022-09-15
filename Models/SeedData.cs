using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travel_Application.Data;

namespace Travel_Application.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Travel_ApplicationContext(
             serviceProvider.GetRequiredService<
             DbContextOptions<Travel_ApplicationContext>>()))
            {

                if (context.Hotel.Any() || context.City.Any() || context.Agency.Any())
                {
                    return; // DB has been seeded
                }
                context.Agency.AddRange(
                new Agency { Name="Savana", Location="str. Orce Nikolov nr.1", PhoneNumber="071 111 111" },
                new Agency { Name = "Fibula", Location = "str. Orce Nikolov nr.2", PhoneNumber = "071 111 222" },
                new Agency { Name = "Atlantis", Location = "str. Orce Nikolov nr.3", PhoneNumber = "071 111 333" },
                new Agency { Name = "Safari", Location = "str. Orce Nikolov nr.4", PhoneNumber = "071 111 444" }, 
                new Agency { Name = "Escape", Location = "str. Orce Nikolov nr.5", PhoneNumber = "071 111 555" }

                );
                context.SaveChanges();


                context.Hotel.AddRange(
                new Hotel { /*Id = 1, */Name = "Marriott", Stars = 5, PetFriendly = "Yes", Spa = "Yes", AgencyId = context.Agency.Single(d => d.Name == "Savana").Id },
                new Hotel { /*Id = 2, */Name = "DoubleTree by Hilton", Stars = 5, PetFriendly = "Yes", Spa = "No", AgencyId = context.Agency.Single(d => d.Name == "Atlantis").Id },
                new Hotel { /*Id = 3, */Name = "Holiday Inn", Stars = 3, PetFriendly = "Yes", Spa = "Yes", AgencyId = context.Agency.Single(d => d.Name == "Safari").Id },
                new Hotel { /*Id = 3, */Name = "Jin Jiang", Stars = 4, PetFriendly = "No", Spa = "Yes", AgencyId = context.Agency.Single(d => d.Name == "Savana").Id },
                new Hotel { /*Id = 3, */Name = "IHG", Stars = 5, PetFriendly = "No", Spa = "Yes", AgencyId = context.Agency.Single(d => d.Name == "Fibula").Id },
                new Hotel { /*Id = 3, */Name = "Alexander Palace", Stars = 1, PetFriendly = "No", Spa = "No", AgencyId = context.Agency.Single(d => d.Name == "Safari").Id },
                new Hotel { /*Id = 3, */Name = "Ceasers Palace", Stars = 2, PetFriendly = "Yes", Spa = "No", AgencyId = context.Agency.Single(d => d.Name == "Escape").Id }
                );
                context.SaveChanges();


                context.City.AddRange(
                new City { /*Id = 1, */Name = "Skopje", Country="Macedonia", PopularLandMark="Milenium Cross"},
                new City { /*Id = 2, */Name = "Ohrid", Country = "Macedonia", PopularLandMark = "Ohrid Lake", },
                new City { /*Id = 3, */Name = "Paris", Country = "France", PopularLandMark = "Eiffel Tower", },
                new City { /*Id = 3, */Name = "Rome", Country = "Italy", PopularLandMark = "Colosseum", },
                new City { /*Id = 3, */Name = "London", Country = "United Kingdom", PopularLandMark = "Buckingham Palace", },
                new City { /*Id = 3, */Name = "Barcelona", Country = "Spain", PopularLandMark = "La Sagrada Familia",},
                new City { /*Id = 3, */Name = "Moscow", Country = "Russia", PopularLandMark = "Red Square", },
                new City { /*Id = 3, */Name = "Berlin", Country = "Germany", PopularLandMark = "Brandenburg Gate", },
                new City { /*Id = 3, */Name = "Istanbul", Country = "Turkey", PopularLandMark = "Hagia Sophia", },
                new City { /*Id = 3, */Name = "Athens", Country = "Greece", PopularLandMark = "Parthenon", },
                new City { /*Id = 3, */Name = "Venice", Country = "Italy", PopularLandMark = "Basilica di San Marco", }
                );
                context.SaveChanges();




                context.HotelCity.AddRange(
                new HotelCity { CityId= context.City.Single(d=>d.Name=="Skopje").Id, HotelId=context.Hotel.Single(d=>d.Name=="Alexander Palace").Id},
                new HotelCity { CityId = context.City.Single(d => d.Name == "Skopje").Id, HotelId = context.Hotel.Single(d => d.Name == "Holiday Inn").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Skopje").Id, HotelId = context.Hotel.Single(d => d.Name == "Marriott").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Skopje").Id, HotelId = context.Hotel.Single(d => d.Name == "DoubleTree by Hilton").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Ohrid").Id, HotelId = context.Hotel.Single(d => d.Name == "Alexander Palace").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Ohrid").Id, HotelId = context.Hotel.Single(d => d.Name == "Ceasers Palace").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Ohrid").Id, HotelId = context.Hotel.Single(d => d.Name == "Holiday Inn").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Paris").Id, HotelId = context.Hotel.Single(d => d.Name == "IHG").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Paris").Id, HotelId = context.Hotel.Single(d => d.Name == "Marriott").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Paris").Id, HotelId = context.Hotel.Single(d => d.Name == "DoubleTree by Hilton").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Paris").Id, HotelId = context.Hotel.Single(d => d.Name == "Jin Jiang").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "London").Id, HotelId = context.Hotel.Single(d => d.Name == "Ceasers Palace").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "London").Id, HotelId = context.Hotel.Single(d => d.Name == "Alexander Palace").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "London").Id, HotelId = context.Hotel.Single(d => d.Name == "DoubleTree by Hilton").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Rome").Id, HotelId = context.Hotel.Single(d => d.Name == "Holiday Inn").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Rome").Id, HotelId = context.Hotel.Single(d => d.Name == "IHG").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Venice").Id, HotelId = context.Hotel.Single(d => d.Name == "Marriott").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Venice").Id, HotelId = context.Hotel.Single(d => d.Name == "Ceasers Palace").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Berlin").Id, HotelId = context.Hotel.Single(d => d.Name == "DoubleTree by Hilton").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Berlin").Id, HotelId = context.Hotel.Single(d => d.Name == "IHG").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Berlin").Id, HotelId = context.Hotel.Single(d => d.Name == "Jin Jiang").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Barcelona").Id, HotelId = context.Hotel.Single(d => d.Name == "Alexander Palace").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Barcelona").Id, HotelId = context.Hotel.Single(d => d.Name == "Holiday Inn").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Barcelona").Id, HotelId = context.Hotel.Single(d => d.Name == "Marriott").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Moscow").Id, HotelId = context.Hotel.Single(d => d.Name == "IHG").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Moscow").Id, HotelId = context.Hotel.Single(d => d.Name == "Jin Jiang").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Istanbul").Id, HotelId = context.Hotel.Single(d => d.Name == "DoubleTree by Hilton").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Istanbul").Id, HotelId = context.Hotel.Single(d => d.Name == "Ceasers Palace").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Istanbul").Id, HotelId = context.Hotel.Single(d => d.Name == "IHG").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Athens").Id, HotelId = context.Hotel.Single(d => d.Name == "Jin Jiang").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Athens").Id, HotelId = context.Hotel.Single(d => d.Name == "Alexander Palace").Id },
                new HotelCity { CityId = context.City.Single(d => d.Name == "Athens").Id, HotelId = context.Hotel.Single(d => d.Name == "Marriott").Id }
                );
                context.SaveChanges();
            }
        }
    }
}