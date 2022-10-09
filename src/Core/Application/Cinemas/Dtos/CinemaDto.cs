using Application.Common.Interfaces;

namespace Application.Cinemas.Dtos
{
    public class CinemaDto : IDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = default!;
        public Guid EnderecoId { get; set; }
        public Guid GerenteId { get; set; }
    }
}
