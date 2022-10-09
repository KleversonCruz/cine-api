using Domain;
using System;
using System.Collections.Generic;

namespace Api.Test.Mocks
{
    internal class GerentesJsonMock : BaseJsonMock
    {
        const string jsonPath = "/Mocks/gerentes.json";
        public GerentesJsonMock() : base(jsonPath) { }

        internal List<MockGerente> GetList()
        {
            return Deserialize<List<MockGerente>>(JsonString);
        }

        internal MockGerente Get(int index)
        {
            return Deserialize<List<MockGerente>>(JsonString)[index];
        }

        internal class MockGerente : Gerente
        {
            public MockGerente(Guid id, string nome) : base(nome)
            {
                Id = id;
            }
        }
    }
}
