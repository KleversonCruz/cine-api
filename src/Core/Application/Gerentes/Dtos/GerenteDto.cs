using Application.Common.Interfaces;

namespace Application.Gerentes.Dtos
{
    public class GerenteDto : IDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = default!;
    }
}