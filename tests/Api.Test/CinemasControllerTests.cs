using Api.Test.Mocks;
using Api.Test.Utils;
using Application.Cinemas.Dtos;
using Application.Cinemas.Requests;
using Application.Gerentes.Dtos;
using Domain;

namespace Api.Test
{
    public class CinemasControllerTests : IntegrationTest
    {
        private readonly CinemasJsonMock _cinemaJsonMock = new();
        private readonly GerentesJsonMock _gerentesJsonMock = new();

        public CinemasControllerTests(ApiWebApplicationFactory fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetList_ReturnsOkStatus_NoParam()
        {
            var response = await _client.GetAsync("/api/cinemas");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetList_ReturnList_NoParam()
        {
            var response = await _client.GetAndDeserialize<List<Cinema>>("api/cinemas");

            response.Should().HaveCount(4);
        }

        [Fact]
        public async Task Get_ReturnsOkStatus_ValidId()
        {
            var id = _cinemaJsonMock.Get(0).Id;

            var response = await _client.GetAsync($"/api/cinemas/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_ReturnsDtoResponseBody_ValidId()
        {
            var cinema = _cinemaJsonMock.Get<CinemaDetailsDto>(0);
            cinema.Gerente = _gerentesJsonMock.Get<GerenteDto>(0);

            var response = await _client.GetAndDeserialize<CinemaDetailsDto>($"/api/cinemas/{cinema.Id}");

            response.Should().BeEquivalentTo(cinema, options =>
                options.Excluding(o => o.Endereco.Id));
        }

        [Fact]
        public async Task Get_ReturnsNotFoundStatus_InvalidId()
        {
            var invalidId = Guid.NewGuid();

            var response = await _client.GetAsync($"/api/cinemas/{invalidId}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory, AutoData]
        public async Task Create_ReturnsCreatedStatus_ValidModel(CreateCinemaRequest createRequest)
        {
            createRequest.GerenteId = _gerentesJsonMock.Get(0).Id;

            var response = await _client.PostAsync("api/cinemas", createRequest.CreateStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Theory, AutoData]
        public async Task Create_ReturnsBadRequestStatus_InvalidModel(CreateCinemaRequest createRequest)
        {
            createRequest.Endereco = default!;

            var response = await _client.PostAsync("api/cinemas", createRequest.CreateStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory, AutoData]
        public async Task Create_ReturnsNotFoundStatus_InvalidGerenteId(CreateCinemaRequest createRequest)
        {
            createRequest.GerenteId = Guid.NewGuid();

            var response = await _client.PostAsync("api/cinemas", createRequest.CreateStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory, AutoData]
        public async Task Update_ReturnsOkStatus_ValidModel(UpdateCinemaRequest updateRequest)
        {
            updateRequest.GerenteId = _gerentesJsonMock.Get(0).Id;
            updateRequest.Id = _cinemaJsonMock.Get(0).Id;

            var response = await _client.PutAsync($"api/cinemas/{updateRequest.Id}", updateRequest.CreateStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory, AutoData]
        public async Task Update_ReturnsBadRequestStatus_InvalidModel(UpdateCinemaRequest updateRequest)
        {
            updateRequest.GerenteId = _gerentesJsonMock.Get(0).Id;
            updateRequest.Id = _cinemaJsonMock.Get(0).Id;
            updateRequest.Endereco = default!;

            var response = await _client.PutAsync($"api/cinemas/{updateRequest.Id}", updateRequest.CreateStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory, AutoData]
        public async Task Update_ReturnsNotFoundStatus_InvalidGerenteId(UpdateCinemaRequest updateRequest)
        {
            updateRequest.GerenteId = Guid.NewGuid();
            updateRequest.Id = _cinemaJsonMock.Get(0).Id;

            var response = await _client.PutAsync($"api/cinemas/{updateRequest.Id}", updateRequest.CreateStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory, AutoData]
        public async Task Update_ReturnsNotFoundStatus_InvalidCinemaId(UpdateCinemaRequest updateRequest)
        {
            updateRequest.GerenteId = _gerentesJsonMock.Get(0).Id;
            updateRequest.Id = Guid.NewGuid();

            var response = await _client.PutAsync($"api/cinemas/{updateRequest.Id}", updateRequest.CreateStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_ReturnsNoContentStatus_ValidId()
        {
            var id = _cinemaJsonMock.Get(0).Id;

            var response = await _client.DeleteAsync($"/api/cinemas/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_ReturnsNotFoundStatus_InvalidId()
        {
            var id = Guid.NewGuid();

            var response = await _client.DeleteAsync($"/api/cinemas/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}