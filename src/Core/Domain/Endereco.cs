using Domain.Common;

namespace Domain
{
    public class Endereco : BaseEntity, IAggregateRoot
    {
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public int Numero { get; set; }
    }
}
