using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Travel_Application.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Travel_Application.Areas.Identity.Data;

namespace Travel_Application.Data
{
    public class Travel_ApplicationContext : IdentityDbContext<Travel_ApplicationUser>
    {
        public Travel_ApplicationContext (DbContextOptions<Travel_ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Travel_Application.Models.Agency>? Agency { get; set; }

        public DbSet<Travel_Application.Models.City>? City { get; set; }

        public DbSet<Travel_Application.Models.Hotel>? Hotel { get; set; }

        public DbSet<Travel_Application.Models.HotelCity>? HotelCity { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<HotelCity>()
            .HasOne<Hotel>(p => p.Hotel)
            .WithMany(p => p.Cities)
            .HasForeignKey(p => p.HotelId);
            //.HasPrincipalKey(p => p.Id);
            builder.Entity<HotelCity>()
            .HasOne<City>(p => p.City)
            .WithMany(p => p.Hotels)
            .HasForeignKey(p => p.CityId);
            //.HasPrincipalKey(p => p.Id);
            builder.Entity<Hotel>()
            .HasOne<Agency>(p => p.Agency)
            .WithMany(p => p.Hotels)
            .HasForeignKey(p => p.AgencyId);
            //.HasPrincipalKey(p => p.Id);

            base.OnModelCreating(builder);

        }

    }
}
