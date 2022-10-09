using Application.Filmes.Dtos;
using Application.Filmes.Requests;

namespace Api.Controllers
{
    public class FilmesController : BaseApiController
    {
        [HttpGet()]
        [MustHavePermission(AppAction.List, AppResource.Filmes)]
        [SwaggerOperation(Summary = "Get list of all filmes.")]
        public Task<List<FilmeDto>> GetListAsync()
        {
            return Mediator.Send(new GetFilmesRequest());
        }
        [HttpGet("{id:guid}")]
        [MustHavePermission(AppAction.View, AppResource.Filmes)]
        [SwaggerOperation(Summary = "Get filme details.")]
        public Task<FilmeDto> GetAsync(Guid id)
        {
            return Mediator.Send(new GetFilmeRequest(id));
        }

        [HttpPost]
        [MustHavePermission(AppAction.Create, AppResource.Filmes)]
        [SwaggerOperation(Summary = "Create a new filme.")]
        public async Task<ActionResult> CreateAsync(CreateFilmeRequest request)
        {
            var id = await Mediator.Send(request);
            return CreatedAtAction(nameof(GetAsync), new { id }, id);
        }

        [HttpPut("{id:guid}")]
        [MustHavePermission(AppAction.Update, AppResource.Filmes)]
        [SwaggerOperation(Summary = "Update a filme.")]
        public async Task<ActionResult<Guid>> UpdateAsync(UpdateFilmeRequest request, Guid id)
        {
            return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
        }

        [HttpDelete("{id:guid}")]
        [SwaggerOperation(Summary = "Delete a filme.")]
        [MustHavePermission(AppAction.Delete, AppResource.Filmes)]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await Mediator.Send(new DeleteFilmeRequest(id));
            return NoContent();
        }
    }
}
