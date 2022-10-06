using Domain.Common;

namespace Domain
{
    public class Filme : BaseEntity, IAggregateRoot
    {
        public string Titulo { get; set; }
        public int Duracao { get; set; }
        public string Diretor { get; set; }
        public string Genero { get; set; }
        public int ClassificacaoEtaria { get; set; }
        public virtual List<Sessao> Sessoes { get; set; }
    }
}
