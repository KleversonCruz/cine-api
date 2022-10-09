using Application.Cinemas.Dtos;
using Application.Common.Interfaces;
using Application.Filmes.Dtos;

namespace Application.Sessoes.Dtos
{
    public class SessaoDto : IDto
    {
        public Guid Id { get; set; }
        public virtual CinemaDetailsDto Cinema { get; set; } = default!;
        public virtual FilmeDto Filme { get; set; } = default!;
        public DateTime HorarioDeEncerramento { get; set; }
    }
}