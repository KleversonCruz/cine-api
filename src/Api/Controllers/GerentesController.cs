using Application.Gerentes.Dtos;
using Application.Gerentes.Requests;

namespace Api.Controllers
{
    public class GerentesController : BaseApiController
    {
        [HttpGet()]
        [MustHavePermission(AppAction.List, AppResource.Gerentes)]
        [SwaggerOperation(Summary = "Get list of all gerentes.")]
        public Task<List<GerenteDto>> GetListAsync()
        {
            return Mediator.Send(new GetGerentesRequest());
        }
        [HttpGet("{id:guid}")]
        [MustHavePermission(AppAction.View, AppResource.Gerentes)]
        [SwaggerOperation(Summary = "Get gerente details.")]
        public Task<GerenteDto> GetAsync(Guid id)
        {
            return Mediator.Send(new GetGerenteRequest(id));
        }

        [HttpPost]
        [MustHavePermission(AppAction.Create, AppResource.Gerentes)]
        [SwaggerOperation(Summary = "Create a new gerente.")]
        public async Task<ActionResult> CreateAsync(CreateGerenteRequest request)
        {
            var id = await Mediator.Send(request);
            return CreatedAtAction(nameof(GetAsync), new { id }, id);
        }

        [HttpPut("{id:guid}")]
        [MustHavePermission(AppAction.Update, AppResource.Gerentes)]
        [SwaggerOperation(Summary = "Update a gerente.")]
        public async Task<ActionResult<Guid>> UpdateAsync(UpdateGerenteRequest request, Guid id)
        {
            return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
        }

        [HttpDelete("{id:guid}")]
        [SwaggerOperation(Summary = "Delete a gerente.")]
        [MustHavePermission(AppAction.Delete, AppResource.Gerentes)]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await Mediator.Send(new DeleteGerenteRequest(id));
            return NoContent();
        }
    }
}
