using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infra.Persistence
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Cinema> Cinemas => Set<Cinema>();
        public DbSet<Endereco> Enderecos => Set<Endereco>();
        public DbSet<Filme> Filmes => Set<Filme>();
        public DbSet<Gerente> Gerentes => Set<Gerente>();
        public DbSet<Sessao> Sessoes => Set<Sessao>();
    }

}
