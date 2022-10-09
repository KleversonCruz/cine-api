using Application.Sessoes.Dtos;
using Application.Sessoes.Requests;

namespace Api.Controllers
{
    public class SessoesController : BaseApiController
    {
        [HttpGet()]
        [MustHavePermission(AppAction.List, AppResource.Sessoes)]
        [SwaggerOperation(Summary = "Get list of all sessoes.")]
        public Task<List<SessaoDto>> GetListAsync()
        {
            return Mediator.Send(new GetSessoesRequest());
        }
        [HttpGet("{id:guid}")]
        [MustHavePermission(AppAction.View, AppResource.Sessoes)]
        [SwaggerOperation(Summary = "Get sessao details.")]
        public Task<SessaoDto> GetAsync(Guid id)
        {
            return Mediator.Send(new GetSessaoRequest(id));
        }

        [HttpPost]
        [MustHavePermission(AppAction.Create, AppResource.Sessoes)]
        [SwaggerOperation(Summary = "Create a new sessao.")]
        public async Task<ActionResult> CreateAsync(CreateSessaoRequest request)
        {
            var id = await Mediator.Send(request);
            return CreatedAtAction(nameof(GetAsync), new { id }, id);
        }

        [HttpPut("{id:guid}")]
        [MustHavePermission(AppAction.Update, AppResource.Sessoes)]
        [SwaggerOperation(Summary = "Update a sessao.")]
        public async Task<ActionResult<Guid>> UpdateAsync(UpdateSessaoRequest request, Guid id)
        {
            return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
        }

        [HttpDelete("{id:guid}")]
        [SwaggerOperation(Summary = "Delete a sessao.")]
        [MustHavePermission(AppAction.Delete, AppResource.Sessoes)]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await Mediator.Send(new DeleteSessaoRequest(id));
            return NoContent();
        }
    }
}
