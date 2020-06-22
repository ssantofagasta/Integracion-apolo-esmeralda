using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Text;

namespace WebService.Services
{
    public class EsmeraldaContext : DbContext
    {
        public EsmeraldaContext(DbContextOptions<EsmeraldaContext> options) : base(options)
        {

        }

        public DbSet<users> users { get; set; }
        public DbSet<Patients> patients { get; set; }
        public DbSet<Communes> communes { get; set; }
        public DbSet<demographics> Demographics { get; set; }
        public DbSet<Sospecha> suspect_cases { get; set; }
    }
}
