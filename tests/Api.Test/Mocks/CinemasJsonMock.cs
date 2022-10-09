using Domain;

namespace Api.Test.Mocks
{
    public class CinemasJsonMock : BaseJsonMock
    {
        const string jsonPath = "/Mocks/Cinemas.json";
        public CinemasJsonMock() : base(jsonPath) { }

        internal List<MockCinema> GetList()
        {
            return Deserialize<List<MockCinema>>(JsonString);
        }

        internal MockCinema Get(int index)
        {
            return Deserialize<List<MockCinema>>(JsonString)[index];
        }

        internal class MockCinema : Cinema
        {
            public MockCinema(Guid id, string nome, Endereco endereco, Guid gerenteId) : base(nome)
            {
                Id = id;
                Endereco = endereco;
                GerenteId = gerenteId;
            }

        }
    }
}
