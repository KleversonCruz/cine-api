using Domain.Common;

namespace Domain
{
    public class Cinema : BaseEntity, IAggregateRoot
    {
        public string Nome { get; set; }
        public int EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }
        public int GerenteId { get; set; }
        public virtual Gerente Gerente { get; set; }
        public virtual List<Sessao> Sessoes { get; set; }
    }
}
