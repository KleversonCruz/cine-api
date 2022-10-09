using Domain.Common;

namespace Domain
{
    public class Endereco : BaseEntity, IAggregateRoot
    {
        public string Logradouro { get; private set; } = default!;
        public string Bairro { get; private set; } = default!;
        public string Numero { get; private set; } = default!;

        public Endereco(string logradouro, string bairro, string numero)
        {
            Logradouro = logradouro;
            Bairro = bairro;
            Numero = numero;
        }

        public Endereco Update(string logradouro, string bairro, string numero)
        {
            if (logradouro is not null && Logradouro?.Equals(logradouro) is not true) Logradouro = logradouro;
            if (bairro is not null && Bairro?.Equals(bairro) is not true) Bairro = bairro;
            if (numero is not null && Numero?.Equals(numero) is not true) Numero = numero;
            return this;
        }
    }
}
