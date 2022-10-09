using Application.Common.Interfaces;
using Infra.Persistence;
using Infra.Persistence.Initialization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Test.Mocks
{
    public class TestSeeder : ICustomSeeder
    {
        private readonly ISerializerService _serializerService;
        private readonly ApplicationDbContext _db;

        public TestSeeder(ISerializerService serializerService, ApplicationDbContext db)
        {
            _serializerService = serializerService;
            _db = db;
        }

        public async Task InitializeAsync(CancellationToken cancellationToken)
        {
            if (!_db.Gerentes.Any())
            {
                var gerentes = new GerentesJsonMock().GetList();
                await _db.Gerentes.AddRangeAsync(gerentes, cancellationToken);
            }

            if (!_db.Cinemas.Any())
            {
                var cinemas = new CinemasJsonMock().GetList();
                await _db.Cinemas.AddRangeAsync(cinemas, cancellationToken);
            }

            if (!_db.Filmes.Any())
            {
                var filmes = new FilmesJsonMock().GetList();
                await _db.Filmes.AddRangeAsync(filmes, cancellationToken);
            }

            if (!_db.Sessoes.Any())
            {
                var sessoes = new SessoesJsonMock().GetList();
                await _db.Sessoes.AddRangeAsync(sessoes, cancellationToken);
            }

            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
