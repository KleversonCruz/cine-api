namespace Application.Sessoes.Specs
{

    public class GetSessaoByIdSpec<TDto> : Specification<Sessao, TDto>, ISingleResultSpecification
    {
        public GetSessaoByIdSpec(Guid id) =>
        Query
            .Where(c => c.Id == id);
    }

    public class GetSessaoByIdSpec : Specification<Sessao>, ISingleResultSpecification
    {
        public GetSessaoByIdSpec(Guid id) =>
        Query
            .Where(c => c.Id == id);
    }
}
