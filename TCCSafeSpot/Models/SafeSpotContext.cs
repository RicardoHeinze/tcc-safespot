using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace TCCSafeSpot.Models
{
    public class SafeSpotContext : DbContext
    {
        public DbSet<CrimeCadastrado> CrimeCadastrado { get; set; }
        public DbSet<CrimeSSP> CrimeSSP { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<TipoCrime> TipoCrime { get; set; }
        public DbSet<Vitima> Vitima { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }

    }
}