using Application.Cinemas.Dtos;
using Application.Cinemas.Requests;

namespace Api.Controllers
{
    public class CinemasController : BaseApiController
    {
        [HttpGet()]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get list of all cinemas.")]
        public Task<List<CinemaDto>> GetListAsync()
        {
            return Mediator.Send(new GetCinemasRequest());
        }
        [HttpGet("{id:guid}")]
        [MustHavePermission(AppAction.View, AppResource.Cinemas)]
        [SwaggerOperation(Summary = "Get cinema details.")]
        public Task<CinemaDetailsDto> GetAsync(Guid id)
        {
            return Mediator.Send(new GetCinemaRequest(id));
        }

        [HttpPost]
        [MustHavePermission(AppAction.Create, AppResource.Cinemas)]
        [SwaggerOperation(Summary = "Create a new cinema.")]
        public async Task<ActionResult> CreateAsync(CreateCinemaRequest request)
        {
            var id = await Mediator.Send(request);
            return CreatedAtAction(nameof(GetAsync), new { id }, id);
        }

        [HttpPut("{id:guid}")]
        [MustHavePermission(AppAction.Update, AppResource.Cinemas)]
        [SwaggerOperation(Summary = "Update a cinema.")]
        public async Task<ActionResult<Guid>> UpdateAsync(UpdateCinemaRequest request, Guid id)
        {
            return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
        }

        [HttpDelete("{id:guid}")]
        [SwaggerOperation(Summary = "Delete a cinema.")]
        [MustHavePermission(AppAction.Delete, AppResource.Cinemas)]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await Mediator.Send(new DeleteCinemaRequest(id));
            return NoContent();
        }
    }
}
