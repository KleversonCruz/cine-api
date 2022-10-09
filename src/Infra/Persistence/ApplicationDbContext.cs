using Domain;
using Infra.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infra.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Cinema> Cinemas => Set<Cinema>();
        public DbSet<Endereco> Enderecos => Set<Endereco>();
        public DbSet<Filme> Filmes => Set<Filme>();
        public DbSet<Gerente> Gerentes => Set<Gerente>();
        public DbSet<Sessao> Sessoes => Set<Sessao>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }

}
