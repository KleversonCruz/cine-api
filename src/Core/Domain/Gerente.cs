using Domain.Common;

namespace Domain
{
    public class Gerente : BaseEntity, IAggregateRoot
    {
        public string Nome { get; private set; } = default!;
        public virtual List<Cinema> Cinemas { get; private set; } = default!;

        public Gerente(string nome)
        {
            Nome = nome;
        }

        public Gerente Update(string? nome)
        {
            if (nome is not null && Nome?.Equals(nome) is not true) Nome = nome;
            return this;
        }
    }
}
