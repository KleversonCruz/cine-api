namespace Application.Cinemas.Specs
{
    public class GetCinemaByIdSpec<TDto> : Specification<Cinema, TDto>, ISingleResultSpecification
    {
        public GetCinemaByIdSpec(Guid id) =>
        Query
            .Where(c => c.Id == id)
            .Include(c => c.Endereco)
            .Include(c => c.Gerente);
    }

    public class GetCinemaByIdSpec : Specification<Cinema>, ISingleResultSpecification
    {
        public GetCinemaByIdSpec(Guid id) =>
        Query
            .Where(c => c.Id == id)
            .Include(c => c.Endereco)
            .Include(c => c.Gerente);
    }
}
