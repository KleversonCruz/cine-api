using Domain.Common;

namespace Domain
{
    public class Cinema : BaseEntity, IAggregateRoot
    {
        public string Nome { get; private set; } = default!;
        public Guid EnderecoId { get; private set; }
        public virtual Endereco Endereco { get; protected set; } = default!;
        public Guid GerenteId { get; protected set; }
        public virtual Gerente Gerente { get; private set; } = default!;
        public virtual List<Sessao> Sessoes { get; private set; } = default!;

        public Cinema(string nome)
        {
            Nome = nome;
        }

        public Cinema WithEndereco(Endereco endereco)
        {
            Endereco = endereco;
            return this;
        }

        public Cinema WithGerente(Guid gerenteId)
        {
            GerenteId = gerenteId;
            return this;
        }

        public Cinema Update(string nome, Endereco endereco, Guid? gerenteId)
        {
            if (nome is not null && Nome?.Equals(nome) is not true) Nome = nome;
            Endereco.Update(endereco.Logradouro, endereco.Bairro, endereco.Numero);
            if (gerenteId.HasValue && gerenteId.Value != Guid.Empty && !GerenteId.Equals(gerenteId.Value)) GerenteId = gerenteId.Value;
            return this;
        }
    }
}
