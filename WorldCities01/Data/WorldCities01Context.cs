using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorldCities01.Models;

namespace WorldCities01.Data
{
    public class WorldCities01Context : DbContext
    {
        public WorldCities01Context (DbContextOptions<WorldCities01Context> options)
            : base(options)
        {
        }

        public DbSet<WorldCities01.Models.City> City { get; set; } = default!;
    }
}
