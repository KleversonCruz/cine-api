using Application.Common.Interfaces;
using Application.Gerentes.Dtos;

namespace Application.Cinemas.Dtos
{
    public class CinemaDetailsDto : IDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = default!;
        public Endereco Endereco { get; set; } = default!;
        public GerenteDto Gerente { get; set; } = default!;
    }
}