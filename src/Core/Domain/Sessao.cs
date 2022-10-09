using Domain.Common;

namespace Domain
{
    public class Sessao : BaseEntity, IAggregateRoot
    {
        public Guid CinemaId { get; private set; }
        public virtual Cinema Cinema { get; private set; } = default!;
        public Guid FilmeId { get; private set; }
        public virtual Filme Filme { get; private set; } = default!;
        public DateTime HorarioDeEncerramento { get; private set; }

        public Sessao(Guid cinemaId, Guid filmeId, DateTime horarioDeEncerramento)
        {
            CinemaId = cinemaId;
            FilmeId = filmeId;
            HorarioDeEncerramento = horarioDeEncerramento;
        }

        public Sessao Update(Guid? cinemaId, Guid? filmeId, DateTime? horarioDeEncerramento)
        {
            if (horarioDeEncerramento.HasValue && HorarioDeEncerramento != horarioDeEncerramento) HorarioDeEncerramento = horarioDeEncerramento.Value;
            if (cinemaId.HasValue && cinemaId.Value != Guid.Empty && !CinemaId.Equals(cinemaId.Value)) CinemaId = cinemaId.Value;
            if (filmeId.HasValue && filmeId.Value != Guid.Empty && !FilmeId.Equals(filmeId.Value)) FilmeId = filmeId.Value;
            return this;
        }
    }
}
