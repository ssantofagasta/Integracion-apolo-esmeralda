using Microsoft.EntityFrameworkCore;
using WebService.Models;

namespace WebService.Services
{
    public class EsmeraldaContext: DbContext
    {
        public EsmeraldaContext(DbContextOptions<EsmeraldaContext> options): base(options) { }

        public DbSet<users> users { get; set; }
        public DbSet<Patients> patients { get; set; }
        public DbSet<Communes> communes { get; set; }
        public DbSet<demographics> Demographics { get; set; }
        public DbSet<Sospecha> suspect_cases { get; set; }
    }
}
