namespace Application.Filmes.Specs
{
    public class GetFilmeByIdSpec<TDto> : Specification<Filme, TDto>, ISingleResultSpecification
    {
        public GetFilmeByIdSpec(Guid id) =>
        Query
            .Where(c => c.Id == id);
    }

    public class GetFilmeByIdSpec : Specification<Filme>, ISingleResultSpecification
    {
        public GetFilmeByIdSpec(Guid id) =>
        Query
            .Where(c => c.Id == id);
    }
}
