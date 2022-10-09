namespace Application.Cinemas.Specs
{
    public class GetCinemasSpec<TDto> : Specification<Cinema, TDto>
    {
        public GetCinemasSpec() =>
        Query
            .Include(c => c.Gerente)
            .Include(c => c.Endereco);
    }

    public class GetCinemasSpec : Specification<Cinema>
    {
        public GetCinemasSpec() =>
        Query
            .Include(c => c.Gerente)
            .Include(c => c.Endereco);
    }
}
