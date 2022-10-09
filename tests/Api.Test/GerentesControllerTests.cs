using Api.Test.Mocks;
using Api.Test.Utils;
using Application.Gerentes.Dtos;
using Application.Gerentes.Requests;
using Domain;

namespace Api.Test
{
    public class GerentesControllerTests : IntegrationTest
    {
        private readonly GerentesJsonMock _gerentesJsonMock = new();

        public GerentesControllerTests(ApiWebApplicationFactory fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetList_ReturnsOkStatus_NoParam()
        {
            var response = await _client.GetAsync("/api/gerentes");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetList_ReturnList_NoParam()
        {
            var response = await _client.GetAndDeserialize<List<Gerente>>("api/gerentes");

            response.Should().HaveCount(4);
        }

        [Fact]
        public async Task Get_ReturnsOkStatus_ValidId()
        {
            var id = _gerentesJsonMock.Get(0).Id;

            var response = await _client.GetAsync($"/api/gerentes/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_ReturnsDtoResponseBody_ValidId()
        {
            var gerente = _gerentesJsonMock.Get<GerenteDto>(0);

            var response = await _client.GetAndDeserialize<GerenteDto>($"/api/gerentes/{gerente.Id}");

            response.Should().BeEquivalentTo(gerente);
        }

        [Fact]
        public async Task Get_ReturnsNotFoundStatus_InvalidId()
        {
            var invalidId = Guid.NewGuid();

            var response = await _client.GetAsync($"/api/gerentes/{invalidId}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory, AutoData]
        public async Task Create_ReturnsCreatedStatus_ValidModel(CreateGerenteRequest createRequest)
        {
            var response = await _client.PostAsync("api/gerentes", createRequest.CreateStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Theory, AutoData]
        public async Task Create_ReturnsBadRequestStatus_InvalidModel(CreateGerenteRequest createRequest)
        {
            createRequest.Nome = string.Empty;

            var response = await _client.PostAsync("api/gerentes", createRequest.CreateStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory, AutoData]
        public async Task Update_ReturnsOkStatus_ValidModel(UpdateGerenteRequest updateRequest)
        {
            updateRequest.Id = _gerentesJsonMock.Get(0).Id;

            var response = await _client.PutAsync($"api/gerentes/{updateRequest.Id}", updateRequest.CreateStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory, AutoData]
        public async Task Update_ReturnsBadRequestStatus_InvalidModel(UpdateGerenteRequest updateRequest)
        {
            updateRequest.Id = _gerentesJsonMock.Get(0).Id;
            updateRequest.Nome = string.Empty;

            var response = await _client.PutAsync($"api/gerentes/{updateRequest.Id}", updateRequest.CreateStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory, AutoData]
        public async Task Update_ReturnsNotFoundStatus_InvalidId(UpdateGerenteRequest updateRequest)
        {
            updateRequest.Id = Guid.NewGuid();

            var response = await _client.PutAsync($"api/gerentes/{updateRequest.Id}", updateRequest.CreateStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_ReturnsNoContentStatus_ValidId()
        {
            var id = _gerentesJsonMock.Get(0).Id;

            var response = await _client.DeleteAsync($"/api/gerentes/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_ReturnsNotFoundStatus_InvalidId()
        {
            var id = Guid.NewGuid();

            var response = await _client.DeleteAsync($"/api/gerentes/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
