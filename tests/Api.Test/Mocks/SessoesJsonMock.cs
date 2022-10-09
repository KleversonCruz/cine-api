using Domain;

namespace Api.Test.Mocks
{
    internal class SessoesJsonMock : BaseJsonMock
    {
        const string jsonPath = "/Mocks/sessoes.json";
        public SessoesJsonMock() : base(jsonPath) { }

        internal List<MockSessao> GetList()
        {
            return Deserialize<List<MockSessao>>(JsonString);
        }

        internal MockSessao Get(int index)
        {
            return Deserialize<List<MockSessao>>(JsonString)[index];
        }

        internal class MockSessao : Sessao
        {
            public MockSessao(Guid id, Guid cinemaId, Guid filmeId, DateTime horarioDeEncerramento) : base(cinemaId, filmeId, horarioDeEncerramento)
            {
                Id = id;
            }
        }
    }
}
