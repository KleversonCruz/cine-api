namespace Application.Gerentes.Specs
{

    public class GetGerenteByIdSpec<TDto> : Specification<Gerente, TDto>, ISingleResultSpecification
    {
        public GetGerenteByIdSpec(Guid id) =>
        Query
            .Where(c => c.Id == id);
    }

    public class GetGerenteByIdSpec : Specification<Gerente>, ISingleResultSpecification
    {
        public GetGerenteByIdSpec(Guid id) =>
        Query
            .Where(c => c.Id == id);
    }
}
