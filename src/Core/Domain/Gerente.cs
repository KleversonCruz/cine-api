using Domain.Common;

namespace Domain
{
    public class Gerente : BaseEntity, IAggregateRoot
    {
        public string Nome { get; set; }
        public virtual List<Cinema> Cinemas { get; set; }
    }
}
