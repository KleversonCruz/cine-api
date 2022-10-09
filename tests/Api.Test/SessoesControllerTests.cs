using Api.Test.Mocks;
using Api.Test.Utils;
using Application.Cinemas.Dtos;
using Application.Filmes.Dtos;
using Application.Sessoes.Dtos;
using Application.Sessoes.Requests;
using Domain;

namespace Api.Test
{
    public class SessoesControllerTests : IntegrationTest
    {
        private readonly SessoesJsonMock _sessoesJsonMock = new();
        private readonly CinemasJsonMock _cinemasJsonMock = new();
        private readonly FilmesJsonMock _filmesJsonMock = new();

        public SessoesControllerTests(ApiWebApplicationFactory fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetList_ReturnsOkStatus_NoParam()
        {
            var response = await _client.GetAsync("/api/sessoes");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetList_ReturnList_NoParam()
        {
            var response = await _client.GetAndDeserialize<List<Sessao>>("api/sessoes");

            response.Should().HaveCount(4);
        }

        [Fact]
        public async Task Get_ReturnsOkStatus_ValidId()
        {
            var id = _sessoesJsonMock.Get(0).Id;

            var response = await _client.GetAsync($"/api/sessoes/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_ReturnsDtoResponseBody_ValidId()
        {
            var sessao = _sessoesJsonMock.Get<SessaoDto>(0);
            sessao.Cinema = _cinemasJsonMock.Get<CinemaDetailsDto>(0);
            sessao.Filme = _filmesJsonMock.Get<FilmeDto>(0);

            var response = await _client.GetAndDeserialize<SessaoDto>($"/api/sessoes/{sessao.Id}");

            response.Should().BeEquivalentTo(sessao, options =>
                options.Excluding(s => s.Cinema.Endereco.Id)
                       .Excluding(s => s.Cinema.Gerente));
        }

        [Fact]
        public async Task Get_ReturnsNotFoundStatus_InvalidId()
        {
            var invalidId = Guid.NewGuid();

            var response = await _client.GetAsync($"/api/sessoes/{invalidId}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory, AutoData]
        public async Task Create_ReturnsCreatedStatus_ValidModel(CreateSessaoRequest createRequest)
        {
            createRequest.CinemaId = _cinemasJsonMock.Get(0).Id;
            createRequest.FilmeId = _filmesJsonMock.Get(0).Id;

            var response = await _client.PostAsync("api/sessoes", createRequest.CreateStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Theory, AutoData]
        public async Task Create_ReturnsBadRequestStatus_InvalidModel(CreateSessaoRequest createRequest)
        {
            createRequest.HorarioDeEncerramento = default;

            var response = await _client.PostAsync("api/sessoes", createRequest.CreateStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory, AutoData]
        public async Task Create_ReturnsNotFoundStatus_InvalidCinemaId(CreateSessaoRequest createRequest)
        {
            createRequest.CinemaId = Guid.NewGuid();
            createRequest.FilmeId = _filmesJsonMock.Get(0).Id;

            var response = await _client.PostAsync("api/sessoes", createRequest.CreateStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory, AutoData]
        public async Task Create_ReturnsNotFoundStatus_InvalidFilmeId(CreateSessaoRequest createRequest)
        {
            createRequest.CinemaId = _cinemasJsonMock.Get(0).Id;
            createRequest.FilmeId = Guid.NewGuid();

            var response = await _client.PostAsync("api/sessoes", createRequest.CreateStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory, AutoData]
        public async Task Update_ReturnsOkStatus_ValidModel(UpdateSessaoRequest updateRequest)
        {
            updateRequest.Id = _sessoesJsonMock.Get(0).Id;
            updateRequest.CinemaId = _cinemasJsonMock.Get(0).Id;
            updateRequest.FilmeId = _filmesJsonMock.Get(0).Id;

            var response = await _client.PutAsync($"api/sessoes/{updateRequest.Id}", updateRequest.CreateStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory, AutoData]
        public async Task Update_ReturnsBadRequestStatus_InvalidModel(UpdateSessaoRequest updateRequest)
        {
            updateRequest.Id = _sessoesJsonMock.Get(0).Id;
            updateRequest.CinemaId = default!;

            var response = await _client.PutAsync($"api/sessoes/{updateRequest.Id}", updateRequest.CreateStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory, AutoData]
        public async Task Update_ReturnsNotFoundStatus_InvalidCinemaId(UpdateSessaoRequest updateRequest)
        {
            updateRequest.Id = _sessoesJsonMock.Get(0).Id;
            updateRequest.CinemaId = Guid.NewGuid();
            updateRequest.FilmeId = _filmesJsonMock.Get(0).Id;

            var response = await _client.PutAsync($"api/sessoes/{updateRequest.Id}", updateRequest.CreateStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory, AutoData]
        public async Task Update_ReturnsNotFoundStatus_InvalidFilmeId(UpdateSessaoRequest updateRequest)
        {
            updateRequest.Id = _sessoesJsonMock.Get(0).Id;
            updateRequest.CinemaId = _cinemasJsonMock.Get(0).Id;
            updateRequest.FilmeId = Guid.NewGuid();

            var response = await _client.PutAsync($"api/sessoes/{updateRequest.Id}", updateRequest.CreateStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_ReturnsNoContentStatus_ValidId()
        {
            var id = _sessoesJsonMock.Get(0).Id;

            var response = await _client.DeleteAsync($"/api/sessoes/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_ReturnsNotFoundStatus_InvalidId()
        {
            var id = Guid.NewGuid();

            var response = await _client.DeleteAsync($"/api/sessoes/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}