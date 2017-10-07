using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace HoustonCrimeImporter.Models
{
    public class HoustonCrimeContext : DbContext
    {
        public HoustonCrimeContext(): base("name=SODADBConnectionString")
        {
            
        }
        public DbSet<Incident> Incidents { get; set; }
        
    }
}
