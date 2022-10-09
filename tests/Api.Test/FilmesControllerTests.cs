using Api.Test.Mocks;
using Api.Test.Utils;
using Application.Filmes.Dtos;
using Application.Filmes.Requests;
using Domain;

namespace Api.Test
{
    public class FilmesControllerTests : IntegrationTest
    {
        private readonly FilmesJsonMock _filmesJsonMock = new();

        public FilmesControllerTests(ApiWebApplicationFactory fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetList_ReturnsOkStatus_NoParam()
        {
            var response = await _client.GetAsync("/api/filmes");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetList_ReturnList_NoParam()
        {
            var response = await _client.GetAndDeserialize<List<Filme>>("api/filmes");

            response.Should().HaveCount(4);
        }

        [Fact]
        public async Task Get_ReturnsOkStatus_ValidId()
        {
            var id = _filmesJsonMock.Get(0).Id;

            var response = await _client.GetAsync($"/api/filmes/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_ReturnsDtoResponseBody_ValidId()
        {
            var filme = _filmesJsonMock.Get<FilmeDto>(0);

            var response = await _client.GetAndDeserialize<FilmeDto>($"/api/filmes/{filme.Id}");

            response.Should().BeEquivalentTo(filme);
        }

        [Fact]
        public async Task Get_ReturnsNotFoundStatus_InvalidId()
        {
            var invalidId = Guid.NewGuid();

            var response = await _client.GetAsync($"/api/filmes/{invalidId}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory, AutoData]
        public async Task Create_ReturnsCreatedStatus_ValidModel(CreateFilmeRequest createRequest)
        {
            var response = await _client.PostAsync("api/filmes", createRequest.CreateStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Theory, AutoData]
        public async Task Create_ReturnsBadRequestStatus_InvalidModel(CreateFilmeRequest createRequest)
        {
            createRequest.Duracao = 0;

            var response = await _client.PostAsync("api/filmes", createRequest.CreateStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory, AutoData]
        public async Task Update_ReturnsOkStatus_ValidModel(UpdateFilmeRequest updateRequest)
        {
            updateRequest.Id = _filmesJsonMock.Get(0).Id;

            var response = await _client.PutAsync($"api/filmes/{updateRequest.Id}", updateRequest.CreateStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory, AutoData]
        public async Task Update_ReturnsBadRequestStatus_InvalidModel(UpdateFilmeRequest updateRequest)
        {
            updateRequest.Id = _filmesJsonMock.Get(0).Id;
            updateRequest.Duracao = 0;

            var response = await _client.PutAsync($"api/filmes/{updateRequest.Id}", updateRequest.CreateStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory, AutoData]
        public async Task Update_ReturnsNotFoundStatus_InvalidId(UpdateFilmeRequest updateRequest)
        {
            updateRequest.Id = Guid.NewGuid();

            var response = await _client.PutAsync($"api/filmes/{updateRequest.Id}", updateRequest.CreateStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_ReturnsNoContentStatus_ValidId()
        {
            var id = _filmesJsonMock.Get(0).Id;

            var response = await _client.DeleteAsync($"/api/filmes/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_ReturnsNotFoundStatus_InvalidId()
        {
            var id = Guid.NewGuid();

            var response = await _client.DeleteAsync($"/api/filmes/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
