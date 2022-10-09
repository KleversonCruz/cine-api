using Domain;

namespace Api.Test.Mocks
{
    public class FilmesJsonMock : BaseJsonMock
    {
        const string jsonPath = "/Mocks/Filmes.json";
        public FilmesJsonMock() : base(jsonPath) { }

        internal List<MockFilme> GetList()
        {
            return Deserialize<List<MockFilme>>(JsonString);
        }

        internal MockFilme Get(int index)
        {
            return Deserialize<List<MockFilme>>(JsonString)[index];
        }

        internal class MockFilme : Filme
        {
            public MockFilme(Guid id, string titulo, int duracao, string diretor, string genero, int classificacaoEtaria) : base(titulo, duracao, diretor, genero, classificacaoEtaria)
            {
                Id = id;
            }
        }
    }
}
