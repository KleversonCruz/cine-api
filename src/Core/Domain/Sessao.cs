using Domain.Common;

namespace Domain
{
    public class Sessao : BaseEntity, IAggregateRoot
    {
        public int CinemaId { get; set; }
        public virtual Cinema Cinema { get; set; }
        public int FilmeId { get; set; }
        public virtual Filme Filme { get; set; }
        public DateTime HorarioDeEncerramento { get; set; }
    }
}
