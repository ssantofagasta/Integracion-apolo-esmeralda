using Microsoft.EntityFrameworkCore;
using WebService.Models;
using WebService.Request;

namespace WebService.Services
{
    public class EsmeraldaContext: DbContext
    {
        public EsmeraldaContext(DbContextOptions<EsmeraldaContext> options): base(options) { }

        public DbSet<users> users { get; set; }
        public DbSet<Patients> patients { get; set; }
        public DbSet<Communes> communes { get; set; }
        public DbSet<demographics> demographics { get; set; }
        public DbSet<SuspectCase> suspect_cases { get; set; } 
        public DbSet<Establishments> establishments{ get; set; }   
        public DbSet<Laboratories> laboratories { get; set; }        
        public DbSet<Countries> countries { get; set; }        
    }
}
